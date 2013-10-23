/* ------------------------------------------------------------------------- */
///
/// Observer.cs
/// 
/// Copyright (c) 2013 CubeSoft, Inc. All rights reserved.
///
/// MIT License
///
/// Permission is hereby granted, free of charge, to any person obtaining a
/// copy of this software and associated documentation files (the "Software"),
/// to deal in the Software without restriction, including without limitation
/// the rights to use, copy, modify, merge, publish, distribute, sublicense,
/// and/or sell copies of the Software, and to permit persons to whom the
/// Software is furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included
/// in all copies or substantial portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
/// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
/// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
/// DEALINGS IN THE SOFTWARE.
///
/* ------------------------------------------------------------------------- */
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace CubeClock.Ntp
{
    /* --------------------------------------------------------------------- */
    ///
    /// Observer
    ///
    /// <summary>
    /// NTP でサーバと定期的に通信を行い、ローカルとの時刻差を監視する
    /// ためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Observer
    {
        #region Initialization and Termination

        /* ----------------------------------------------------------------- */
        ///
        /// Observer (constructor)
        /// 
        /// <summary>
        /// 引数に指定された NTP クライアントを用いてオブジェクトを初期化
        /// します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Observer(Ntp.Client client)
        {
            _client = client;
            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += new DoWorkEventHandler(OnDoWork);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Observer (constructor)
        /// 
        /// <summary>
        /// 引数に指定されたホスト名、または IP アドレスを用いてオブジェクト
        /// を初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Observer(string host_or_ipaddr, int port = 123)
            : this(new Ntp.Client(host_or_ipaddr, port)) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Observer (constructor)
        /// 
        /// <summary>
        /// 既定の値でオブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Observer() : this(new Ntp.Client()) { }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Timeout
        /// 
        /// <summary>
        /// NTP サーバとの通信時のタイムアウト時間をミリ秒単位で取得、
        /// または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Timeout
        {
            get { return _client.ReceiveTimeout; }
            set { _client.ReceiveTimeout = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TimeToLive
        /// 
        /// <summary>
        /// NTP サーバから取得した最新の結果が有効な時間をミリ秒単位で
        /// 取得、または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int TimeToLive
        {
            get { return _ttl; }
            set { _ttl = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Client
        /// 
        /// <summary>
        /// NTP サーバと通信しているクライアントオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Ntp.Client Client
        {
            get { return _client; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LastResult
        /// 
        /// <summary>
        /// NTP サーバとの最新の通信結果を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Ntp.Packet LastResult
        {
            get { return _last; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsValid
        /// 
        /// <summary>
        /// NTP サーバから取得した最新の結果が有効であるかどうか表す値を
        /// 取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsValid
        {
            get
            {
                lock (_lock)
                {
                    return (_last != null && _last.IsValid &&
                        (DateTime.Now - _last.CreationTime).TotalMilliseconds <= _ttl);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LocalClockOffset
        /// 
        /// <summary>
        /// NTP サーバとローカルとの時刻差を取得します。
        /// </summary>
        /// 
        /// <remarks>
        /// LocalClockOffset プロパティが実行されたタイミングで、最新の
        /// 取得結果が TTL を超えていないかどうか判断します。NTP サーバとの
        /// 通信を非同期で行っている関係で、一時的に TTL を超える（古い）
        /// 結果に基づいた時刻差が返る事があります。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public TimeSpan LocalClockOffset
        {
            get
            {
                lock (_lock)
                {
                    if (!IsValid && !_worker.IsBusy) _worker.RunWorkerAsync();
                    return (_last != null) ? _last.LocalClockOffset : new TimeSpan(0);
                }
            }
        }

        #endregion

        #region Public methods

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        /// 
        /// <summary>
        /// NTP サーバとの通信結果を更新します。結果の取得に失敗した場合、
        /// 最大で max_retry 回、interval 秒毎に再試行します。
        /// </summary>
        ///
        /// <remarks>
        /// 通常、TTL の値に基づいてバックグラウンドで定期的に通信を行って
        /// いますが、ユーザが能動的に結果を更新する必要がある場合は
        /// このメソッドを実行して下さい。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public void Refresh(int max_retry, TimeSpan interval)
        {
            if (_client == null) return;
            try
            {
                lock (_lock)
                {
                    var packet = _client.Receive();
                    if (packet != null && packet.IsValid) _last = packet;
                }
            }
            catch (Exception err)
            {
                Trace.WriteLine(err.ToString());
                if (max_retry > 0)
                {
                    System.Threading.Thread.Sleep(interval);
                    Refresh(max_retry - 1, interval);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        /// 
        /// <summary>
        /// NTP サーバとの通信結果を更新します。更新に失敗した場合、再試行は
        /// 行なわれません。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Refresh() { Refresh(0, new TimeSpan(0)); }

        /* ----------------------------------------------------------------- */
        ///
        /// Reset
        /// 
        /// <summary>
        /// 通信を行う NTP サーバを変更します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public void Reset(string host_or_ipaddr, int port = 123)
        {
            var preserve = _client;
            var last = _last;
            try
            {
                if (_worker.IsBusy && !_worker.CancellationPending) _worker.CancelAsync();
                while (_worker.CancellationPending) System.Threading.Thread.Sleep(20);
                _client = new Ntp.Client(host_or_ipaddr, port);
                _client.ReceiveTimeout = preserve.ReceiveTimeout;
                _last = null;
                Refresh();
            }
            catch (Exception err)
            {
                Trace.WriteLine(err.ToString());
                _client = preserve;
                _last = last;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Synchronize
        /// 
        /// <summary>
        /// システム時計の時刻を NTP サーバと同期します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public void Synchronize()
        {
            lock (_lock)
            {
                if (!IsValid) Refresh();
                CubeClock.SystemClock.Adjust(LocalClockOffset);
                _last = null;
            }
        }

        #endregion

        #region Methods for background worker

        /* ----------------------------------------------------------------- */
        ///
        /// OnDoWork
        /// 
        /// <summary>
        /// バックグラウンドで NTP サーバとの通信を行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OnDoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Cancel) return;
            Refresh(3, new TimeSpan(0, 0, 5));
        }

        #endregion

        #region Variables
        private Ntp.Client _client = null;
        private Ntp.Packet _last = null;
        private int _ttl = 60 * 60 * 1000;
        private BackgroundWorker _worker = null;
        private object _lock = new object();
        #endregion
    }
}

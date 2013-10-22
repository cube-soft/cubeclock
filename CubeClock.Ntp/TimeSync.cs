/* ------------------------------------------------------------------------- */
///
/// TimeSync.cs
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
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace CubeClock.Ntp
{
    /* --------------------------------------------------------------------- */
    ///
    /// TimeSync
    ///
    /// <summary>
    /// NTP でサーバとローカルの時刻を同期するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class TimeSync
    {
        #region Initialization and Termination

        /* ----------------------------------------------------------------- */
        ///
        /// TimeSync (constructor)
        /// 
        /// <summary>
        /// 引数に指定された NTP クライアントを用いてオブジェクトを初期化
        /// します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TimeSync(Ntp.Client client)
        {
            _client = client;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TimeSync (constructor)
        /// 
        /// <summary>
        /// 既定の値でオブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TimeSync() : this(new Ntp.Client()) { }

        #endregion

        #region Public methods

        /* ----------------------------------------------------------------- */
        ///
        /// Run
        /// 
        /// <summary>
        /// 時刻の同期を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Run()
        {
            if (_client == null) return;
            var packet = _client.Receive();
            if (packet == null || !packet.IsValid()) return;

            Run(packet);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Run
        /// 
        /// <summary>
        /// 指定された NTP サーバから取得されたパケットを用いて、
        /// 時刻の同期を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Run(Ntp.Packet packet)
        {
            Adjust(packet.LocalClockOffset);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Adjust
        /// 
        /// <summary>
        /// ローカルの時刻を引数分ずらします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Adjust(TimeSpan offset)
        {
            var time = ToSystemTime(DateTime.Now + offset);
            if (!SetLocalTime(ref time)) throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        #endregion

        #region Other methods

        /* ----------------------------------------------------------------- */
        ///
        /// ToSystemTime
        /// 
        /// <summary>
        /// DateTime オブジェクトを Win32 API 用の構造体に変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private SystemTime ToSystemTime(DateTime src)
        {
            var dest = new SystemTime();

            dest.wYear         = (ushort)src.Year;
            dest.wMonth        = (ushort)src.Month;
            dest.wDay          = (ushort)src.Day;
            dest.wHour         = (ushort)src.Hour;
            dest.wMinute       = (ushort)src.Minute;
            dest.wSecond       = (ushort)src.Second;
            dest.wMilliseconds = (ushort)src.Millisecond;

            return dest;
        }

        #endregion

        #region Win32 APIs

        [StructLayout(LayoutKind.Sequential)]
        public struct SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

        [DllImport("kernel32.dll")]
        private static extern bool SetLocalTime(ref SystemTime lpSystemTime);

        #endregion

        #region Variables
        private Ntp.Client _client = null;
        #endregion
    }
}

/* ------------------------------------------------------------------------- */
///
/// MainForm.cs
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
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CubeClockObserver
{
    /* --------------------------------------------------------------------- */
    ///
    /// MainForm
    /// 
    /// <summary>
    /// メイン画面を表示するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class MainForm : Form
    {
        #region Initialization and Termination

        /* ----------------------------------------------------------------- */
        ///
        /// MainForm (constructor)
        /// 
        /// <summary>
        /// 既定の値でオブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public MainForm()
        {
            InitializeComponent();

            var shield = new Icon(SystemIcons.Shield, new Size(16, 16));
            SyncButton.Image = shield.ToBitmap();
            SyncNotifyIcon.ContextMenuStrip = CreateContextMenuStrip();
            LocalClockLabel.Text = DateTime.Now.ToString();
            ServerClockLabel.Text = LocalClockLabel.Text;
            AdWebBrowser.Url = new Uri(Properties.Resources.AdUrl);
            AdWebBrowser.Document.Click += new HtmlElementEventHandler(Document_Click);

            ClockTimer.Start();
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// ClockTimer_Tick
        /// 
        /// <summary>
        /// 一定時間ごとに実行されるイベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                var local  = DateTime.Now;
                if (Math.Abs((local - _last).TotalSeconds) > 3.0) _observer.Reset();
                _last = local;

                var server = local + _observer.LocalClockOffset;
                LocalClockLabel.Text  = local.ToString();
                ServerClockLabel.Text = server.ToString();
                UpdateNotifyIcon();
            }
            catch (Exception err) { Trace.WriteLine(err.ToString()); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SyncButton_Click
        /// 
        /// <summary>
        /// 時刻を同期する際に実行されるイベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SyncButton_Click(object sender, EventArgs e)
        {
            try
            {
                var info   = new System.Diagnostics.ProcessStartInfo();
                var dir    = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var exec   = System.IO.Path.Combine(dir, "CubeClockAdjuster.exe");
                var offset = (int)_observer.LocalClockOffset.TotalMilliseconds;
                info.FileName = exec;
                info.Arguments = offset.ToString();

                var process = new System.Diagnostics.Process();
                process.StartInfo = info;
                process.Start();

                _notified = false;
            }
            catch (Exception err) { Trace.WriteLine(err.ToString()); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MainForm_FormClosing
        ///
        /// <summary>
        /// Close() メソッドが実行された時に実行されるイベントハンドラです。
        /// ユーザが×ボタンを押した場合にはタスクトレイにのみ表示し、
        /// プロセス自体の終了は、タスクトレイのメニューから「終了」を
        /// 選択した場合のみとします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_exit)
            {
                Hide();
                e.Cancel = true;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OpenItem_Click
        ///
        /// <summary>
        /// タスクトレイに表示（最小化）時、「開く」メニューを選択した時に
        /// 実行されるイベントハンドラです。このイベントハンドラは、
        /// タスクトレイに表示されているアイコンを右クリックした時に
        /// 表示されるコンテキストメニューの他、アイコンをダブルクリック
        /// した時にも実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OpenItem_Click(object sender, EventArgs e)
        {
            if (Visible) return;
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Document_Click
        /// 
        /// <summary>
        /// Web ブラウザ領域がクリックされた時に実行されるイベントハンドラ
        /// です。リンク先に移動する際、既定のブラウザを使用します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Document_Click(object sender, HtmlElementEventArgs e)
        {
            var element = AdWebBrowser.Document.GetElementFromPoint(e.MousePosition);
            while (element != null)
            {
                if (element.TagName.ToLower() == "a")
                {
                    e.ReturnValue = false;
                    var link = element.GetAttribute("href");
                    if (string.IsNullOrEmpty(link)) return;

                    try { System.Diagnostics.Process.Start(link); }
                    catch (Exception err) { Trace.WriteLine(err.ToString()); }
                    return;
                }
                element = element.Parent;
            }
        }

        #endregion

        #region Other methods

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateNotifyIcon
        /// 
        /// <summary>
        /// タスクトレイ上のアイコンの表示状態を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateNotifyIcon()
        {
            var offset = (int)Math.Abs(_observer.LocalClockOffset.TotalSeconds);
            if (offset > _threshold)
            {
                if (_notified) return;
                var format = (_observer.LocalClockOffset.TotalMilliseconds <= 0) ?
                    Properties.Resources.TimeFastWarning : Properties.Resources.TimeBehindWarning;
                var message = string.Format(format, offset);
                SyncNotifyIcon.Text = message;
                SyncNotifyIcon.BalloonTipText = message;
                SyncNotifyIcon.ShowBalloonTip(30000);
                _notified = true;
            }
            else SyncNotifyIcon.Text = "CubeClock";
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateContextMenuStrip
        /// 
        /// <summary>
        /// アプリケーションで使用するコンテキストメニュを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private ContextMenuStrip CreateContextMenuStrip()
        {
            var dest = new ContextMenuStrip();

            var open = new ToolStripMenuItem();
            open.Name = "OpenToolStripMenuItem";
            open.Size = new System.Drawing.Size(100, 22);
            open.Text = "開く";
            open.Click += new System.EventHandler(this.OpenItem_Click);
            dest.Items.Add(open);

            var exit = new ToolStripMenuItem();
            exit.Name = "ExitToolStripMenuItem";
            exit.Size = new System.Drawing.Size(100, 22);
            exit.Text = "終了";
            exit.Click += (sender, e) => {
                this._exit = true;
                this.Close();
            };
            dest.Items.Add(exit);

            return dest;
        }

        #endregion

        #region Variables
        private CubeClock.Ntp.Observer _observer = new CubeClock.Ntp.Observer();
        private int _threshold = 5;
        private bool _notified = false;
        private bool _exit = false;
        private DateTime _last = DateTime.Now;
        #endregion
    }
}

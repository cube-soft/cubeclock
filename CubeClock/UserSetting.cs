/* ------------------------------------------------------------------------- */
///
/// UserSetting.cs
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
using Microsoft.Win32;

namespace CubeClock
{
    /* --------------------------------------------------------------------- */
    ///
    /// UserSetting
    /// 
    /// <summary>
    /// ユーザ設定を保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class UserSetting
    {
        #region Initialization and Termination

        /* ----------------------------------------------------------------- */
        ///
        /// UserSetting (constructor)
        /// 
        /// <summary>
        /// 既定の値でオブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public UserSetting()
        {
            try
            {
                var subkey = Registry.LocalMachine.OpenSubKey(_RegRoot, false);
                if (subkey == null) return;
                _path = subkey.GetValue(_RegPath, string.Empty) as string;
                _version = subkey.GetValue(_RegVersion, "1.0.0") as string;
            }
            catch (Exception err) { Trace.WriteLine(err.ToString()); }
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// InstallDirectory
        /// 
        /// <summary>
        /// アプリケーションをインストールしたディレクトリへのパスを取得
        /// します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string InstallDirectory
        {
            get { return _path; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        /// 
        /// <summary>
        /// アプリケーションのバージョンを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Version
        {
            get { return _version; }
        }

        public string Sever
        {
            get { return _server; }
            set { _server = value; }
        }

        public bool LaunchOnBoot
        {
            get { return _boot; }
            set { _boot = value; }
        }

        public bool Resident
        {
            get { return _resident; }
            set { _resident = value; }
        }

        public bool HideOnLaunch
        {
            get { return _hide; }
            set { _hide = value; }
        }

        public bool Notify
        {
            get { return _notify; }
            set { _notify = value; }
        }

        public int NotifyThreshold
        {
            get { return _threshold; }
            set { _threshold = value; }
        }

        #endregion

        #region Public methods

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        /// 
        /// <summary>
        /// レジストリから設定を読み込みます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Load()
        {
            try
            {
                var root = Registry.CurrentUser.OpenSubKey(_RegRoot, false);
                if (root == null) return;

                var setting = new Cube.Settings.Document();
                setting.Read(root);

                var server = setting.Root.Find(_RegServer);
                if (server != null) _server = server.GetValue(_server);

                var boot = setting.Root.Find(_RegBoot);
                if (boot != null) _boot = boot.GetValue(_boot);

                var resident = setting.Root.Find(_RegResident);
                if (resident != null) _resident = resident.GetValue(_resident);

                var hide = setting.Root.Find(_RegHide);
                if (hide != null) _hide = hide.GetValue(_hide);

                var notify = setting.Root.Find(_RegNotify);
                if (notify != null) _notify = notify.GetValue(_notify);

                var threshold = setting.Root.Find(_RegThreshold);
                if (threshold != null) _threshold = threshold.GetValue(_threshold);
            }
            catch (Exception /* err */) { }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        /// 
        /// <summary>
        /// レジストリへ設定を保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Save()
        {
            try
            {
                var root = Registry.CurrentUser.CreateSubKey(_RegRoot);
                if (root == null) return;

                var setting = new Cube.Settings.Document();
                setting.Root.Add(new Cube.Settings.Node(_RegServer, _server));
                setting.Root.Add(new Cube.Settings.Node(_RegBoot, _boot));
                setting.Root.Add(new Cube.Settings.Node(_RegResident, _resident));
                setting.Root.Add(new Cube.Settings.Node(_RegHide, _hide));
                setting.Root.Add(new Cube.Settings.Node(_RegNotify, _notify));
                setting.Root.Add(new Cube.Settings.Node(_RegThreshold, _threshold));

                setting.Write(root);
            }
            catch (Exception /* err */) { }
        }

        #endregion


        #region Variables
        private string _path = string.Empty;
        private string _version = "1.0.0";
        private string _server = "ntp.cube-soft.jp";
        private bool _boot = true;
        private bool _resident = true;
        private bool _hide = false;
        private bool _notify = true;
        private int _threshold = 1000;
        #endregion

        #region Constant variables
        private static readonly string _RegRoot = @"Software\CubeSoft\CubeClock";
        private static readonly string _RegPath = "InstallDirectory";
        private static readonly string _RegVersion = "Version";
        private static readonly string _RegServer = "Server";
        private static readonly string _RegBoot = "LaunchOnBoot";
        private static readonly string _RegResident = "Resident";
        private static readonly string _RegHide = "HideOnLaunch";
        private static readonly string _RegNotify = "Notify";
        private static readonly string _RegThreshold = "NotifyThreshold";
        #endregion
    }
}

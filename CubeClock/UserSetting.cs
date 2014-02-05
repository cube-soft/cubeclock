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

        #endregion

        #region Variables
        private string _path = string.Empty;
        private string _version = "1.0.0";
        #endregion

        #region Constant variables
        private static readonly string _RegRoot = @"Software\CubeSoft\CubeUploader";
        private static readonly string _RegPath = "InstallDirectory";
        private static readonly string _RegVersion = "Version";
        #endregion
    }
}

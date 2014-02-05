/* ------------------------------------------------------------------------- */
///
/// SettingForm.cs
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
using System.Windows.Forms;

namespace CubeClockObserver
{
    /* --------------------------------------------------------------------- */
    ///
    /// SettingForm
    /// 
    /// <summary>
    /// 設定画面を表示するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class SettingForm : Form
    {
        #region Initialization and Termination

        /* ----------------------------------------------------------------- */
        ///
        /// SettingForm (constructor)
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SettingForm()
        {
            InitializeComponent();

            HideOnLaunchCheckBox.Enabled = ResidentCheckBox.Checked;
            NotifyThresholdLabel.Enabled = NotifyCheckBox.Checked;
            NotifyThresholdNumericUpDown.Enabled = NotifyCheckBox.Checked;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SettingForm (constructor)
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SettingForm(CubeClock.UserSetting setting)
            : this()
        {
            _setting = setting;
            LoadSettings();
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// SaveButton_Click
        /// 
        /// <summary>
        /// OK ボタンが押下された時に実行されるイベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!IsValidServer())
            {
                MessageBox.Show(Properties.Resources.ServerFailed, Properties.Resources.ErrorTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            SaveSettings();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ExitButton_Click
        /// 
        /// <summary>
        /// キャンセルボタンが押下された時に実行されるイベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ResidentCheckBox_CheckedChanged
        /// 
        /// <summary>
        /// 「タスクトレイに常駐する」のチェック状態が変更された時に実行される
        /// イベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ResidentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var control = sender as CheckBox;
            if (control == null) return;

            HideOnLaunchCheckBox.Enabled = control.Checked;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ShowBalloonCheckBox_CheckedChanged
        /// 
        /// <summary>
        /// 「時刻のずれが生じている時にポップアップで通知する」のチェック
        /// 状態が変更された時に実行されるイベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ShowBalloonCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var control = sender as CheckBox;
            if (control == null) return;

            NotifyThresholdLabel.Enabled = control.Checked;
            NotifyThresholdNumericUpDown.Enabled = control.Checked;
        }

        #endregion

        #region Other methods

        /* ----------------------------------------------------------------- */
        ///
        /// LoadSetting
        /// 
        /// <summary>
        /// 設定内容を GUI に反映します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void LoadSettings()
        {
            if (_setting == null) return;

            try
            {
                ServerTextBox.Text = _setting.Sever;
                LaunchOnBootCheckBox.Checked = _setting.LaunchOnBoot;
                ResidentCheckBox.Checked = _setting.Resident;
                HideOnLaunchCheckBox.Checked = _setting.HideOnLaunch;
                NotifyCheckBox.Checked = _setting.Notify;
                NotifyThresholdNumericUpDown.Value = _setting.NotifyThreshold;
            }
            catch (Exception err) { Trace.WriteLine(err.ToString()); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadSetting
        /// 
        /// <summary>
        /// GUI の内容を UserSetting オブジェクトに保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SaveSettings()
        {
            if (_setting == null) return;

            try
            {
                _setting.Sever = ServerTextBox.Text;
                _setting.LaunchOnBoot = LaunchOnBootCheckBox.Checked;
                _setting.Resident = ResidentCheckBox.Checked;
                _setting.HideOnLaunch = HideOnLaunchCheckBox.Checked;
                _setting.Notify = NotifyCheckBox.Checked;
                _setting.NotifyThreshold = (int)NotifyThresholdNumericUpDown.Value;

                _setting.Save();
            }
            catch (Exception err) { Trace.WriteLine(err.ToString()); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsValidServer
        /// 
        /// <summary>
        /// 設定されたサーバと通信を試み、正しい NTP サーバかどうかを判別
        /// します。
        /// </summary>
        /// 
        /// <remarks>
        /// 現在は、設定されたサーバとの通信が成功し、何らかのパケットが
        /// 返ってきたら OK と判断しています。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsValidServer()
        {
            try
            {
                var client = new CubeClock.Ntp.Client(ServerTextBox.Text);
                var packet = client.Receive();
                return true;
            }
            catch (Exception err)
            {
                Trace.WriteLine(err.ToString());
                return false;
            }
        }

        #endregion

        #region Variables
        private CubeClock.UserSetting _setting = null;
        #endregion
    }
}

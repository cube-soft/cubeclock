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

            HideOnStartCheckBox.Enabled = ResidentCheckBox.Checked;
            IgnoreMsecLabel.Enabled = ShowBalloonCheckBox.Checked;
            IgnoreMsecNumericUpDown.Enabled = ShowBalloonCheckBox.Checked;
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

            HideOnStartCheckBox.Enabled = control.Checked;
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

            IgnoreMsecLabel.Enabled = control.Checked;
            IgnoreMsecNumericUpDown.Enabled = control.Checked;
        }

        #endregion
    }
}

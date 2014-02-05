namespace CubeClockObserver
{
    partial class SettingForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.ServerLabel = new System.Windows.Forms.Label();
            this.ServerTextBox = new System.Windows.Forms.TextBox();
            this.LaunchOnBootCheckBox = new System.Windows.Forms.CheckBox();
            this.ResidentCheckBox = new System.Windows.Forms.CheckBox();
            this.HideOnStartCheckBox = new System.Windows.Forms.CheckBox();
            this.ShowBalloonCheckBox = new System.Windows.Forms.CheckBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.IgnoreMsecLabel = new System.Windows.Forms.Label();
            this.IgnoreMsecNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.IgnoreMsecNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ServerLabel
            // 
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Location = new System.Drawing.Point(12, 15);
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Size = new System.Drawing.Size(61, 12);
            this.ServerLabel.TabIndex = 0;
            this.ServerLabel.Text = "NTP サーバ";
            // 
            // ServerTextBox
            // 
            this.ServerTextBox.Location = new System.Drawing.Point(79, 12);
            this.ServerTextBox.Name = "ServerTextBox";
            this.ServerTextBox.Size = new System.Drawing.Size(303, 19);
            this.ServerTextBox.TabIndex = 1;
            // 
            // LaunchOnBootCheckBox
            // 
            this.LaunchOnBootCheckBox.AutoSize = true;
            this.LaunchOnBootCheckBox.Location = new System.Drawing.Point(12, 37);
            this.LaunchOnBootCheckBox.Name = "LaunchOnBootCheckBox";
            this.LaunchOnBootCheckBox.Size = new System.Drawing.Size(236, 16);
            this.LaunchOnBootCheckBox.TabIndex = 2;
            this.LaunchOnBootCheckBox.Text = "コンピュータ起動時に CubeClock を起動する";
            this.LaunchOnBootCheckBox.UseVisualStyleBackColor = true;
            // 
            // ResidentCheckBox
            // 
            this.ResidentCheckBox.AutoSize = true;
            this.ResidentCheckBox.Location = new System.Drawing.Point(12, 59);
            this.ResidentCheckBox.Name = "ResidentCheckBox";
            this.ResidentCheckBox.Size = new System.Drawing.Size(127, 16);
            this.ResidentCheckBox.TabIndex = 3;
            this.ResidentCheckBox.Text = "タスクトレイに常駐する";
            this.ResidentCheckBox.UseVisualStyleBackColor = true;
            this.ResidentCheckBox.CheckedChanged += new System.EventHandler(this.ResidentCheckBox_CheckedChanged);
            // 
            // HideOnStartCheckBox
            // 
            this.HideOnStartCheckBox.AutoSize = true;
            this.HideOnStartCheckBox.Location = new System.Drawing.Point(30, 81);
            this.HideOnStartCheckBox.Name = "HideOnStartCheckBox";
            this.HideOnStartCheckBox.Size = new System.Drawing.Size(240, 16);
            this.HideOnStartCheckBox.TabIndex = 4;
            this.HideOnStartCheckBox.Text = "CubeClock 起動時にメイン画面を表示しない";
            this.HideOnStartCheckBox.UseVisualStyleBackColor = true;
            // 
            // ShowBalloonCheckBox
            // 
            this.ShowBalloonCheckBox.AutoSize = true;
            this.ShowBalloonCheckBox.Location = new System.Drawing.Point(12, 103);
            this.ShowBalloonCheckBox.Name = "ShowBalloonCheckBox";
            this.ShowBalloonCheckBox.Size = new System.Drawing.Size(263, 16);
            this.ShowBalloonCheckBox.TabIndex = 5;
            this.ShowBalloonCheckBox.Text = "時刻のずれが生じている時にポップアップで通知する";
            this.ShowBalloonCheckBox.UseVisualStyleBackColor = true;
            this.ShowBalloonCheckBox.CheckedChanged += new System.EventHandler(this.ShowBalloonCheckBox_CheckedChanged);
            // 
            // SaveButton
            // 
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SaveButton.Location = new System.Drawing.Point(176, 185);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(100, 25);
            this.SaveButton.TabIndex = 8;
            this.SaveButton.Text = "OK";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ExitButton.Location = new System.Drawing.Point(282, 185);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(100, 25);
            this.ExitButton.TabIndex = 9;
            this.ExitButton.Text = "キャンセル";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // IgnoreMsecLabel
            // 
            this.IgnoreMsecLabel.AutoSize = true;
            this.IgnoreMsecLabel.Location = new System.Drawing.Point(116, 127);
            this.IgnoreMsecLabel.Name = "IgnoreMsecLabel";
            this.IgnoreMsecLabel.Size = new System.Drawing.Size(152, 12);
            this.IgnoreMsecLabel.TabIndex = 11;
            this.IgnoreMsecLabel.Text = "ミリ秒以下の場合は通知しない";
            // 
            // IgnoreMsecNumericUpDown
            // 
            this.IgnoreMsecNumericUpDown.Location = new System.Drawing.Point(30, 125);
            this.IgnoreMsecNumericUpDown.Name = "IgnoreMsecNumericUpDown";
            this.IgnoreMsecNumericUpDown.Size = new System.Drawing.Size(80, 19);
            this.IgnoreMsecNumericUpDown.TabIndex = 10;
            this.IgnoreMsecNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 222);
            this.Controls.Add(this.IgnoreMsecLabel);
            this.Controls.Add(this.IgnoreMsecNumericUpDown);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ShowBalloonCheckBox);
            this.Controls.Add(this.HideOnStartCheckBox);
            this.Controls.Add(this.ResidentCheckBox);
            this.Controls.Add(this.LaunchOnBootCheckBox);
            this.Controls.Add(this.ServerTextBox);
            this.Controls.Add(this.ServerLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.Text = "CubeClock 設定";
            ((System.ComponentModel.ISupportInitialize)(this.IgnoreMsecNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ServerLabel;
        private System.Windows.Forms.TextBox ServerTextBox;
        private System.Windows.Forms.CheckBox LaunchOnBootCheckBox;
        private System.Windows.Forms.CheckBox ResidentCheckBox;
        private System.Windows.Forms.CheckBox HideOnStartCheckBox;
        private System.Windows.Forms.CheckBox ShowBalloonCheckBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Label IgnoreMsecLabel;
        private System.Windows.Forms.NumericUpDown IgnoreMsecNumericUpDown;
    }
}
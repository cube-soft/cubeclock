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
            this.HideOnLaunchCheckBox = new System.Windows.Forms.CheckBox();
            this.NotifyCheckBox = new System.Windows.Forms.CheckBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.NotifyThresholdLabel = new System.Windows.Forms.Label();
            this.NotifyThresholdNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.NotifyThresholdNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ServerLabel
            // 
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Location = new System.Drawing.Point(12, 15);
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Size = new System.Drawing.Size(61, 12);
            this.ServerLabel.TabIndex = 100;
            this.ServerLabel.Text = "NTP サーバ";
            // 
            // ServerTextBox
            // 
            this.ServerTextBox.Location = new System.Drawing.Point(79, 12);
            this.ServerTextBox.Name = "ServerTextBox";
            this.ServerTextBox.Size = new System.Drawing.Size(303, 19);
            this.ServerTextBox.TabIndex = 2;
            // 
            // LaunchOnBootCheckBox
            // 
            this.LaunchOnBootCheckBox.AutoSize = true;
            this.LaunchOnBootCheckBox.Location = new System.Drawing.Point(12, 37);
            this.LaunchOnBootCheckBox.Name = "LaunchOnBootCheckBox";
            this.LaunchOnBootCheckBox.Size = new System.Drawing.Size(236, 16);
            this.LaunchOnBootCheckBox.TabIndex = 3;
            this.LaunchOnBootCheckBox.Text = "コンピュータ起動時に CubeClock を起動する";
            this.LaunchOnBootCheckBox.UseVisualStyleBackColor = true;
            // 
            // ResidentCheckBox
            // 
            this.ResidentCheckBox.AutoSize = true;
            this.ResidentCheckBox.Location = new System.Drawing.Point(12, 59);
            this.ResidentCheckBox.Name = "ResidentCheckBox";
            this.ResidentCheckBox.Size = new System.Drawing.Size(127, 16);
            this.ResidentCheckBox.TabIndex = 4;
            this.ResidentCheckBox.Text = "タスクトレイに常駐する";
            this.ResidentCheckBox.UseVisualStyleBackColor = true;
            this.ResidentCheckBox.CheckedChanged += new System.EventHandler(this.ResidentCheckBox_CheckedChanged);
            // 
            // HideOnLaunchCheckBox
            // 
            this.HideOnLaunchCheckBox.AutoSize = true;
            this.HideOnLaunchCheckBox.Location = new System.Drawing.Point(30, 81);
            this.HideOnLaunchCheckBox.Name = "HideOnLaunchCheckBox";
            this.HideOnLaunchCheckBox.Size = new System.Drawing.Size(240, 16);
            this.HideOnLaunchCheckBox.TabIndex = 5;
            this.HideOnLaunchCheckBox.Text = "CubeClock 起動時にメイン画面を表示しない";
            this.HideOnLaunchCheckBox.UseVisualStyleBackColor = true;
            // 
            // NotifyCheckBox
            // 
            this.NotifyCheckBox.AutoSize = true;
            this.NotifyCheckBox.Location = new System.Drawing.Point(12, 103);
            this.NotifyCheckBox.Name = "NotifyCheckBox";
            this.NotifyCheckBox.Size = new System.Drawing.Size(263, 16);
            this.NotifyCheckBox.TabIndex = 6;
            this.NotifyCheckBox.Text = "時刻のずれが生じている時にポップアップで通知する";
            this.NotifyCheckBox.UseVisualStyleBackColor = true;
            this.NotifyCheckBox.CheckedChanged += new System.EventHandler(this.ShowBalloonCheckBox_CheckedChanged);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(176, 185);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(100, 25);
            this.SaveButton.TabIndex = 0;
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
            this.ExitButton.TabIndex = 1;
            this.ExitButton.Text = "キャンセル";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // NotifyThresholdLabel
            // 
            this.NotifyThresholdLabel.AutoSize = true;
            this.NotifyThresholdLabel.Location = new System.Drawing.Point(116, 127);
            this.NotifyThresholdLabel.Name = "NotifyThresholdLabel";
            this.NotifyThresholdLabel.Size = new System.Drawing.Size(152, 12);
            this.NotifyThresholdLabel.TabIndex = 100;
            this.NotifyThresholdLabel.Text = "ミリ秒以下の場合は通知しない";
            // 
            // NotifyThresholdNumericUpDown
            // 
            this.NotifyThresholdNumericUpDown.Location = new System.Drawing.Point(30, 125);
            this.NotifyThresholdNumericUpDown.Maximum = new decimal(new int[] {
            2000000000,
            0,
            0,
            0});
            this.NotifyThresholdNumericUpDown.Name = "NotifyThresholdNumericUpDown";
            this.NotifyThresholdNumericUpDown.Size = new System.Drawing.Size(80, 19);
            this.NotifyThresholdNumericUpDown.TabIndex = 7;
            this.NotifyThresholdNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 222);
            this.Controls.Add(this.NotifyThresholdLabel);
            this.Controls.Add(this.NotifyThresholdNumericUpDown);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.NotifyCheckBox);
            this.Controls.Add(this.HideOnLaunchCheckBox);
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
            ((System.ComponentModel.ISupportInitialize)(this.NotifyThresholdNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ServerLabel;
        private System.Windows.Forms.TextBox ServerTextBox;
        private System.Windows.Forms.CheckBox LaunchOnBootCheckBox;
        private System.Windows.Forms.CheckBox ResidentCheckBox;
        private System.Windows.Forms.CheckBox HideOnLaunchCheckBox;
        private System.Windows.Forms.CheckBox NotifyCheckBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Label NotifyThresholdLabel;
        private System.Windows.Forms.NumericUpDown NotifyThresholdNumericUpDown;
    }
}
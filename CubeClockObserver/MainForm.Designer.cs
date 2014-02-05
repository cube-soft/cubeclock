namespace CubeClockObserver
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LocalClockLabel = new System.Windows.Forms.Label();
            this.ServerClockLabel = new System.Windows.Forms.Label();
            this.ClockTimer = new System.Windows.Forms.Timer(this.components);
            this.SyncButton = new System.Windows.Forms.Button();
            this.SyncNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(20, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "パソコンの時刻";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(28, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "サーバの時刻";
            // 
            // LocalClockLabel
            // 
            this.LocalClockLabel.AutoSize = true;
            this.LocalClockLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LocalClockLabel.Location = new System.Drawing.Point(141, 9);
            this.LocalClockLabel.Name = "LocalClockLabel";
            this.LocalClockLabel.Size = new System.Drawing.Size(174, 16);
            this.LocalClockLabel.TabIndex = 3;
            this.LocalClockLabel.Text = "2013/10/22 11:11:11.012";
            // 
            // ServerClockLabel
            // 
            this.ServerClockLabel.AutoSize = true;
            this.ServerClockLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ServerClockLabel.Location = new System.Drawing.Point(141, 31);
            this.ServerClockLabel.Name = "ServerClockLabel";
            this.ServerClockLabel.Size = new System.Drawing.Size(174, 16);
            this.ServerClockLabel.TabIndex = 4;
            this.ServerClockLabel.Text = "2013/10/22 11:11:12.023";
            // 
            // ClockTimer
            // 
            this.ClockTimer.Tick += new System.EventHandler(this.ClockTimer_Tick);
            // 
            // SyncButton
            // 
            this.SyncButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SyncButton.Location = new System.Drawing.Point(12, 72);
            this.SyncButton.Name = "SyncButton";
            this.SyncButton.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.SyncButton.Size = new System.Drawing.Size(303, 28);
            this.SyncButton.TabIndex = 5;
            this.SyncButton.Text = "時刻を同期する...";
            this.SyncButton.UseVisualStyleBackColor = true;
            this.SyncButton.Click += new System.EventHandler(this.SyncButton_Click);
            // 
            // SyncNotifyIcon
            // 
            this.SyncNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.SyncNotifyIcon.BalloonTipText = "時刻の調整が必要です。";
            this.SyncNotifyIcon.BalloonTipTitle = "時刻の調整";
            this.SyncNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("SyncNotifyIcon.Icon")));
            this.SyncNotifyIcon.Text = "CubeClock";
            this.SyncNotifyIcon.Visible = true;
            this.SyncNotifyIcon.BalloonTipClicked += new System.EventHandler(this.OpenItem_Click);
            this.SyncNotifyIcon.DoubleClick += new System.EventHandler(this.OpenItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 112);
            this.Controls.Add(this.SyncButton);
            this.Controls.Add(this.ServerClockLabel);
            this.Controls.Add(this.LocalClockLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "CubeClock";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LocalClockLabel;
        private System.Windows.Forms.Label ServerClockLabel;
        private System.Windows.Forms.Timer ClockTimer;
        private System.Windows.Forms.Button SyncButton;
        private System.Windows.Forms.NotifyIcon SyncNotifyIcon;

    }
}


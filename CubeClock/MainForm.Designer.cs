﻿namespace CubeClock.Ntp
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LocalClockLabel = new System.Windows.Forms.Label();
            this.ServerClockLabel = new System.Windows.Forms.Label();
            this.ClockTimer = new System.Windows.Forms.Timer(this.components);
            this.SyncButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(30, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "ローカル時計";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(39, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "サーバ時計";
            // 
            // LocalClockLabel
            // 
            this.LocalClockLabel.AutoSize = true;
            this.LocalClockLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LocalClockLabel.Location = new System.Drawing.Point(135, 9);
            this.LocalClockLabel.Name = "LocalClockLabel";
            this.LocalClockLabel.Size = new System.Drawing.Size(147, 16);
            this.LocalClockLabel.TabIndex = 3;
            this.LocalClockLabel.Text = "2013/10/22 11:11:11";
            // 
            // ServerClockLabel
            // 
            this.ServerClockLabel.AutoSize = true;
            this.ServerClockLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ServerClockLabel.Location = new System.Drawing.Point(135, 31);
            this.ServerClockLabel.Name = "ServerClockLabel";
            this.ServerClockLabel.Size = new System.Drawing.Size(147, 16);
            this.ServerClockLabel.TabIndex = 4;
            this.ServerClockLabel.Text = "2013/10/22 11:11:12";
            // 
            // ClockTimer
            // 
            this.ClockTimer.Tick += new System.EventHandler(this.ClockTimer_Tick);
            // 
            // SyncButton
            // 
            this.SyncButton.Location = new System.Drawing.Point(12, 67);
            this.SyncButton.Name = "SyncButton";
            this.SyncButton.Size = new System.Drawing.Size(270, 23);
            this.SyncButton.TabIndex = 5;
            this.SyncButton.Text = "時刻を同期する";
            this.SyncButton.UseVisualStyleBackColor = true;
            this.SyncButton.Click += new System.EventHandler(this.SyncButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 102);
            this.Controls.Add(this.SyncButton);
            this.Controls.Add(this.ServerClockLabel);
            this.Controls.Add(this.LocalClockLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "CubeClock";
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

    }
}


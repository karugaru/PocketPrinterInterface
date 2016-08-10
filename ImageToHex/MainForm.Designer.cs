namespace ImageToHex
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
            this.selectFileButton = new System.Windows.Forms.Button();
            this.fileAddressTextBox = new System.Windows.Forms.TextBox();
            this.convertButton = new System.Windows.Forms.Button();
            this.ContrastNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.RefleshPortsButton = new System.Windows.Forms.Button();
            this.SendDataButton = new System.Windows.Forms.Button();
            this.PortsComboBox = new System.Windows.Forms.ComboBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.DisConnectButton = new System.Windows.Forms.Button();
            this.convertedPictureBox = new System.Windows.Forms.PictureBox();
            this.previewRefleshTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ContrastNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.convertedPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // selectFileButton
            // 
            this.selectFileButton.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.selectFileButton.Location = new System.Drawing.Point(12, 12);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(75, 33);
            this.selectFileButton.TabIndex = 0;
            this.selectFileButton.Text = "参照";
            this.selectFileButton.UseVisualStyleBackColor = true;
            this.selectFileButton.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // fileAddressTextBox
            // 
            this.fileAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileAddressTextBox.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.fileAddressTextBox.Location = new System.Drawing.Point(93, 14);
            this.fileAddressTextBox.Name = "fileAddressTextBox";
            this.fileAddressTextBox.Size = new System.Drawing.Size(636, 31);
            this.fileAddressTextBox.TabIndex = 1;
            // 
            // convertButton
            // 
            this.convertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.convertButton.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.convertButton.Location = new System.Drawing.Point(735, 12);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(75, 33);
            this.convertButton.TabIndex = 3;
            this.convertButton.Text = "変換";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // ContrastNumericUpDown
            // 
            this.ContrastNumericUpDown.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ContrastNumericUpDown.Location = new System.Drawing.Point(132, 50);
            this.ContrastNumericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.ContrastNumericUpDown.Name = "ContrastNumericUpDown";
            this.ContrastNumericUpDown.Size = new System.Drawing.Size(120, 31);
            this.ContrastNumericUpDown.TabIndex = 4;
            this.ContrastNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "コントラスト";
            // 
            // RefleshPortsButton
            // 
            this.RefleshPortsButton.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RefleshPortsButton.Location = new System.Drawing.Point(258, 49);
            this.RefleshPortsButton.Name = "RefleshPortsButton";
            this.RefleshPortsButton.Size = new System.Drawing.Size(75, 33);
            this.RefleshPortsButton.TabIndex = 6;
            this.RefleshPortsButton.Text = "更新";
            this.RefleshPortsButton.UseVisualStyleBackColor = true;
            this.RefleshPortsButton.Click += new System.EventHandler(this.RefleshPortsButton_Click);
            // 
            // SendDataButton
            // 
            this.SendDataButton.Enabled = false;
            this.SendDataButton.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SendDataButton.Location = new System.Drawing.Point(735, 49);
            this.SendDataButton.Name = "SendDataButton";
            this.SendDataButton.Size = new System.Drawing.Size(75, 33);
            this.SendDataButton.TabIndex = 7;
            this.SendDataButton.Text = "送信";
            this.SendDataButton.UseVisualStyleBackColor = true;
            this.SendDataButton.Click += new System.EventHandler(this.SendDataButton_Click);
            // 
            // PortsComboBox
            // 
            this.PortsComboBox.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PortsComboBox.FormattingEnabled = true;
            this.PortsComboBox.Location = new System.Drawing.Point(339, 49);
            this.PortsComboBox.Name = "PortsComboBox";
            this.PortsComboBox.Size = new System.Drawing.Size(202, 32);
            this.PortsComboBox.TabIndex = 8;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ConnectButton.Location = new System.Drawing.Point(558, 49);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 33);
            this.ConnectButton.TabIndex = 9;
            this.ConnectButton.Text = "接続";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // DisConnectButton
            // 
            this.DisConnectButton.Enabled = false;
            this.DisConnectButton.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DisConnectButton.Location = new System.Drawing.Point(639, 50);
            this.DisConnectButton.Name = "DisConnectButton";
            this.DisConnectButton.Size = new System.Drawing.Size(75, 33);
            this.DisConnectButton.TabIndex = 10;
            this.DisConnectButton.Text = "切断";
            this.DisConnectButton.UseVisualStyleBackColor = true;
            this.DisConnectButton.Click += new System.EventHandler(this.DisConnectButton_Click);
            // 
            // convertedPictureBox
            // 
            this.convertedPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.convertedPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.convertedPictureBox.Location = new System.Drawing.Point(12, 87);
            this.convertedPictureBox.Name = "convertedPictureBox";
            this.convertedPictureBox.Size = new System.Drawing.Size(356, 356);
            this.convertedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.convertedPictureBox.TabIndex = 11;
            this.convertedPictureBox.TabStop = false;
            this.convertedPictureBox.WaitOnLoad = true;
            // 
            // previewRefleshTimer
            // 
            this.previewRefleshTimer.Enabled = true;
            this.previewRefleshTimer.Interval = 1000;
            this.previewRefleshTimer.Tick += new System.EventHandler(this.previewRefleshTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 455);
            this.Controls.Add(this.convertedPictureBox);
            this.Controls.Add(this.DisConnectButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.PortsComboBox);
            this.Controls.Add(this.SendDataButton);
            this.Controls.Add(this.RefleshPortsButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ContrastNumericUpDown);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.fileAddressTextBox);
            this.Controls.Add(this.selectFileButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.ContrastNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.convertedPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectFileButton;
        private System.Windows.Forms.TextBox fileAddressTextBox;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.NumericUpDown ContrastNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RefleshPortsButton;
        private System.Windows.Forms.Button SendDataButton;
        private System.Windows.Forms.ComboBox PortsComboBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button DisConnectButton;
        private System.Windows.Forms.PictureBox convertedPictureBox;
        private System.Windows.Forms.Timer previewRefleshTimer;
    }
}


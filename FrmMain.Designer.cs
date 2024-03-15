namespace alisthelper
{
    partial class Frm_main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lst_runlog = new System.Windows.Forms.ListBox();
            this.btn_wallet = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chb_startup = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lst_runlog
            // 
            this.lst_runlog.FormattingEnabled = true;
            this.lst_runlog.Location = new System.Drawing.Point(12, 44);
            this.lst_runlog.Name = "lst_runlog";
            this.lst_runlog.Size = new System.Drawing.Size(450, 251);
            this.lst_runlog.TabIndex = 5;
            // 
            // btn_wallet
            // 
            this.btn_wallet.Location = new System.Drawing.Point(12, 322);
            this.btn_wallet.Name = "btn_wallet";
            this.btn_wallet.Size = new System.Drawing.Size(75, 25);
            this.btn_wallet.TabIndex = 10;
            this.btn_wallet.Text = "启动";
            this.btn_wallet.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(109, 322);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 10;
            this.button1.Text = "更新";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // chb_startup
            // 
            this.chb_startup.AutoSize = true;
            this.chb_startup.Location = new System.Drawing.Point(222, 310);
            this.chb_startup.Name = "chb_startup";
            this.chb_startup.Size = new System.Drawing.Size(74, 17);
            this.chb_startup.TabIndex = 11;
            this.chb_startup.Text = "开机启动";
            this.chb_startup.UseVisualStyleBackColor = true;
            this.chb_startup.CheckedChanged += new System.EventHandler(this.Chb_startup_CheckedChanged);
            this.chb_startup.CheckStateChanged += new System.EventHandler(this.chb_startup_CheckStateChanged);
            // 
            // Frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 380);
            this.Controls.Add(this.chb_startup);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_wallet);
            this.Controls.Add(this.lst_runlog);
            this.Name = "Frm_main";
            this.Text = "alist助手";
            this.Load += new System.EventHandler(this.Frm_main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lst_runlog;
        private System.Windows.Forms.Button btn_wallet;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chb_startup;
    }
}


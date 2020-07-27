namespace FTP
{
    partial class FTP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTP));
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnMax = new System.Windows.Forms.Button();
            this.lblHost = new System.Windows.Forms.Label();
            this.txbHost = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txbPort = new System.Windows.Forms.TextBox();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlList = new System.Windows.Forms.Panel();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.lblRemote = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txbLocal = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lsvRemote = new System.Windows.Forms.ListView();
            this.lsvLocal = new System.Windows.Forms.ListView();
            this.文件名 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.文件大小 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.文件类型 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.上次修改 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lsbStatus = new System.Windows.Forms.ListBox();
            this.lblLocal = new System.Windows.Forms.Label();
            this.pnlConnect = new System.Windows.Forms.Panel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.ckbProxy = new System.Windows.Forms.CheckBox();
            this.ckbEncrypt = new System.Windows.Forms.CheckBox();
            this.txbPasswd = new System.Windows.Forms.TextBox();
            this.txbUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPasswd = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlBody.SuspendLayout();
            this.pnlList.SuspendLayout();
            this.pnlConnect.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.pnlTitle.Controls.Add(this.btnExit);
            this.pnlTitle.Controls.Add(this.picLogo);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1000, 38);
            this.pnlTitle.TabIndex = 0;
            this.pnlTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PnlTitle_MouseDown);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(198)))), ((int)(((byte)(198)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(946, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 36);
            this.btnExit.TabIndex = 4;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(24, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(38, 38);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(68, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(60, 38);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "FTP";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.btnRestore.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRestore.BackgroundImage")));
            this.btnRestore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRestore.FlatAppearance.BorderSize = 0;
            this.btnRestore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(198)))), ((int)(((byte)(198)))));
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Location = new System.Drawing.Point(892, 2);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(54, 36);
            this.btnRestore.TabIndex = 1;
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.BtnRestore_Click);
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.btnMin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMin.BackgroundImage")));
            this.btnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(198)))), ((int)(((byte)(198)))));
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Location = new System.Drawing.Point(838, 2);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(54, 36);
            this.btnMin.TabIndex = 2;
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new System.EventHandler(this.BtnMin_Click);
            // 
            // btnMax
            // 
            this.btnMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.btnMax.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMax.BackgroundImage")));
            this.btnMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMax.FlatAppearance.BorderSize = 0;
            this.btnMax.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(198)))), ((int)(((byte)(198)))));
            this.btnMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMax.Location = new System.Drawing.Point(892, 2);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(54, 36);
            this.btnMax.TabIndex = 3;
            this.btnMax.UseVisualStyleBackColor = false;
            this.btnMax.Click += new System.EventHandler(this.BtnMax_Click);
            // 
            // lblHost
            // 
            this.lblHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHost.Location = new System.Drawing.Point(52, 29);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(66, 27);
            this.lblHost.TabIndex = 0;
            this.lblHost.Text = "主机：";
            // 
            // txbHost
            // 
            this.txbHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbHost.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbHost.Location = new System.Drawing.Point(109, 29);
            this.txbHost.Multiline = true;
            this.txbHost.Name = "txbHost";
            this.txbHost.Size = new System.Drawing.Size(200, 27);
            this.txbHost.TabIndex = 1;
            this.txbHost.Text = "39.99.225.178";
            // 
            // lblPort
            // 
            this.lblPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPort.Location = new System.Drawing.Point(382, 29);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(66, 27);
            this.lblPort.TabIndex = 8;
            this.lblPort.Text = "端口：";
            // 
            // txbPort
            // 
            this.txbPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbPort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbPort.Location = new System.Drawing.Point(439, 29);
            this.txbPort.Multiline = true;
            this.txbPort.Name = "txbPort";
            this.txbPort.Size = new System.Drawing.Size(200, 27);
            this.txbPort.TabIndex = 9;
            this.txbPort.Text = "21";
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlBody.Controls.Add(this.pnlList);
            this.pnlBody.Controls.Add(this.pnlConnect);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 38);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(1000, 730);
            this.pnlBody.TabIndex = 4;
            // 
            // pnlList
            // 
            this.pnlList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlList.Controls.Add(this.btnDownload);
            this.pnlList.Controls.Add(this.btnUpload);
            this.pnlList.Controls.Add(this.lblRemote);
            this.pnlList.Controls.Add(this.btnOpen);
            this.pnlList.Controls.Add(this.txbLocal);
            this.pnlList.Controls.Add(this.lblStatus);
            this.pnlList.Controls.Add(this.lsvRemote);
            this.pnlList.Controls.Add(this.lsvLocal);
            this.pnlList.Controls.Add(this.lsbStatus);
            this.pnlList.Controls.Add(this.lblLocal);
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlList.Location = new System.Drawing.Point(0, 127);
            this.pnlList.Name = "pnlList";
            this.pnlList.Size = new System.Drawing.Size(1000, 603);
            this.pnlList.TabIndex = 21;
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnDownload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDownload.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Location = new System.Drawing.Point(885, 379);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(90, 30);
            this.btnDownload.TabIndex = 22;
            this.btnDownload.Text = "下载";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnUpload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnUpload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUpload.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpload.Location = new System.Drawing.Point(386, 379);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(90, 30);
            this.btnUpload.TabIndex = 21;
            this.btnUpload.Text = "上传";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.BtnUpload_Click);
            // 
            // lblRemote
            // 
            this.lblRemote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRemote.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRemote.Location = new System.Drawing.Point(490, 15);
            this.lblRemote.Name = "lblRemote";
            this.lblRemote.Size = new System.Drawing.Size(126, 28);
            this.lblRemote.TabIndex = 20;
            this.lblRemote.Text = "remote:";
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnOpen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOpen.BackgroundImage")));
            this.btnOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOpen.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Location = new System.Drawing.Point(427, 15);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(46, 28);
            this.btnOpen.TabIndex = 17;
            this.btnOpen.UseVisualStyleBackColor = false;
            // 
            // txbLocal
            // 
            this.txbLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbLocal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbLocal.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txbLocal.Location = new System.Drawing.Point(109, 15);
            this.txbLocal.Multiline = true;
            this.txbLocal.Name = "txbLocal";
            this.txbLocal.Size = new System.Drawing.Size(312, 28);
            this.txbLocal.TabIndex = 17;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(20, 388);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(66, 27);
            this.lblStatus.TabIndex = 17;
            this.lblStatus.Text = "日志：";
            // 
            // lsvRemote
            // 
            this.lsvRemote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsvRemote.HideSelection = false;
            this.lsvRemote.Location = new System.Drawing.Point(494, 52);
            this.lsvRemote.Name = "lsvRemote";
            this.lsvRemote.Size = new System.Drawing.Size(481, 321);
            this.lsvRemote.TabIndex = 2;
            this.lsvRemote.UseCompatibleStateImageBehavior = false;
            // 
            // lsvLocal
            // 
            this.lsvLocal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsvLocal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.文件名,
            this.文件大小,
            this.文件类型,
            this.上次修改});
            this.lsvLocal.HideSelection = false;
            this.lsvLocal.Location = new System.Drawing.Point(24, 52);
            this.lsvLocal.Name = "lsvLocal";
            this.lsvLocal.Size = new System.Drawing.Size(449, 321);
            this.lsvLocal.TabIndex = 1;
            this.lsvLocal.UseCompatibleStateImageBehavior = false;
            // 
            // 文件名
            // 
            this.文件名.Width = 146;
            // 
            // 文件大小
            // 
            this.文件大小.Width = 97;
            // 
            // 文件类型
            // 
            this.文件类型.Width = 97;
            // 
            // 上次修改
            // 
            this.上次修改.Width = 146;
            // 
            // lsbStatus
            // 
            this.lsbStatus.FormattingEnabled = true;
            this.lsbStatus.ItemHeight = 23;
            this.lsbStatus.Location = new System.Drawing.Point(24, 418);
            this.lsbStatus.Name = "lsbStatus";
            this.lsbStatus.Size = new System.Drawing.Size(951, 165);
            this.lsbStatus.TabIndex = 0;
            // 
            // lblLocal
            // 
            this.lblLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLocal.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLocal.Location = new System.Drawing.Point(20, 15);
            this.lblLocal.Name = "lblLocal";
            this.lblLocal.Size = new System.Drawing.Size(126, 28);
            this.lblLocal.TabIndex = 19;
            this.lblLocal.Text = "本地目录：";
            // 
            // pnlConnect
            // 
            this.pnlConnect.Controls.Add(this.txbHost);
            this.pnlConnect.Controls.Add(this.txbPort);
            this.pnlConnect.Controls.Add(this.btnConnect);
            this.pnlConnect.Controls.Add(this.ckbProxy);
            this.pnlConnect.Controls.Add(this.ckbEncrypt);
            this.pnlConnect.Controls.Add(this.txbPasswd);
            this.pnlConnect.Controls.Add(this.txbUser);
            this.pnlConnect.Controls.Add(this.lblPort);
            this.pnlConnect.Controls.Add(this.lblHost);
            this.pnlConnect.Controls.Add(this.lblUser);
            this.pnlConnect.Controls.Add(this.lblPasswd);
            this.pnlConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlConnect.Location = new System.Drawing.Point(0, 0);
            this.pnlConnect.Name = "pnlConnect";
            this.pnlConnect.Size = new System.Drawing.Size(1000, 127);
            this.pnlConnect.TabIndex = 20;
            this.pnlConnect.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlConnect_Paint);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnConnect.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Location = new System.Drawing.Point(705, 78);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(240, 31);
            this.btnConnect.TabIndex = 16;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // ckbProxy
            // 
            this.ckbProxy.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ckbProxy.AutoSize = true;
            this.ckbProxy.Location = new System.Drawing.Point(845, 28);
            this.ckbProxy.Name = "ckbProxy";
            this.ckbProxy.Size = new System.Drawing.Size(100, 27);
            this.ckbProxy.TabIndex = 15;
            this.ckbProxy.Text = "绕过代理";
            this.ckbProxy.UseVisualStyleBackColor = true;
            // 
            // ckbEncrypt
            // 
            this.ckbEncrypt.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ckbEncrypt.AutoSize = true;
            this.ckbEncrypt.Location = new System.Drawing.Point(716, 28);
            this.ckbEncrypt.Name = "ckbEncrypt";
            this.ckbEncrypt.Size = new System.Drawing.Size(100, 27);
            this.ckbEncrypt.TabIndex = 14;
            this.ckbEncrypt.Text = "加密传输";
            this.ckbEncrypt.UseVisualStyleBackColor = true;
            // 
            // txbPasswd
            // 
            this.txbPasswd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbPasswd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbPasswd.Location = new System.Drawing.Point(439, 82);
            this.txbPasswd.Multiline = true;
            this.txbPasswd.Name = "txbPasswd";
            this.txbPasswd.PasswordChar = '•';
            this.txbPasswd.Size = new System.Drawing.Size(200, 27);
            this.txbPasswd.TabIndex = 13;
            this.txbPasswd.Text = "123456";
            // 
            // txbUser
            // 
            this.txbUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbUser.Location = new System.Drawing.Point(109, 81);
            this.txbUser.Multiline = true;
            this.txbUser.Name = "txbUser";
            this.txbUser.Size = new System.Drawing.Size(200, 27);
            this.txbUser.TabIndex = 11;
            this.txbUser.Text = "ftpusr";
            // 
            // lblUser
            // 
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUser.Location = new System.Drawing.Point(52, 82);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(66, 27);
            this.lblUser.TabIndex = 10;
            this.lblUser.Text = "用户：";
            // 
            // lblPasswd
            // 
            this.lblPasswd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPasswd.Location = new System.Drawing.Point(382, 82);
            this.lblPasswd.Name = "lblPasswd";
            this.lblPasswd.Size = new System.Drawing.Size(66, 27);
            this.lblPasswd.TabIndex = 12;
            this.lblPasswd.Text = "密码：";
            // 
            // FTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 768);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.btnMax);
            this.Controls.Add(this.btnMin);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.pnlTitle);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FTP";
            this.Text = "Form1";
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlBody.ResumeLayout(false);
            this.pnlList.ResumeLayout(false);
            this.pnlList.PerformLayout();
            this.pnlConnect.ResumeLayout(false);
            this.pnlConnect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Button btnMax;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.TextBox txbHost;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txbPort;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.TextBox txbPasswd;
        private System.Windows.Forms.Label lblPasswd;
        private System.Windows.Forms.TextBox txbUser;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.CheckBox ckbEncrypt;
        private System.Windows.Forms.CheckBox ckbProxy;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Panel pnlConnect;
        private System.Windows.Forms.Panel pnlList;
        private System.Windows.Forms.ListView lsvRemote;
        private System.Windows.Forms.ListView lsvLocal;
        private System.Windows.Forms.ListBox lsbStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ColumnHeader 文件名;
        private System.Windows.Forms.ColumnHeader 文件大小;
        private System.Windows.Forms.ColumnHeader 文件类型;
        private System.Windows.Forms.ColumnHeader 上次修改;
        private System.Windows.Forms.Label lblRemote;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txbLocal;
        private System.Windows.Forms.Label lblLocal;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnDownload;
    }
}


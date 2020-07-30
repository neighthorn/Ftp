using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTP
{
    public partial class FTP : Form
    {
        private TcpClient cmdSocket;  // 命令套接字
        private TcpClient dataSocket;  // 数据套接字

        private NetworkStream cmdSw;  // 命令写输入流
        private StreamReader cmdSr;  // 命令写输出流
        private NetworkStream dataSw;  // 数据写输入流
        private StreamReader dataSr;  // 数据写输出流
        private String cmd;  // 命令
        private byte[] cmdBytes;  // 字节命令

        private String host;  // 主机名/IP
        private int port;  // 命令端口号
        private String user;  // 用户名
        private String passwd;  // 用户密码

        private readonly String separator = "\r\n";  // 分隔符

        private readonly String newLine = Environment.NewLine;  // 系统默认换行符

        public FTP()
        {
            InitializeComponent();
        }

        #region 自定义标题栏
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnMax.BringToFront();
        }

        private void BtnMax_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            btnRestore.BringToFront();
        }

        private void BtnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void PnlTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        #endregion

        #region 分隔线
        private void PnlConnect_Paint(object sender, PaintEventArgs e)
        {
            Graphics pnlLine = pnlConnect.CreateGraphics();
            Pen blackPen = new Pen(Color.FromArgb(220, 220, 220), 1.5f);
            int y1 = pnlConnect.Location.Y + pnlConnect.Height - 1;
            int x2 = pnlConnect.Width;
            int y2 = y1;
            pnlLine.DrawLine(blackPen, 0, y1, x2, y2);
        }

        #endregion

        /// <summary>
        /// 输出信息到日志窗口
        /// </summary>
        /// <param name="info">要输出的信息字符串</param>
        /// <param name="code">
        ///     code = 0时， 输出命令端口返回的信息；
        ///     code = 1时， 输出必要的提示信息；
        ///     code = 2时， 输出错误信息
        /// </param>
        private void LogToUI(String info, int code = 1)
        {
            if (code == 2) info = "Error: " + info;
            lsbStatus.Items.Add(info);
            lsbStatus.SelectedIndex = lsbStatus.Items.Count - 1;
        }

        /// <summary>
        /// 输出命令端口返回的信息到日志窗口
        /// </summary>
        /// <returns>命令端口返回的字符串</returns>
        private String ShowStatus(bool show=true)
        {
            String sta = cmdSr.ReadLine();
            if(show)    LogToUI(sta, 0);
            return sta;
        }

        /// <summary>
        /// 通过命令输入流发送命令到FTP服务器
        /// </summary>
        /// <param name="cmd">构造的命令</param>
        private void SendCommand(String cmd)
        {
            cmdBytes = Encoding.ASCII.GetBytes(cmd.ToCharArray());
            cmdSw.Write(cmdBytes, 0, cmdBytes.Length);
        }

        /// <summary>
        /// 登录到FTP服务器，初始化必要的控件和命令端口输入输出流；
        /// 或者使部分控件不可使用，断开命令端口的连接
        /// </summary>
        private void Login()
        {
            // 后面会做异常处理
            host = txbHost.Text;
            port = Convert.ToInt32(txbPort.Text);
            user = txbUser.Text;
            passwd = txbPasswd.Text;

            cmdSocket = new TcpClient(host, port);
            cmdSocket.SendTimeout = 3000;
            cmdSocket.ReceiveTimeout = 3000;
            try
            {
                cmdSr = new StreamReader(cmdSocket.GetStream());
                cmdSw = cmdSocket.GetStream();
                ShowStatus();

                cmd = "USER " + user + separator;
                SendCommand(cmd);
                ShowStatus(false);

                cmd = "PASS " + passwd + separator;
                SendCommand(cmd);
                String response = ShowStatus(false);

                cmd ="TYPE "+"binary" + separator;
                SendCommand(cmd);
                ShowStatus(false);

                if (response.StartsWith("530")) throw new InvalidOperationException("账号或密码不正确, 请重新输入");

            } catch (Exception e)
            {
                LogToUI("打开命令流失败 " + e.Message, 2);
                if (cmdSr != null) cmdSr.Close();
                if (cmdSw != null) cmdSw.Close();
            }
        }

        /// <summary>
        /// 使部分控件恢复到初始状态
        /// </summary>
        private void Reset()
        {
            txbHost.Text = "";
            txbPort.Text = "";
            txbUser.Text = "";
            txbPasswd.Text = "";
            btnUpload.Enabled = false;
            btnDownload.Enabled = false;
            btnRemoteUpLevel.Enabled = false;
            txbRemote.Enabled = false;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "连接")
            {
                Cursor normal = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                btnConnect.Enabled = false;
                if (lsbStatus.SelectedIndex > 0) LogToUI(newLine);

                Login();

                Cursor.Current = normal;
                btnConnect.Enabled = true;
                String ret = Convert.ToString(lsbStatus.SelectedItem);
                if (!ret.StartsWith("Error"))
                {
                    btnConnect.Text = "断开";
                    btnUpload.Enabled = true;
                    btnDownload.Enabled = true;
                    btnRemoteUpLevel.Enabled = true;
                    txbRemote.Enabled = true;
                }

                cmd = "PWD" + separator;
                SendCommand(cmd);
                String response = cmdSr.ReadLine();
                String path = Regex.Match(response, "\\\"(.*?)\\\"").Value;
                String wd = path.Substring(1, path.Length - 2);
                txbRemote.Text = wd;
                RefreshRemoteFileList();
            }
            else
            {
                Cursor normal = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                btnConnect.Enabled = false;

                cmd = "QUIT" + separator;
                SendCommand(cmd);
                ShowStatus();

                cmdSr.Close();
                cmdSw.Close();

                //Reset();
                btnConnect.Text = "连接";
                Cursor.Current = normal;
                btnConnect.Enabled = true;
            }
        }

        /// <summary>
        /// 打开数据端口，连接相应的输入输出流
        /// </summary>
        /// <param name="sendTimeOut">数据发送流单次发送超时时间，单位为ms，默认值是3000</param>
        /// <param name="receiveTimeOut">数据接收流单次接收超时时间，单位为ms，默认值是3000</param>
        private void OpenDataSocket(int sendTimeOut = 3000, int receiveTimeOut = 3000)
        {
            cmd = "PASV" + separator;
            SendCommand(cmd);
            String ret = ShowStatus(false);

            if (ret.StartsWith("227"))
            {
                String dataSocketInfo = Regex.Match(ret, "\\(.*?\\)").Value;
                String[] socketArray = dataSocketInfo.Substring(1, dataSocketInfo.Length - 2).Split(',');
                for (int i = 0; i < socketArray.Length; i++) socketArray[i].Trim();
                int dataPort = Convert.ToInt32(socketArray[4]) * 256 + Convert.ToInt32(socketArray[5]);
                //LogToUI("Data Port is " + dataPort);

                try
                {
                    dataSocket = new TcpClient(host, dataPort);
                    dataSocket.SendTimeout = sendTimeOut;
                    dataSocket.ReceiveTimeout = receiveTimeOut;
                    dataSr = new StreamReader(dataSocket.GetStream());
                    dataSw = dataSocket.GetStream();
                } catch (Exception e)
                {
                    LogToUI("打开数据流失败 " + e.Message, 2);
                    if (dataSr != null) dataSr.Close();
                    if (dataSw != null) dataSw.Close();
                }
            }
            else
            {
                LogToUI(ret);
            }
        }

        /// <summary>
        /// 关闭输入输出端口，断开相应的输入输出流
        /// </summary>
        private void CloseDataSocket()
        {
            if (dataSr != null) dataSr.Close();
            if (dataSw != null) dataSw.Close();

            cmd = "ABOR" + separator;
            SendCommand(cmd);
            ShowStatus(false);
        }

        /// <summary>
        /// 获取本地文件大小，不存在返回-1
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        private long GetLocalFileSize(string filePath)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(filePath);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (fileInfo == null || !fileInfo.Exists)
                return -1;
            else
                return fileInfo.Length;
        }

        /// <summary>
        /// 获取FTP文件大小，不存在返回-1
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        private long GetFtpFileSize(string fileName)
        {
            cmd = "SIZE " + fileName + separator;
            SendCommand(Encoding.UTF8.GetBytes(cmd).ToString());
            string ret = ShowStatus();

            //响应码正确
            if (ret.StartsWith("213"))
            {
                return Int64.Parse(ret.Substring(4));
            }
            else//响应码错误
                return -1;
        }

        /// <summary>
        /// 上传多个文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName">文件名称</param>
        private void UploadFiles(List<String> filePath, List<String> fileName)
        {
            var nameIter = fileName.GetEnumerator();
            var pathIter = filePath.GetEnumerator();
            string res;//记录upload结果

            while (nameIter.MoveNext() && pathIter.MoveNext())
            {
                res = UploadFile(nameIter.Current, pathIter.Current);
                if(res.StartsWith("BreakPoint"))//如果需要断点续传
                {
                    while (res.StartsWith("BreakPoint"))
                        res = UploadFile(nameIter.Current, pathIter.Current);
                }
                else if(res!= "Finished.")//如果文件已经存在
                {
                    LogToUI(res);
                }
            }
        }

        /// <summary>
        /// 下载多个文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName">文件名称</param>
        private void DownloadFiles(List<String> filePath, List<String> fileName)
        {
            var nameIter = fileName.GetEnumerator();
            var pathIter = filePath.GetEnumerator();
            string res;//记录download结果

            while (nameIter.MoveNext() && pathIter.MoveNext())
            {
                res = DownloadFile(nameIter.Current, pathIter.Current);
                if (res.StartsWith("BreakPoint"))//如果需要断点续传
                {
                    while (res.StartsWith("BreakPoint"))
                        res = DownloadFile(nameIter.Current, pathIter.Current);
                }
                else if (res != "Finished.")//如果文件已经存在
                {
                    LogToUI(res);
                }
            }
        }

        /// <summary>
        /// 上传单个文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="breakPoint">设置断点，到断点停止上传</param>
        private string UploadFile(string fileName, string filePath, int  breakPoint = -1)
        {
            OpenDataSocket();//发送时间待定

            long fileSizeInFtp = GetFtpFileSize(fileName);
            long fileSize = GetLocalFileSize(filePath);

            if (fileSizeInFtp == -1)//如果文件还未被上传则直接上传
            {
                cmd = "STOR " + fileName + separator;
                SendCommand(Encoding.UTF8.GetBytes(cmd).ToString());
                string ret = ShowStatus();

                using(FileStream f=new FileStream(filePath, FileMode.Open))
                {
                    byte[] fbytes = new byte[1030];
                    int sum = 0;//记录已读取的文件字节数
                    int cnt = 0;//记录一次读取的文件字节数

                    while ((cnt=f.Read(fbytes, 0, 1024))!=0)
                    {
                        if (breakPoint != -1 && sum + cnt > breakPoint)
                        {
                            dataSw.Write(fbytes, 0, breakPoint-sum);
                            ShowStatus();
                            CloseDataSocket();
                            return "BreakPoint: " + breakPoint.ToString();
                        }
                        else
                        {
                            if (cnt == 1024)
                                dataSw.Write(fbytes, 0, cnt);
                            else
                            {
                                dataSw.Write(fbytes, 0, cnt);
                                break;
                            }
                            sum += cnt;
                        }
                    }
                }
                CloseDataSocket();
                return "Finished.";
            }
            else if (fileSizeInFtp < fileSize)//判断是否需要从断点上传
            {
                CloseDataSocket();
                UploadFromBreak(fileName, filePath, (int)fileSizeInFtp);
                return "Finished.";
            }
            else
            {
                CloseDataSocket();
                return fileName + " has already existed.";
            }
        }

        /// <summary>
        /// 下载单个文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="breakPoint">设置断点，到断点停止下载</param>
        private string DownloadFile(string fileName, string filePath, int breakPoint = -1)
        {
            OpenDataSocket();//发送时间待定

            long fileSize = GetFtpFileSize(fileName);
            long fileSizeInLocal = GetLocalFileSize(filePath);

            if (fileSizeInLocal == -1)//如果本地不存在文件
            {
                cmd = "RETR " + fileName + separator;
                SendCommand(Encoding.UTF8.GetBytes(cmd).ToString());
                string ret = ShowStatus();

                using(FileStream f=new FileStream(filePath, FileMode.Create))
                {
                    byte[] fbytes = new byte[1030];
                    int sum = 0;//记录已读取的字节数
                    int cnt = 0;//记录一次读取的字节数

                    while((cnt=dataSw.Read(fbytes, 0, 1024)) != 0)
                    {
                        if (breakPoint != -1 && sum + cnt > breakPoint)
                        {
                            f.Write(fbytes, 0, breakPoint - sum);
                            ShowStatus();
                            CloseDataSocket();
                            return "BreakPoint: " + breakPoint.ToString();
                        }
                        else
                        {
                            f.Write(fbytes, 0, cnt);
                            sum += cnt;
                        }
                    }
                }
                CloseDataSocket();
                return "Finished.";
            }
            else if (fileSizeInLocal < fileSize)//考虑从断点下载
            {
                CloseDataSocket();
                DownloadFromBreak(fileName, filePath, (int)fileSizeInLocal);
                return "Finished.";
            }
            else
            {
                CloseDataSocket();
                return fileName + " has already existed.";
            }
        }

        /// <summary>
        /// 断点续传上传文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="breakPoint">断点</param>
        private string UploadFromBreak(string fileName, string filePath, int breakPoint)
        {
            OpenDataSocket();//发送时间待定

            //申请断点续传
            cmd = "REST " + breakPoint.ToString() + separator;
            SendCommand(Encoding.UTF8.GetBytes(cmd).ToString());
            ShowStatus();

            //申请上传文件
            cmd = "STOR " + fileName + separator;
            SendCommand(Encoding.UTF8.GetBytes(cmd).ToString());
            ShowStatus();

            using(FileStream f=new FileStream(filePath, FileMode.Open))
            {
                f.Seek(breakPoint, SeekOrigin.Begin);

                byte[] fbytes = new byte[1030];
                int cnt;
                while((cnt=f.Read(fbytes, 0, 1024)) != 0)
                {
                    if (cnt == 1024)
                        dataSw.Write(fbytes, 0, cnt);
                    else
                    {
                        dataSw.Write(fbytes, 0, cnt);
                        break;
                    }
                }
            }
            CloseDataSocket();
            return "Finished.";
        }

        /// <summary>
        /// 断点续传下载文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="breakPoint">断点</param>
        private string DownloadFromBreak(string fileName, string filePath, int breakPoint)
        {
            OpenDataSocket();//发送时间待定

            cmd = "REST " + breakPoint.ToString() + separator;
            SendCommand(Encoding.UTF8.GetBytes(cmd).ToString());
            ShowStatus();

            cmd = "RETR " + fileName + separator;
            SendCommand(Encoding.UTF8.GetBytes(cmd).ToString());
            ShowStatus();

            using(FileStream f=new FileStream(filePath, FileMode.Open))
            {
                byte[] fbytes = new byte[1030];
                f.Seek(breakPoint, SeekOrigin.Begin);

                int cnt;
                while((cnt=dataSw.Read(fbytes, 0, 1024)) != 0)
                {
                    f.Write(fbytes, 0, cnt);
                }
            }
            CloseDataSocket();
            return "Finished.";
        }

        /// <summary>
        /// 上传文件按钮，可上传多个文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpload_Click(object sender, EventArgs e)
        {
            string[] fileName=new string[2];
            string[] filePath=new string[2];

            //断点设置，用于断点续传的测试，-1为默认值表示没有断点
            int breakFileNum = -1;//第几个文件作为断点文件
            int breakPoint = -1;//断点设置


            fileName[0] = "test.txt";
            fileName[1] = "test.docx";

            filePath[0] = "C:\\Users\\Azura\\Desktop\\ftpserver\\test.txt";
            filePath[1] = "C:\\Users\\Azura\\Desktop\\ftpserver\\test.docx";

            if (breakFileNum == -1)
            {
                for (int i = 0; i < fileName.Length; i++)
                    UploadFile(fileName[i], filePath[i]);
            }
            else
            {
                for (int i = 0; i < breakFileNum; i++)
                    UploadFile(fileName[i], filePath[i]);
                UploadFile(fileName[breakFileNum], filePath[breakFileNum], breakPoint);
                for (int i = breakFileNum; i < fileName.Length; i++)
                    UploadFile(fileName[i], filePath[i]);
            }
        }

        /// <summary>
        /// 下载文件按钮，可下载多个文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownload_Click(object sender, EventArgs e)
        {

        }

        #region 根据FileSystemInfo获取系统图标
        /// <summary>
        /// 包含文件信息的结构体
        /// </summary>
        /// <see cref="https://docs.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-shfileinfoa"/>
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        /// <summary>  
        /// 返回系统设置的图标  
        /// </summary>  
        /// <param name="pszPath">文件路径 如果为"", 返回文件夹的图标</param>  
        /// <param name="dwFileAttributes">0</param>  
        /// <param name="psfi">结构体</param>  
        /// <param name="cbSizeFileInfo">结构体大小</param>  
        /// <param name="uFlags">枚举类型</param>  
        /// <returns>-1失败</returns>  
        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        public enum SHGFI
        {
            SHGFI_ICON = 0x100,
            SHGFI_LARGEICON = 0x0,
            SHGFI_USEFILEATTRIBUTES = 0x10
        }


        /// <summary>  
        /// 获取文件图标
        /// </summary>  
        /// <param name="p_Path">文件全路径</param>  
        /// <returns>图标</returns>  
        private Icon GetFileIcon(string p_Path)
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(p_Path, 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI.SHGFI_ICON | SHGFI.SHGFI_LARGEICON | SHGFI.SHGFI_USEFILEATTRIBUTES));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }

        /// <summary>  
        /// 获取文件夹图标
        /// </summary>  
        /// <returns>图标</returns>  
        private Icon GetDirectoryIcon()
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(@"", 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI.SHGFI_ICON | SHGFI.SHGFI_LARGEICON));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }
        #endregion

        private ImageList GetIconList(FileSystemInfo[] fileSystemInfos)
        {
            ImageList iconList = new ImageList();
            iconList.ImageSize = new Size(16, 16);
            Icon folderIcon = GetDirectoryIcon();
            foreach (FileSystemInfo fsi in fileSystemInfos)
            {
                if (fsi is FileInfo)
                    iconList.Images.Add(GetFileIcon(fsi.FullName));
                else
                    iconList.Images.Add(folderIcon);
            }
            return iconList;
        }

        /// <summary>
        /// 将文件大小转化成合适的单位
        /// </summary>
        /// <param name="size">长整型的文件大小</param>
        /// <returns>转化成合适单位后的字符串</returns>
        private string GetFileSize(long size)
        {
            string result = "0KB";
            int filelength = size.ToString().Length;
            if (filelength < 4)
                result = size + "byte";
            else if (filelength < 7)
                result = Math.Round(Convert.ToDouble(size / 1024d), 1) + "KB";
            else if (filelength < 10)
                result = Math.Round(Convert.ToDouble(size / 1024d / 1024), 1) + "MB";
            else if (filelength < 13)
                result = Math.Round(Convert.ToDouble(size / 1024d / 1024 / 1024), 1) + "GB";
            else
                result = Math.Round(Convert.ToDouble(size / 1024d / 1024 / 1024 / 1024), 1) + "TB";
            return result;
        }

        private void InitLocalFileList()
        {
            String localPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            txbLocal.Text = localPath;
            lsvLocal.LabelEdit = true;
            lsvLocal.Scrollable = true;

            lsvLocal.Columns.Add("文件名", (int)(4.0 / 10 * lsvLocal.Width), HorizontalAlignment.Left);
            lsvLocal.Columns.Add("文件大小", (int)(3.0 / 10 * lsvLocal.Width), HorizontalAlignment.Right);
            lsvLocal.Columns.Add("文件类型", (int)(3.0 / 10 * lsvLocal.Width), HorizontalAlignment.Left);

            RefreshLocalFileList();
        }

        private void RefreshLocalFileList()
        {

            lsvLocal.Items.Clear();

            lsvLocal.BeginUpdate();
            DirectoryInfo dir = new DirectoryInfo(txbLocal.Text);
            FileSystemInfo[] fileSystemInfos = dir.GetFileSystemInfos();

            lsvLocal.SmallImageList = GetIconList(fileSystemInfos);

            for (int i = 0; i < fileSystemInfos.Length; i++)
            {
                ListViewItem lvi = new ListViewItem();
                String name = fileSystemInfos[i].Name;
                String type = fileSystemInfos[i].Attributes.ToString();
                String size;
                if (fileSystemInfos[i] is FileInfo) size = GetFileSize(((FileInfo)fileSystemInfos[i]).Length);
                else size = newLine;

                lvi.ImageIndex = i;
                lvi.Text = name;
                lvi.SubItems.Add(size);
                lvi.SubItems.Add(type);
                //lvi.SubItems.Add(fileSystemInfo[i].LastWriteTime.ToString());

                lsvLocal.Items.Add(lvi);
            }
            lsvLocal.EndUpdate();
            
        }

        private void InitRemoteFileList()
        {
            lsvRemote.LabelEdit = true;
            lsvRemote.Scrollable = true;

            lsvRemote.Columns.Add("文件名", 190, HorizontalAlignment.Left);
            lsvRemote.Columns.Add("文件大小", 144, HorizontalAlignment.Right);
            lsvRemote.Columns.Add("文件类型", 144, HorizontalAlignment.Left);
            lsvRemote.Columns.Add("权限", 140, HorizontalAlignment.Left);
            lsvRemote.Columns.Add("所有者/组", 180);
        }

        private void RefreshRemoteFileList()
        {
            lsvRemote.Items.Clear();

            OpenDataSocket();
            cmd = "MLSD" + separator;
            SendCommand(cmd);
            String response = ShowStatus(false);
            if (!response.StartsWith("500"))
            {
                UpdateRemoteFileList(ParseMLSDFormat());
            }
            else
            {
                // 比较复杂，待完善
                SendCommand("SYST" + separator);
                response = ShowStatus(false);

                SendCommand("LIST" + separator);
                ShowStatus(false);
                ShowStatus(false);

                String msg = response.Substring(4);
                if (msg.StartsWith("DOS/360"))
                {
                    UpdateRemoteFileList(ParseDOSFormat());
                }else if (msg.StartsWith("UNIX")){
                    UpdateRemoteFileList(ParseUNIXFormat());
                }
                else
                {
                    LogToUI("暂不支持当前FTP服务器", 2);
                }
            }


            CloseDataSocket();
        }

        private void UpdateRemoteFileList(List<FileItem> list)
        {
            ImageList iconList = new ImageList();
            iconList.ImageSize = new Size(16, 16);
            Icon folderIcon = GetDirectoryIcon();
            foreach(FileItem fi in list)
            {
                if (fi.fileType == "Directory")
                    iconList.Images.Add(folderIcon);
                else
                    iconList.Images.Add(GetFileIcon(fi.name));
            }

            lsvRemote.BeginUpdate();
            lsvRemote.SmallImageList = iconList;
            for(int i = 0; i < list.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = i;
                lvi.Text = list[i].name;
                lvi.SubItems.Add(list[i].size);
                lvi.SubItems.Add(list[i].fileType);
                lvi.SubItems.Add(list[i].permission);
                lvi.SubItems.Add(list[i].owner);
                lsvRemote.Items.Add(lvi);
            }
            lsvRemote.EndUpdate();
        }

        private struct FileItem
        {
            public String name;
            public String size;
            public String fileType;
            public String permission;
            public String owner;
        }

        private List<FileItem> ParseMLSDFormat()
        {
            List<FileItem> fileItems = new List<FileItem>();

            String pattern = @"Type=(.*?);Size=(.*?)Modify=(.*?);\s+(.*?)";
            Regex regex = new Regex(pattern);
            while (!dataSr.EndOfStream)
            {
                FileItem item;
                String line = dataSr.ReadLine();
                Match match = regex.Match(line);
                item.fileType = (match.Groups[0].Value!="file")?"Directory":newLine;
                item.size = (item.fileType != "Directory") ? GetFileSize(long.Parse(match.Groups[1].Value)) : newLine;
                item.name = match.Groups[3].Value;
                item.permission = newLine;
                item.owner = newLine;
                fileItems.Add(item);
            }

            return fileItems;
        }

        private List<FileItem> ParseDOSFormat()
        {
            List<FileItem> fileItems = new List<FileItem>();

            string pattern = @"^(\d+-\d+-\d+\s+\d+:\d+(?:AM|PM))\s+(<DIR>|\d+)\s+(.+)$";
            Regex regex = new Regex(pattern);
            while (!dataSr.EndOfStream)
            {
                FileItem item;
                string line = dataSr.ReadLine();
                Match match = regex.Match(line);
                String s = match.Groups[2].Value;
                item.size = (s != "<DIR>") ? GetFileSize(long.Parse(s)) : newLine;
                item.name = match.Groups[3].Value;
                item.fileType = (s == "<DIR>") ? "Directory" : newLine;
                item.permission = newLine;
                item.owner = newLine;
                fileItems.Add(item);
            }

            return fileItems;
        }

        private List<FileItem> ParseUNIXFormat()
        {
            List<FileItem> fileItems = new List<FileItem>();

            string pattern =
                @"^([\w-]+)\s+(\d+)\s+(\w+)\s+(\w+)\s+(\d+)\s+" +
                @"(\w+\s+\d+\s+\d+|\w+\s+\d+\s+\d+:\d+)\s+(.+)$";
            Regex regex = new Regex(pattern);
            IFormatProvider culture = CultureInfo.GetCultureInfo("en-us");

            while (!dataSr.EndOfStream)
            {
                FileItem item;
                string line = dataSr.ReadLine();
                Match match = regex.Match(line);
                String permisson = match.Groups[1].Value;
                string owner = match.Groups[3].Value;
                string group = match.Groups[4].Value;
                long size = long.Parse(match.Groups[5].Value, culture);
                string name = match.Groups[7].Value;

                item.name = name;
                item.size = (size != 0) ? GetFileSize(size) : newLine;
                item.fileType = (permisson.StartsWith("d")) ? "Directory" : newLine;
                item.permission = permisson;
                item.owner = owner + "/" + group;
                fileItems.Add(item);
            }

            return fileItems;
        }

        private void FTP_Load(object sender, EventArgs e)
        {
            // 后面再解决缩放问题
            btnMax.Enabled = false;
            btnRestore.Enabled = false;

            txbLocal.ReadOnly = true;
            txbRemote.ReadOnly = true;
            txbRemote.Enabled = false;
            btnRemoteUpLevel.Enabled = false;

            InitLocalFileList();
            InitRemoteFileList();
        }

        

        private void LsvLocal_DoubleClick(object sender, EventArgs e)
        {
            if (lsvLocal.SelectedItems.Count!=1||lsvLocal.SelectedItems==null) return;
            if (lsvLocal.SelectedItems[0].SubItems[2].Text.Contains("Directory"))
            {
                txbLocal.Text = txbLocal.Text + "\\" + lsvLocal.SelectedItems[0].Text;
                //LogToUI("本地文件目录已切换到："+txbLocal.Text);
                RefreshLocalFileList();
            }
            else
            {
                System.Diagnostics.Process.Start(txbLocal.Text + "\\" + lsvLocal.SelectedItems[0].SubItems[0].Text);
            }
        }

        private void TxbLocal_Click(object sender, EventArgs e)
        {
            String prevPath=txbLocal.Text;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK) txbLocal.Text = fbd.SelectedPath;
            if (prevPath != txbLocal.Text)
                RefreshLocalFileList();
        }

        private void BtnUpLevel_Click(object sender, EventArgs e)
        {
            if (Directory.GetParent(txbLocal.Text) is null) return;
            else
            {
                txbLocal.Text = Directory.GetParent(txbLocal.Text).ToString();
                RefreshLocalFileList();
            }  
        }

        private void LsvRemote_DoubleClick(object sender, EventArgs e)
        {
            if (lsvRemote.SelectedItems.Count != 1 || lsvRemote.SelectedItems == null) return;
            if (lsvRemote.SelectedItems[0].SubItems[2].Text.Contains("Directory"))
            {
                txbRemote.Text = txbRemote.Text + "/" + lsvRemote.SelectedItems[0].Text;
                cmd = "CWD " + txbRemote.Text + separator;
                SendCommand(cmd);
                String response = ShowStatus(false);
                if (!response.StartsWith("250"))
                {
                    LogToUI("当前路径不支持切换目录");
                    return;
                }
                RefreshRemoteFileList();
            }
            else
            {
                //
                // 下载
                // 打开
                //
            }
        }

        private void BtnRemoteUpLevel_Click(object sender, EventArgs e)
        {
            String separatorChar = String.Empty;
            if (txbRemote.Text.StartsWith("/")) separatorChar = "/";
            else if (txbRemote.Text.Contains(@"\")) separatorChar = @"\";
            else separatorChar = "/";
            int pos = txbRemote.Text.LastIndexOf(separatorChar);
            String parent = txbRemote.Text.Substring(0, pos);

            if (parent == String.Empty) parent = "/";
            txbRemote.Text = parent;
            cmd = "CWD " + txbRemote.Text + separator;
            SendCommand(cmd);
            String response = ShowStatus(false);
            if (!response.StartsWith("250"))
            {
                LogToUI("当前路径不支持切换目录");
                return;
            }
            RefreshRemoteFileList();
        }

        private void 上传ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            List<String> selectedFilesPath = new List<string>();
            List<String> selectedFilesName = new List<string>();
            if (!(lsvLocal.SelectedItems.Count > 0)) return;
            foreach(ListViewItem file in lsvLocal.SelectedItems)
            {
                if (!file.SubItems[2].Text.Contains("Directory"))
                {
                    selectedFilesPath.Add(txbLocal.Text + "\\" + file.Text);
                    selectedFilesName.Add(file.Text);
                }
            }
            UploadFiles(selectedFilesPath, selectedFilesName);
            RefreshRemoteFileList();
        }

        private void 下载toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            String separatorChar = String.Empty;
            if (txbRemote.Text.StartsWith("/")) separatorChar = "/";
            else if (txbRemote.Text.Contains(@"\")) separatorChar = @"\";
            else separatorChar = "/";

            List<String> selectedFilesPath = new List<string>();
            List<String> selectedFilesName = new List<string>();
            if (!(lsvRemote.SelectedItems.Count > 0)) return;
            foreach(ListViewItem file in lsvRemote.SelectedItems)
            {
                if (!file.SubItems[2].Text.Contains("Directory"))
                {
                    selectedFilesPath.Add(txbRemote.Text + separatorChar + file.Text);
                    selectedFilesName.Add(file.Text);
                }
            }
            // 
            // 下载
            //
            RefreshLocalFileList();
        }
    }
}

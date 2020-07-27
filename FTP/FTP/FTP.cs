using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            // 后面再解决缩放问题
            btnMax.Enabled = false;
            btnRestore.Enabled = false;
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
        private void LogToUI(String info, int code=1)
        {
            if (code == 2) info = "Error: " + info;
            lsbStatus.Items.Add(info);
            lsbStatus.SelectedIndex = lsbStatus.Items.Count - 1;
        }
        
        /// <summary>
        /// 输出命令端口返回的信息到日志窗口
        /// </summary>
        /// <returns>命令端口返回的字符串</returns>
        private String ShowStatus()
        {
            String sta = cmdSr.ReadLine();
            LogToUI(sta, 0);
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
                //LogToUI(cmd);
                SendCommand(cmd);
                ShowStatus();

                cmd = "PASS " + passwd + separator;
                //LogToUI(cmd);
                SendCommand(cmd);
                String response = ShowStatus();

                if (response.StartsWith("530")) throw new InvalidOperationException("账号或密码不正确, 请重新输入");

            }catch(Exception e)
            {
                LogToUI(e.Message, 2);
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
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "连接")
            {
                Cursor normal = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                btnConnect.Enabled = false;
                if(lsbStatus.SelectedIndex>0)    LogToUI(newLine);

                Login();

                Cursor.Current = normal;
                btnConnect.Enabled = true;
                String ret = Convert.ToString(lsbStatus.SelectedItem);
                if (!ret.StartsWith("Error"))
                {
                    btnConnect.Text = "断开";
                    btnUpload.Enabled = true;
                    btnDownload.Enabled = true;
                }

                OpenDataSocket();
                CloseDataSocket();
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
        /// <param name="sendTimeOut">数据发送流单次发送超时时间</param>
        /// <param name="receiveTimeOut">数据接收流单次接收超时时间</param>
        private void OpenDataSocket(int sendTimeOut=3000, int receiveTimeOut=3000)
        {
            cmd = "PASV" + separator;
            SendCommand(cmd);
            String ret = ShowStatus();

            if (ret.StartsWith("227"))
            {
                String dataSocketInfo = Regex.Match(ret, "\\(.*?\\)").Value;
                String[] socketArray = dataSocketInfo.Substring(1, dataSocketInfo.Length - 2).Split(',');
                for (int i = 0; i < socketArray.Length; i++) socketArray[i].Trim();
                int dataPort = Convert.ToInt32(socketArray[4]) * 256 + Convert.ToInt32(socketArray[5]);
                LogToUI("Data Port is "+dataPort);

                try
                {
                    dataSocket = new TcpClient(host, dataPort);
                    dataSocket.SendTimeout = sendTimeOut;
                    dataSocket.ReceiveTimeout = receiveTimeOut;
                    dataSr = new StreamReader(dataSocket.GetStream());
                    dataSw = dataSocket.GetStream();
                }catch(Exception e)
                {
                    LogToUI(e.Message, 2);
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
            ShowStatus();
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {

        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {

        }
    }
}

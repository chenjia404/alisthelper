using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace alisthelper
{
    public partial class Frm_main : Form
    {
        public Frm_main()
        {
            InitializeComponent();
        }
        //实例化process对象  
        Process process = new Process();

        private INI config;

        private void Frm_main_Load(object sender, EventArgs e)
        {
            //异步任务
            try
            {
                this.LoadConfig();
                Thread sync_worker = new Thread(worker)
                {
                    IsBackground = true
                };
                sync_worker.Start();
            }
            catch (Exception ex)
            {
                Util.Logging.Log(ex);
            }
        }

        public void LoadConfig()
        {
            #region 配置文件

            //不存在就创建
            if (!File.Exists(Application.StartupPath + @"\config.ini"))
                System.IO.File.WriteAllLines(Application.StartupPath + @"\config.ini", new string[0]);

            if (File.Exists(Application.StartupPath + @"\config.ini"))
            {
                this.config = new INI(Application.StartupPath + @"\config.ini");


                string startup = config.ReadValue("config", "startup");
                if (startup == "1")
                    chb_startup.Checked = true;


            }
            #endregion
        }

        public void worker()
        {
            process = new Process();
            process.StartInfo.FileName = "alist.exe";
            process.StartInfo.Arguments = "server";

            process.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            process.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            process.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            process.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            process.StartInfo.CreateNoWindow = true;//不显示程序窗口
            bool is_start = process.Start();//启动程序
            AddItemToListBox("启动alist成功");
            process.BeginOutputReadLine();
            process.OutputDataReceived += (sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    AddItemToListBox(e.Data);
                }
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    AddItemToListBox(e.Data);
                }
            };

            process.StandardInput.AutoFlush = true;


            process.Exited += new EventHandler(myProcess_Exited);
        }

        private void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
            {
                AddItemToListBox(e.Data);
            }
        }

        int node_restart_wait_time = 1000;//单位毫秒
        private void myProcess_Exited(object sender, EventArgs e)
        {
            //等待一秒，也许是主动被杀
            Thread.Sleep(node_restart_wait_time);
            if (process.ExitCode > 0)
            {
                AddItemToListBox("alist意外退出，现在开始重启");
                if (node_restart_wait_time <= 60 * 1000)
                    node_restart_wait_time = node_restart_wait_time * 2;
            }
        }

        delegate void AddItemToListBoxDelegate(string str);

        /// <summary>  
        /// 在ListBox中追加状态信息  
        /// </summary>  
        /// <param name="str">要追加的信息</param>
        private void AddItemToListBox(string str)
        {
            if (lst_runlog.InvokeRequired)
            {
                AddItemToListBoxDelegate d = AddItemToListBox;
                lst_runlog.Invoke(d, str);
            }
            else
            {
                lst_runlog.Items.Add(str);
                lst_runlog.SelectedIndex = lst_runlog.Items.Count - 1;
                lst_runlog.ClearSelected();
            }
        }

        private void Chb_startup_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void chb_startup_CheckStateChanged(object sender, EventArgs e)
        {
            RegistryKey reg = null;
            try
            {
                string fileName = Application.StartupPath + @"\alist.exe";
                string name = "alist_" + Application.StartupPath.GetHashCode();


                //取消
                if (!chb_startup.Checked && config.ReadValue("config", "startup") == "1")
                {
                    AutoStartup.Set(false);
                    config.Writue("config", "startup", "0");
                }
                //开机启动
                else if (chb_startup.Checked && config.ReadValue("config", "startup") != "1")
                {
                    AutoStartup.Set(true);
                    config.Writue("config", "startup", "1");
                    chb_startup.Checked = true;
                }
            }
            catch (System.Security.SecurityException)
            {
                MessageBox.Show("需要管理员权限，请重新打开本程序(右键『已管理员身份运行』后再设置)");
            }
        }
    }
}

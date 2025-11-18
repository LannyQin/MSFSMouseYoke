using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.DualShock4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSFSMouseYoke
{
    internal class Controller
    {
        static private ViGEmClient client;
        static private IDualShock4Controller controller;
        static public bool isConnected = false;
        static private Settings settings;

        static public Thread updateThread;
        static private bool running;

        static private byte _x = 127;
        static private byte _y = 127;

        internal static void Initialize(Settings _settings)
        {
            WarningIfNotInstalled();
            settings = _settings;
            client = new ViGEmClient();
            controller = client.CreateDualShock4Controller();
            Connect();

            running = true;
            updateThread = new Thread(new ThreadStart(_Update));
            updateThread.Start();
        }

        internal static void WarningIfNotInstalled()
        {
            if (!IsViGEmBusInstalled())
            {
                DialogResult result = MessageBox.Show("未检测到ViGEmBus驱动程序，请检查ViGEmBus是否正确安装，若未安装控制器功能将无法使用。\n请前往 https://github.com/nefarius/ViGEmBus/releases 下载并安装ViGEmBus驱动程序。\n点击“是”退出程序并前往下载\n点击“否”忽略警告并继续使用本软件\n点击“取消”以退出本软件", "警告", System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    var url = "https://github.com/nefarius/ViGEmBus/releases";
                    try
                    {
                        var psi = new ProcessStartInfo
                        {
                            FileName = url,
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                    }
                    catch (Win32Exception)
                    {
                        // 备选方案：通过 cmd 启动，或提示用户手动打开链接
                        Process.Start("cmd", $"/c start \"\" \"{url}\"");
                    }

                    Environment.Exit(0);
                }
                else if (result == DialogResult.Cancel)
                {
                    Environment.Exit(0);
                }
                else if (result == DialogResult.No)
                { }
            }
        }

        internal static bool IsViGEmBusInstalled()
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services"))
                {
                    if (key != null)
                    {
                        var subKeyNames = key.GetSubKeyNames();
                        return subKeyNames.Any(name => name.Contains("ViGEmBus"));
                    }
                }
            }
            catch
            {
                // 忽略错误
            }
            return false;
        }

        internal static void Connect()
        {
            if (!isConnected)
            {
                controller.Connect();
                isConnected = true;
            }
        }

        internal static void Disconnect()
        {
            if (isConnected)
            {
                controller.Disconnect();
                isConnected = false;
            }
        }

        internal static void ChangeConnectionStatus()
        {
            if (isConnected)
            {
                Disconnect();
            }
            else
            {
                Connect();
            }

        }

        internal static void Update(double x, double y)
        {
            _x = Convert.ToByte(Math.Min(Math.Max((x + 1.0) * 128, 0), 255));
            _y = Convert.ToByte(Math.Min(Math.Max((y + 1.0) * 128, 0), 255));
        }

        private static void _Update()
        {
            while (running)
            {
                if (isConnected)
                {
                    controller.SetAxisValue(DualShock4Axis.LeftThumbX, _x);
                    controller.SetAxisValue(DualShock4Axis.LeftThumbY, _y);

                    // 提交更新
                    controller.SubmitReport();
                }
            }
        }

        internal static void Dispose()
        {
            running = false;
            Disconnect();
            client.Dispose();
        }
    }
}

using MSFSMouseYoke.Properties;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.DualShock4;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System;
using System.Threading;
using System.Windows.Forms;

namespace MSFSMouseYoke
{
    internal class Controller
    {
        static private ViGEmClient client;
        static private Settings settings;
        static public bool isConnected => controller?.isConnected ?? false;
        static public bool running => controller?.running ?? false;
        static public IController controller;

        internal static void Initialize(Settings _settings)
        {
            WarningIfNotInstalled();
            settings = _settings;

            client?.Dispose();
            client = new ViGEmClient();

            if (true)    //todo
                controller = new Xbox360Controller();
            else
                controller = new DualShock4Controller();

            controller.Initialize(settings, client);

        }

        internal static void WarningIfNotInstalled()
        {
            if (!IsViGEmBusInstalled())
            {
                MessageBox.Show(
                    "未检测到 ViGEmBus 驱动，请安装后重试。\nhttps://github.com/nefarius/ViGEmBus/releases",
                    "警告",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                Environment.Exit(0);
            }
        }

        internal static bool IsViGEmBusInstalled()
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\ViGEmBus"))
                {
                    return key != null;
                }
            }
            catch { return false; }
        }

        public static void Connent()
        {
            controller.Connect();
        }
        public static void Disconnect()
        {
            controller.Disconnect();
        }
        public static void ChangeConnectionStatus()
        {
            controller.ChangeConnectionStatus();
        }
        public static void Update(double x, double y)
        {
            controller.Update(x, y);
        }
        public static void Dispose()
        {
            controller.Dispose();
        }
    }

    interface IController
    {
        bool running { get; }
        bool isConnected { get; }
        void Initialize(Settings _settings, ViGEmClient _client);
        void Connect();
        void Disconnect();
        void ChangeConnectionStatus();
        void Update(double x, double y);  //Update:程序界面将鼠标坐标发送给具体类并处理    _Update:线程循环将处理后的坐标发送给虚拟手柄
        void Dispose();
    }

    internal class Xbox360Controller : IController
    {
        private ViGEmClient client;
        private IXbox360Controller controller;
        private Settings settings;
        public bool isConnected { get; private set; }

        private Thread updateThread;
        public bool running { get; private set; }

        private short _x = 0;
        private short _y = 0;

        public void Initialize(Settings _settings, ViGEmClient _client)
        {
            settings = _settings;
            client = _client;
            isConnected = false;

            controller = client.CreateXbox360Controller();
            Connect();

            running = true;
            updateThread = new Thread(_Update);
            updateThread.IsBackground = true;
            updateThread.Start();
        }



        public void Connect()
        {
            if (!isConnected)
            {
                controller.Connect();
                isConnected = true;
            }
        }

        public void Disconnect()
        {
            if (isConnected)
            {
                controller.Disconnect();
                isConnected = false;
            }
        }

        public void ChangeConnectionStatus()
        {
            if (isConnected) Disconnect();
            else Connect();
        }

        public void Update(double x, double y)
        {
            _x = (short)(Math.Max(-1, Math.Min(1, x)) * 32767);
            _y = (short)(Math.Max(-1, Math.Min(1, y)) * 32767);
        }

        private void _Update()
        {
            while (running)
            {
                if (isConnected)
                {
                    controller.SetAxisValue(Xbox360Axis.LeftThumbX, _x);
                    controller.SetAxisValue(Xbox360Axis.LeftThumbY, _y);
                    controller.SubmitReport();
                }
                Thread.Sleep(2); // 防止 CPU 占满
            }
        }

        public void Dispose()
        {
            running = false;
            Disconnect();
            client?.Dispose();
        }
    }

    internal class DualShock4Controller : IController
    {
        private ViGEmClient client;
        private IDualShock4Controller controller;
        private Settings settings;

        public bool isConnected { get; private set; }
        public bool running { get; private set; }

        private Thread updateThread;

        private byte _x = 127;
        private byte _y = 127;

        public void Initialize(Settings _settings, ViGEmClient _client)
        {
            settings = _settings;
            client = _client;
            isConnected = false;

            controller = client.CreateDualShock4Controller();
            Connect();

            running = true;
            updateThread = new Thread(_Update);
            updateThread.IsBackground = true;
            updateThread.Start();
        }

        public void Connect()
        {
            if (!isConnected)
            {
                controller.Connect();
                isConnected = true;
            }
        }

        public void Disconnect()
        {
            if (isConnected)
            {
                controller.Disconnect();
                isConnected = false;
            }
        }

        public void ChangeConnectionStatus()
        {
            if (isConnected)
                Disconnect();
            else
                Connect();
        }

        public void Update(double x, double y)
        {
            _x = Convert.ToByte(Math.Min(Math.Max((x + 1.0) * 128, 0), 255));
            _y = Convert.ToByte(Math.Min(Math.Max((y + 1.0) * 128, 0), 255));
        }

        private void _Update()
        {
            while (running)
            {
                if (isConnected)
                {
                    controller.SetAxisValue(DualShock4Axis.LeftThumbX, _x);
                    controller.SetAxisValue(DualShock4Axis.LeftThumbY, _y);
                    controller.SubmitReport();
                }
                Thread.Sleep(2);
            }
        }

        public void Dispose()
        {
            running = false;
            Disconnect();
            controller?.Dispose();
            client?.Dispose();
        }
    }
}
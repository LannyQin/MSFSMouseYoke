using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.DualShock4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            settings = _settings;
            client = new ViGEmClient();
            controller = client.CreateDualShock4Controller();
            Connect();

            running = true;
            updateThread = new Thread(new ThreadStart(_Update));
            updateThread.Start();
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

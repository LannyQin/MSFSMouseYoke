using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MSFSMouseYoke
{
    public partial class MouseYokeOverlay : Form
    {
        // Windows API 常量
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int LWA_ALPHA = 0x2;
        private const int GWL_EXSTYLE = -20;
        private const int WM_NCHITTEST = 0x84;
        private const int WM_MOUSEACTIVATE = 0x21;
        private const int MA_NOACTIVATE = 0x0003;
        private const int HTCLIENT = 1;
        private const int HTCAPTION = 2;
        private const int WM_NCLBUTTONDOWN = 0x00A1;
        private const int WM_NCRBUTTONDOWN = 0x00A4;
        private const int WM_NCMBUTTONDOWN = 0x00A7;

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ClipCursor(ref Rectangle lpRect);

        [DllImport("user32.dll")]
        private static extern bool ClipCursor(IntPtr lpRect);

        [DllImport("user32.dll")]
        private static extern bool GetClipCursor(ref Rectangle lpRect);


        // 设置
        Settings settings = new Settings();

        // 控制状态
        private bool mouseControlEnabled;
        private Point mouseDownPosition;
        private bool isDragging = false;
        private Point formStartPosition;
        private Timer controlTimer;
        private Point lastMousePosition;
        private Point beforePauseMousePosition;
        private Point centerPosition;
        public double x = 0;
        public double y = 0;

        // 中键相关功能
        private bool isMiddleMouseDown = false;
        private DateTime middleMouseDownTime;
        private Timer middleMouseTimer;
        private const int MiddleMouseLongPressThreshold = 500; // 长按时间阈值（毫秒）

        public MouseYokeOverlay()
        {
            InitializeComponent();
            SetupForm();
            SetupTimer();
        }

        private void SetupForm()
        {
            this.Text = "MSFS Mouse Yoke";
            this.Size = new Size(settings.window_width, settings.window_height);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.DoubleBuffered = true;
            //this.Icon = new Icon(new System.IO.MemoryStream(Properties.Resources.AppIcon));
            this.Icon = new Icon(GetType(), "MSFSMouseYoke.ico");
            mouseControlEnabled = settings.enable_when_start;

            // 设置窗口样式：分层、不激活
            int initialStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
            int newStyle = initialStyle | WS_EX_LAYERED | WS_EX_NOACTIVATE;
            SetWindowLong(this.Handle, GWL_EXSTYLE, newStyle);

            // 使用 SetLayeredWindowAttributes 设置整体透明度
            SetLayeredWindowAttributes(this.Handle, 0, 64, LWA_ALPHA); // 中等透明度

            // 计算中心位置
            centerPosition = new Point(this.Width / 2, this.Height / 2);
            lastMousePosition = centerPosition;
            beforePauseMousePosition = centerPosition;

            // 事件处理
            this.LocationChanged += OnLocationChanged;
            this.SizeChanged += OnSizeChanged;
            this.MouseDown += OnMouseDown;
            this.MouseMove += OnMouseMove;
            this.MouseUp += OnMouseUp;
            this.Paint += OnPaint;
            //this.KeyDown += OnKeyDown;

            // 初始化中键长按计时器
            middleMouseTimer = new Timer();
            middleMouseTimer.Interval = MiddleMouseLongPressThreshold;
            middleMouseTimer.Tick += OnMiddleMouseLongPress;

            // 激活控制器
            Controller.Initialize(settings);
        }

        private void EnableMouseLock()
        {
            if (mouseControlEnabled) return;

            mouseControlEnabled = true;

            // 锁定鼠标到窗口区域
            Rectangle clipRect = this.RectangleToScreen(this.ClientRectangle);
            ClipCursor(ref clipRect);

            UpdateVisualState();
            this.Invalidate();
        }

        private void DisableMouseLock()
        {
            if (!mouseControlEnabled) return;

            mouseControlEnabled = false;

            // 释放鼠标锁定
            ClipCursor(IntPtr.Zero);

            UpdateVisualState();
            this.Invalidate();
        }

        private void UpdateMouseLock()
        {
            if (mouseControlEnabled)
            {
                // 确保鼠标仍然被锁定在当前窗口区域
                Rectangle clipRect = this.RectangleToScreen(this.ClientRectangle);
                ClipCursor(ref clipRect);
            }
        }

        private void SetupTimer()
        {
            controlTimer = new Timer();
            controlTimer.Interval = 10;
            controlTimer.Tick += OnControlTimerTick;
            controlTimer.Start();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Controller.isConnected)
                {
                    // 左键点击切换鼠标控制状态
                    ChangeMouseEnableState();
                    UpdateVisualState();
                    this.Invalidate();
                }
                else
                {
                    mouseControlEnabled = false;
                    DisableMouseLock();
                }
            }
            else if ((settings.drag_button=="middle")?(e.Button == MouseButtons.Middle):(e.Button==MouseButtons.Right))
            {
                if (!mouseControlEnabled)
                {
                    // 右键开始拖动窗口
                    isDragging = true;
                    mouseDownPosition = Cursor.Position;
                    formStartPosition = this.Location;

                    // 捕获鼠标以便拖动
                    SetCapture(this.Handle);
                }
            }
            else if ((settings.drag_button == "middle") ? (e.Button == MouseButtons.Right) : (e.Button == MouseButtons.Middle))
            {
                // 中键按下
                isMiddleMouseDown = true;
                middleMouseDownTime = DateTime.Now;

                // 启动长按检测计时器
                middleMouseTimer.Start();
            }
        }

        private void ChangeMouseEnableState()
        {
            if (mouseControlEnabled)
            {
                mouseControlEnabled = false;
                DisableMouseLock();
            }
            else
            {
                mouseControlEnabled = true;
                EnableMouseLock();
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (((settings.drag_button == "middle") ? (e.Button == MouseButtons.Middle) : (e.Button == MouseButtons.Right)) && isDragging)
            {
                isDragging = false;
                ReleaseCapture();
            }
            else if (((settings.drag_button == "middle") ? (e.Button == MouseButtons.Right) : (e.Button == MouseButtons.Middle)) && isMiddleMouseDown)
            {
                isMiddleMouseDown = false;
                middleMouseTimer.Stop();

                // 计算按下时间
                TimeSpan pressDuration = DateTime.Now - middleMouseDownTime;

                if (pressDuration.TotalMilliseconds < MiddleMouseLongPressThreshold)
                {
                    // 短按：显示右键菜单
                    ShowContextMenu();
                }
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            // 更新鼠标位置用于控制计算和绘制光标十字
            lastMousePosition = e.Location;
            this.Invalidate();

            if (mouseControlEnabled)
            {
                beforePauseMousePosition = e.Location;
                UpdateMouseLock(); // 更新鼠标锁定状态
            }

            if (isDragging)
            {
                // 使用屏幕坐标计算移动距离，避免闪烁和位置偏移
                Point currentScreenPos = Cursor.Position;
                int deltaX = currentScreenPos.X - mouseDownPosition.X;
                int deltaY = currentScreenPos.Y - mouseDownPosition.Y;

                this.Location = new Point(formStartPosition.X + deltaX, formStartPosition.Y + deltaY);
            }

            // 更新光标显示
            this.Cursor = isDragging ? Cursors.SizeAll : Cursors.Default;
        }

        private void OnMiddleMouseLongPress(object sender, EventArgs e)
        {
            if (isMiddleMouseDown)
            {
                middleMouseTimer.Stop();
                isMiddleMouseDown = false;

                // 执行长按自定义代码
                ExecuteMiddleMouseLongPressAction();
            }
        }

        private void ShowContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // 添加菜单项
            ToolStripMenuItem toggleControlItem = new ToolStripMenuItem("切换控制状态");
            toggleControlItem.Click += (s, e) => ChangeMouseEnableState();

            ToolStripMenuItem toCenterItem = new ToolStripMenuItem("摇杆回中");
            toCenterItem.Click += (s, e) => ToCenter();

            ToolStripMenuItem connectStatusItem = new ToolStripMenuItem(Controller.isConnected?"断开摇杆":"连接摇杆");
            connectStatusItem.Click += (s, e) => ChangeConnectionStatus();

            ToolStripMenuItem settingsItem = new ToolStripMenuItem("设置");
            settingsItem.Click += (s, e) => ShowSettingsForm();

            ToolStripMenuItem closeItem = new ToolStripMenuItem("退出");
            closeItem.Click += (s, e) => Application.Exit();

            contextMenu.Items.Add(toggleControlItem);
            contextMenu.Items.Add(toCenterItem);
            contextMenu.Items.Add(connectStatusItem);
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add(settingsItem);
            contextMenu.Items.Add(closeItem);

            // 在鼠标位置显示菜单
            contextMenu.Show(Cursor.Position);
        }

        private void ChangeConnectionStatus()
        {
            Controller.ChangeConnectionStatus();
            ToCenter();
            mouseControlEnabled = false;
            DisableMouseLock();
        }
        private void ShowSettingsForm()
        {
            // 创建并显示设置窗体
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog(); // 使用 ShowDialog 使其成为模态对话框
        }

        // 中键长按自定义代码
        private void ExecuteMiddleMouseLongPressAction()
        {
            // 这里可以添加你的自定义代码
            // 例如：重置鼠标位置、切换模式等

            if (settings.on_middle_button_long_pressed == "exit")
            {
                Application.Exit();
            }
            else
            {
                ToCenter();
            }
        }

        private void ToCenter()
        {
            // 将鼠标重置到中心位置
            Point screenCenter = this.PointToScreen(centerPosition);
            Cursor.Position = screenCenter;
            lastMousePosition = centerPosition;
            beforePauseMousePosition = centerPosition;
            this.Invalidate();

            // 重置控制器输入
            Controller.Update(0, 0);
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 绘制半透明背景 - 使用适当的 Alpha 值
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(127, 0, 0, 0))) // Alpha=80
            {
                g.FillRectangle(bgBrush, this.ClientRectangle);
            }

            // 绘制边框和状态指示
            if (settings.border_width > 0)
            {
                using (Pen borderPen = new Pen((settings.has_bordel_color ? (Controller.isConnected ? (mouseControlEnabled ? Color.Lime : Color.Red) : Color.Yellow) : Color.Black), settings.border_width))
                {
                    g.DrawRectangle(borderPen, 1, 1, this.Width - settings.border_width, this.Height - settings.border_width);
                }
            }

            // 绘制十字线
            drawCross(g);

        }

        private void drawCross(Graphics g)
        {
            // 绘制中心十字线
            //绘制黑色外线
            using (Pen crossPen = new Pen(Color.Black, 5))
            {
                g.DrawLine(crossPen, centerPosition.X - (settings.cross_length / 2 + 1), centerPosition.Y, centerPosition.X + (settings.cross_length / 2 + 1), centerPosition.Y);
                g.DrawLine(crossPen, centerPosition.X, centerPosition.Y - (settings.cross_length / 2 + 1), centerPosition.X, centerPosition.Y + (settings.cross_length / 2 + 1));
            }
            //绘制白色内线
            using (Pen crossPen = new Pen(Color.White, 3))
            {
                g.DrawLine(crossPen, centerPosition.X - (settings.cross_length / 2 - 1), centerPosition.Y, centerPosition.X + (settings.cross_length / 2 - 1), centerPosition.Y);
                g.DrawLine(crossPen, centerPosition.X, centerPosition.Y - (settings.cross_length / 2 - 1), centerPosition.X, centerPosition.Y + (settings.cross_length / 2 - 1));
            }

            // 绘制鼠标十字线
            if (settings.cursor_cross_enabled)
            {
                using (Pen cursorCrossPen = new Pen(Color.Black, 3))
                {
                    g.DrawLine(cursorCrossPen, beforePauseMousePosition.X - (settings.cursor_cross_length / 2), beforePauseMousePosition.Y, beforePauseMousePosition.X + (settings.cursor_cross_length / 2), beforePauseMousePosition.Y);
                    g.DrawLine(cursorCrossPen, beforePauseMousePosition.X, beforePauseMousePosition.Y - (settings.cursor_cross_length / 2), beforePauseMousePosition.X, beforePauseMousePosition.Y + (settings.cursor_cross_length / 2));
                }
            }
        }

        private void OnControlTimerTick(object sender, EventArgs e)
        {
            if (mouseControlEnabled)
            {
                // 计算相对于中心的偏移量（用于俯仰和滚转控制）
                double pitch = (lastMousePosition.Y - centerPosition.Y) / (float)centerPosition.Y;
                double roll = (lastMousePosition.X - centerPosition.X) / (float)centerPosition.X;

                if (mouseControlEnabled)
                {
                    x = roll;
                    y = pitch;
                }

                // 限制在[-1, 1]范围内
                pitch = Math.Max(-1, Math.Min(1, pitch));
                roll = Math.Max(-1, Math.Min(1, roll));

                // 这里可以添加发送控制信号到微软模拟飞行的代码
                // 例如通过模拟输入、网络协议或内存写入等方式
                Console.WriteLine($"Pitch: {y:F4}, Roll: {x:F4}");
                Controller.Update(roll, pitch);
            }
        }

        private void OnLocationChanged(object sender, EventArgs e)
        {
            // 窗口移动时更新鼠标锁定区域
            if (mouseControlEnabled)
            {
                UpdateMouseLock();
            }
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            // 窗口大小改变时更新中心位置和鼠标锁定区域
            centerPosition = new Point(this.Width / 2, this.Height / 2);
            if (mouseControlEnabled)
            {
                UpdateMouseLock();
            }
        }

        private void UpdateVisualState()
        {
            this.Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_MOUSEACTIVATE:
                    // 防止窗口被激活
                    m.Result = (IntPtr)MA_NOACTIVATE;
                    return;

                case WM_NCHITTEST:
                    // 让整个窗口都能接收鼠标事件
                    base.WndProc(ref m);
                    if (m.Result == (IntPtr)HTCLIENT && !isDragging)
                    {
                        // 如果是客户区且不在拖动状态，返回HTCLIENT保持正常鼠标事件
                        m.Result = (IntPtr)HTCLIENT;
                    }
                    return;
            }
            base.WndProc(ref m);
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(settings.window_width, settings.window_height);
            this.Name = "MouseYokeOverlay";
            this.ResumeLayout(false);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MouseYokeOverlay());
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 确保释放鼠标锁定
            if (mouseControlEnabled)
            {
                ClipCursor(IntPtr.Zero);
            }

            // 停止并释放计时器
            if (middleMouseTimer != null)
            {
                middleMouseTimer.Stop();
                middleMouseTimer.Dispose();
            }

            // 关闭手柄
            Controller.Dispose();

            base.OnFormClosing(e);
        }
    }
}

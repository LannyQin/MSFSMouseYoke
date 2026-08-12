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

        private DateTime leftMouseDownTime;
        private Timer leftLongPressTimer;
        private Point rightMouseDownPosition = Point.Empty;   // 右键按下的起点
        private const int DragThreshold = 10;  // 显示菜单栏的最大拖动距离
        private int LongPressThreshold = 300; // 毫秒

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
            this.LongPressThreshold = settings.long_press_threshold;
            //this.KeyDown += OnKeyDown;

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
            // 更新鼠标位置
            controlTimer = new Timer();
            controlTimer.Interval = 10;
            controlTimer.Tick += OnControlTimerTick;
            controlTimer.Start();

            // 左键长按逻辑
            leftLongPressTimer = new Timer();
            leftLongPressTimer.Interval = LongPressThreshold;
            leftLongPressTimer.Tick += (s, e) =>
            {
                leftLongPressTimer.Stop();
                ToCenter();
            };
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                leftMouseDownTime = DateTime.Now;
                leftLongPressTimer.Start(); // 开始计时

                if (!Controller.isConnected)
                {
                    mouseControlEnabled = false;
                    DisableMouseLock();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (!mouseControlEnabled)
                {
                    // 非操控状态：可以拖窗口
                    isDragging = true;
                    mouseDownPosition = Cursor.Position;
                    formStartPosition = this.Location;
                    SetCapture(this.Handle);
                }

                rightMouseDownPosition = Cursor.Position; // 记录起点
            }
        }

        private void ChangeMouseEnableState()
        {
            if (mouseControlEnabled)
            {
                mouseControlEnabled = false;
                DisableMouseLock();
                Debug("Disabled");
            }
            else
            {
                mouseControlEnabled = true;
                EnableMouseLock();
                Debug("Enabled");
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            // ===== 左键 =====
            if (e.Button == MouseButtons.Left)
            {
                leftLongPressTimer.Stop(); // 没到时间就松手，取消回中

                TimeSpan pressDuration = DateTime.Now - leftMouseDownTime;
                if (pressDuration.TotalMilliseconds < LongPressThreshold && Controller.isConnected)
                {
                    // 只有短按才切换控制
                    ChangeMouseEnableState();
                }

                UpdateVisualState();
                this.Invalidate();
            }

            // ===== 右键 =====
            if (e.Button == MouseButtons.Right)
            {
                if (isDragging)
                {
                    isDragging = false;
                    ReleaseCapture();
                }

                if (rightMouseDownPosition == Point.Empty)  // 如果没有记录起点，直接返回，防止系统吞事件
                    return;

                Point currentPos = Cursor.Position;
                int deltaX = Math.Abs(currentPos.X - rightMouseDownPosition.X);
                int deltaY = Math.Abs(currentPos.Y - rightMouseDownPosition.Y);

                // 只看移动距离，不看时间
                if (deltaX <= DragThreshold && deltaY <= DragThreshold)
                {
                    ShowContextMenu(); // 没怎么动 → 菜单
                }
                // 动了 → 什么都不做（已经是拖窗口）
            }

        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            lastMousePosition = e.Location;
            this.Invalidate();

            if (mouseControlEnabled)
            {
                beforePauseMousePosition = e.Location;
                UpdateMouseLock();
            }

            if (isDragging)
            {
                Point currentScreenPos = Cursor.Position;
                int deltaX = currentScreenPos.X - mouseDownPosition.X;
                int deltaY = currentScreenPos.Y - mouseDownPosition.Y;
                this.Location = new Point(formStartPosition.X + deltaX, formStartPosition.Y + deltaY);
            }

            this.Cursor = isDragging ? Cursors.SizeAll : Cursors.Default;
        }


        private void ShowContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // 添加菜单项
            ToolStripMenuItem toggleControlItem = new ToolStripMenuItem("切换控制状态");
            toggleControlItem.Click += (s, e) => ChangeMouseEnableState();

            ToolStripMenuItem toCenterItem = new ToolStripMenuItem("摇杆回中");
            toCenterItem.Click += (s, e) => ToCenter();

            ToolStripMenuItem connectStatusItem = new ToolStripMenuItem(Controller.isConnected ? "断开摇杆" : "连接摇杆");
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
            ToCenter();
            Controller.ChangeConnectionStatus();
            Debug(Controller.isConnected ? "Connected" : "Disconnected");
            mouseControlEnabled = false;
            DisableMouseLock();
        }
        private void ShowSettingsForm()
        {
            // 创建并显示设置窗体

            // 暂时取消置顶以便操作设置界面
            this.TopMost = false;

            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog(); // 使用 ShowDialog 使其成为模态对话框

            // 恢复置顶
            this.TopMost = true;
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
            Debug("Centered");
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


                Debug($"Pitch: {y:F4}, Roll: {x:F4}");
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

        private void Debug(string message)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] {message}");
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

            // 关闭手柄
            Controller.Dispose();

            base.OnFormClosing(e);
        }
    }
}

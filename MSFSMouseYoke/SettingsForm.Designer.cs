namespace MSFSMouseYoke
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.windowWidthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.windowHeightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.borderHasColorCheckBox = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.borderWidthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.centerLengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.mouseCrossLengthLabel = new System.Windows.Forms.Label();
            this.showMouseCrossCheckBox = new System.Windows.Forms.CheckBox();
            this.mouseCrossLengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.controlWhenStartCheckBox = new System.Windows.Forms.CheckBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.resetButton = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.controllerTypeComboBox = new System.Windows.Forms.ComboBox();
            this.controllerTypeLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.appearance = new System.Windows.Forms.TabPage();
            this.control = new System.Windows.Forms.TabPage();
            this.advanced = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.longPressNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.longPressLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.windowWidthNumericUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.windowHeightNumericUpDown)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.borderWidthNumericUpDown)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.centerLengthNumericUpDown)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mouseCrossLengthNumericUpDown)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.appearance.SuspendLayout();
            this.control.SuspendLayout();
            this.advanced.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.longPressNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "窗口大小";
            // 
            // windowWidthNumericUpDown
            // 
            this.windowWidthNumericUpDown.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.windowWidthNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.windowWidthNumericUpDown.Location = new System.Drawing.Point(115, 1);
            this.windowWidthNumericUpDown.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.windowWidthNumericUpDown.Minimum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.windowWidthNumericUpDown.Name = "windowWidthNumericUpDown";
            this.windowWidthNumericUpDown.Size = new System.Drawing.Size(57, 23);
            this.windowWidthNumericUpDown.TabIndex = 1;
            this.windowWidthNumericUpDown.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.windowHeightNumericUpDown);
            this.panel1.Controls.Add(this.windowWidthNumericUpDown);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 30);
            this.panel1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(184, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "高：";
            // 
            // windowHeightNumericUpDown
            // 
            this.windowHeightNumericUpDown.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.windowHeightNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.windowHeightNumericUpDown.Location = new System.Drawing.Point(220, 1);
            this.windowHeightNumericUpDown.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.windowHeightNumericUpDown.Minimum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.windowHeightNumericUpDown.Name = "windowHeightNumericUpDown";
            this.windowHeightNumericUpDown.Size = new System.Drawing.Size(57, 23);
            this.windowHeightNumericUpDown.TabIndex = 3;
            this.windowHeightNumericUpDown.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(78, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "宽：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.borderHasColorCheckBox);
            this.panel2.Location = new System.Drawing.Point(6, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(288, 30);
            this.panel2.TabIndex = 3;
            // 
            // borderHasColorCheckBox
            // 
            this.borderHasColorCheckBox.AutoSize = true;
            this.borderHasColorCheckBox.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.borderHasColorCheckBox.Location = new System.Drawing.Point(6, 4);
            this.borderHasColorCheckBox.Name = "borderHasColorCheckBox";
            this.borderHasColorCheckBox.Size = new System.Drawing.Size(96, 18);
            this.borderHasColorCheckBox.TabIndex = 0;
            this.borderHasColorCheckBox.Text = "边框有颜色";
            this.borderHasColorCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.borderWidthNumericUpDown);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(6, 78);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(288, 30);
            this.panel3.TabIndex = 4;
            // 
            // borderWidthNumericUpDown
            // 
            this.borderWidthNumericUpDown.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.borderWidthNumericUpDown.Location = new System.Drawing.Point(72, 1);
            this.borderWidthNumericUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.borderWidthNumericUpDown.Name = "borderWidthNumericUpDown";
            this.borderWidthNumericUpDown.Size = new System.Drawing.Size(57, 23);
            this.borderWidthNumericUpDown.TabIndex = 2;
            this.borderWidthNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(3, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "边框宽度";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.centerLengthNumericUpDown);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(6, 114);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(288, 30);
            this.panel4.TabIndex = 5;
            // 
            // centerLengthNumericUpDown
            // 
            this.centerLengthNumericUpDown.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.centerLengthNumericUpDown.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.centerLengthNumericUpDown.Location = new System.Drawing.Point(114, 1);
            this.centerLengthNumericUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.centerLengthNumericUpDown.Minimum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.centerLengthNumericUpDown.Name = "centerLengthNumericUpDown";
            this.centerLengthNumericUpDown.Size = new System.Drawing.Size(57, 23);
            this.centerLengthNumericUpDown.TabIndex = 2;
            this.centerLengthNumericUpDown.Value = new decimal(new int[] {
            22,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(3, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "中心十字架长度";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.mouseCrossLengthLabel);
            this.panel5.Controls.Add(this.showMouseCrossCheckBox);
            this.panel5.Controls.Add(this.mouseCrossLengthNumericUpDown);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Location = new System.Drawing.Point(6, 150);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(288, 30);
            this.panel5.TabIndex = 6;
            // 
            // mouseCrossLengthLabel
            // 
            this.mouseCrossLengthLabel.AutoSize = true;
            this.mouseCrossLengthLabel.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mouseCrossLengthLabel.Location = new System.Drawing.Point(146, 6);
            this.mouseCrossLengthLabel.Name = "mouseCrossLengthLabel";
            this.mouseCrossLengthLabel.Size = new System.Drawing.Size(35, 14);
            this.mouseCrossLengthLabel.TabIndex = 4;
            this.mouseCrossLengthLabel.Text = "长度";
            // 
            // showMouseCrossCheckBox
            // 
            this.showMouseCrossCheckBox.AutoSize = true;
            this.showMouseCrossCheckBox.Font = new System.Drawing.Font("宋体", 10F);
            this.showMouseCrossCheckBox.Location = new System.Drawing.Point(86, 5);
            this.showMouseCrossCheckBox.Name = "showMouseCrossCheckBox";
            this.showMouseCrossCheckBox.Size = new System.Drawing.Size(54, 18);
            this.showMouseCrossCheckBox.TabIndex = 3;
            this.showMouseCrossCheckBox.Text = "显示";
            this.showMouseCrossCheckBox.UseVisualStyleBackColor = true;
            this.showMouseCrossCheckBox.CheckedChanged += new System.EventHandler(this.showMouseCrossCheckBox_CheckedChanged);
            // 
            // mouseCrossLengthNumericUpDown
            // 
            this.mouseCrossLengthNumericUpDown.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mouseCrossLengthNumericUpDown.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.mouseCrossLengthNumericUpDown.Location = new System.Drawing.Point(187, 2);
            this.mouseCrossLengthNumericUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.mouseCrossLengthNumericUpDown.Minimum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.mouseCrossLengthNumericUpDown.Name = "mouseCrossLengthNumericUpDown";
            this.mouseCrossLengthNumericUpDown.Size = new System.Drawing.Size(57, 23);
            this.mouseCrossLengthNumericUpDown.TabIndex = 2;
            this.mouseCrossLengthNumericUpDown.Value = new decimal(new int[] {
            22,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(3, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "鼠标十字架";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.controlWhenStartCheckBox);
            this.panel6.Location = new System.Drawing.Point(6, 6);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(288, 30);
            this.panel6.TabIndex = 4;
            // 
            // controlWhenStartCheckBox
            // 
            this.controlWhenStartCheckBox.AutoSize = true;
            this.controlWhenStartCheckBox.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.controlWhenStartCheckBox.Location = new System.Drawing.Point(6, 3);
            this.controlWhenStartCheckBox.Name = "controlWhenStartCheckBox";
            this.controlWhenStartCheckBox.Size = new System.Drawing.Size(124, 18);
            this.controlWhenStartCheckBox.TabIndex = 0;
            this.controlWhenStartCheckBox.Text = "启动时开始控制";
            this.controlWhenStartCheckBox.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("宋体", 10F);
            this.OKButton.Location = new System.Drawing.Point(4, 3);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(65, 28);
            this.OKButton.TabIndex = 7;
            this.OKButton.Text = "确定";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("宋体", 10F);
            this.cancelButton.Location = new System.Drawing.Point(75, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(65, 28);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.OKButton);
            this.panel8.Controls.Add(this.cancelButton);
            this.panel8.Location = new System.Drawing.Point(84, 310);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(144, 34);
            this.panel8.TabIndex = 7;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.resetButton);
            this.panel9.Location = new System.Drawing.Point(3, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(288, 30);
            this.panel9.TabIndex = 9;
            // 
            // resetButton
            // 
            this.resetButton.Font = new System.Drawing.Font("宋体", 10F);
            this.resetButton.Location = new System.Drawing.Point(3, 1);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(110, 28);
            this.resetButton.TabIndex = 7;
            this.resetButton.Text = "重置所有设置";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.controllerTypeComboBox);
            this.panel10.Controls.Add(this.controllerTypeLabel);
            this.panel10.Location = new System.Drawing.Point(6, 42);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(288, 30);
            this.panel10.TabIndex = 7;
            // 
            // controllerTypeComboBox
            // 
            this.controllerTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.controllerTypeComboBox.FormattingEnabled = true;
            this.controllerTypeComboBox.Items.AddRange(new object[] {
            "Xbox360",
            "Dualshock4（可能有兼容性问题）"});
            this.controllerTypeComboBox.Location = new System.Drawing.Point(86, 3);
            this.controllerTypeComboBox.Name = "controllerTypeComboBox";
            this.controllerTypeComboBox.Size = new System.Drawing.Size(199, 20);
            this.controllerTypeComboBox.TabIndex = 1;
            this.controllerTypeComboBox.Tag = "";
            // 
            // controllerTypeLabel
            // 
            this.controllerTypeLabel.AutoSize = true;
            this.controllerTypeLabel.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.controllerTypeLabel.Location = new System.Drawing.Point(3, 5);
            this.controllerTypeLabel.Name = "controllerTypeLabel";
            this.controllerTypeLabel.Size = new System.Drawing.Size(77, 14);
            this.controllerTypeLabel.TabIndex = 0;
            this.controllerTypeLabel.Text = "控制器类型";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.appearance);
            this.tabControl1.Controls.Add(this.control);
            this.tabControl1.Controls.Add(this.advanced);
            this.tabControl1.Location = new System.Drawing.Point(3, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(317, 311);
            this.tabControl1.TabIndex = 10;
            // 
            // appearance
            // 
            this.appearance.Controls.Add(this.panel1);
            this.appearance.Controls.Add(this.panel2);
            this.appearance.Controls.Add(this.panel3);
            this.appearance.Controls.Add(this.panel4);
            this.appearance.Controls.Add(this.panel5);
            this.appearance.Location = new System.Drawing.Point(4, 22);
            this.appearance.Name = "appearance";
            this.appearance.Padding = new System.Windows.Forms.Padding(3);
            this.appearance.Size = new System.Drawing.Size(309, 285);
            this.appearance.TabIndex = 0;
            this.appearance.Text = "外观";
            // 
            // control
            // 
            this.control.Controls.Add(this.panel7);
            this.control.Controls.Add(this.panel6);
            this.control.Controls.Add(this.panel10);
            this.control.Location = new System.Drawing.Point(4, 22);
            this.control.Name = "control";
            this.control.Padding = new System.Windows.Forms.Padding(3);
            this.control.Size = new System.Drawing.Size(309, 285);
            this.control.TabIndex = 1;
            this.control.Text = "控制";
            // 
            // advanced
            // 
            this.advanced.Controls.Add(this.panel9);
            this.advanced.Location = new System.Drawing.Point(4, 22);
            this.advanced.Name = "advanced";
            this.advanced.Size = new System.Drawing.Size(309, 285);
            this.advanced.TabIndex = 2;
            this.advanced.Text = "高级";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.longPressNumericUpDown);
            this.panel7.Controls.Add(this.longPressLabel);
            this.panel7.Location = new System.Drawing.Point(6, 78);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(288, 30);
            this.panel7.TabIndex = 8;
            // 
            // longPressNumericUpDown
            // 
            this.longPressNumericUpDown.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.longPressNumericUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.longPressNumericUpDown.Location = new System.Drawing.Point(117, 1);
            this.longPressNumericUpDown.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.longPressNumericUpDown.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.longPressNumericUpDown.Name = "longPressNumericUpDown";
            this.longPressNumericUpDown.Size = new System.Drawing.Size(57, 23);
            this.longPressNumericUpDown.TabIndex = 2;
            this.longPressNumericUpDown.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // longPressLabel
            // 
            this.longPressLabel.AutoSize = true;
            this.longPressLabel.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.longPressLabel.Location = new System.Drawing.Point(3, 5);
            this.longPressLabel.Name = "longPressLabel";
            this.longPressLabel.Size = new System.Drawing.Size(119, 14);
            this.longPressLabel.TabIndex = 0;
            this.longPressLabel.Text = "长按时间（毫秒）";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 351);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(338, 390);
            this.MinimumSize = new System.Drawing.Size(338, 390);
            this.Name = "SettingsForm";
            this.Text = "设置";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.windowWidthNumericUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.windowHeightNumericUpDown)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.borderWidthNumericUpDown)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.centerLengthNumericUpDown)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mouseCrossLengthNumericUpDown)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.appearance.ResumeLayout(false);
            this.control.ResumeLayout(false);
            this.advanced.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.longPressNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown windowWidthNumericUpDown;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown windowHeightNumericUpDown;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox borderHasColorCheckBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown borderWidthNumericUpDown;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.NumericUpDown centerLengthNumericUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox showMouseCrossCheckBox;
        private System.Windows.Forms.NumericUpDown mouseCrossLengthNumericUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label mouseCrossLengthLabel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.CheckBox controlWhenStartCheckBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.ComboBox controllerTypeComboBox;
        private System.Windows.Forms.Label controllerTypeLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage appearance;
        private System.Windows.Forms.TabPage control;
        private System.Windows.Forms.TabPage advanced;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.NumericUpDown longPressNumericUpDown;
        private System.Windows.Forms.Label longPressLabel;
    }
}
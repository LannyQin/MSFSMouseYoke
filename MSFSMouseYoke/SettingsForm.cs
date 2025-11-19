using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSFSMouseYoke
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            this.Load += SettingsForm_Load; // 加载时读取配置
            this.OKButton.Click += OKButton_Click; // 保存按钮点击事件

            // 控件提示
            this.toolTip.SetToolTip(this.dragButtonComboBox, "当选择右键时，中键显示弹出菜单，右键拖动\n当选择左键时，右键显示弹出菜单，中键拖动");
            this.toolTip.SetToolTip(this.dragButtonLabel, "当选择右键时，中键显示弹出菜单，右键拖动\n当选择左键时，右键显示弹出菜单，中键拖动");
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // 从配置文件加载设置
            LoadSettings();
        }

        private void LoadSettings()
        {
            // 主窗口尺寸
            windowWidthNumericUpDown.Value = Settings.Default.window_width;
            windowHeightNumericUpDown.Value = Settings.Default.window_height;

            // 边框设置
            borderHasColorCheckBox.Checked = Settings.Default.has_bordel_color;
            borderWidthNumericUpDown.Value = Settings.Default.border_width;

            // 十字线设置
            centerLengthNumericUpDown.Value = Settings.Default.cross_length;
            mouseCrossLengthNumericUpDown.Value = Settings.Default.cursor_cross_length;
            showMouseCrossCheckBox.Checked = Settings.Default.cursor_cross_enabled;

            // 启动设置
            controlWhenStartCheckBox.Checked = Settings.Default.enable_when_start;

            // 中键长按行为
            string longPressAction = Settings.Default.on_middle_button_long_pressed;
            object selectedItem;
            switch (longPressAction)
            {
                case "exit":
                    selectedItem = longPressComboBox.Items[1];
                    break;
                case "to_center":
                default:
                    selectedItem = longPressComboBox.Items[0];
                    break;
            }
            longPressComboBox.SelectedItem = selectedItem;

            // 拖动键
            string dragAction = Settings.Default.drag_button;
            object selectedItem2;
            switch (dragAction)
            {
                case "middle":
                    selectedItem2 = dragButtonComboBox.Items[1];
                    break;
                case "right":
                default:
                    selectedItem2 = dragButtonComboBox.Items[0];
                    break;
            }
            dragButtonComboBox.SelectedItem = selectedItem2;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            // 保存到配置文件
            SaveSettings();
            DialogResult result = MessageBox.Show("设置已保存，下次启动时生效。\n是否立即重新启动", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void SaveSettings()
        {
            // 主窗口尺寸
            Settings.Default.window_width = (int)windowWidthNumericUpDown.Value;
            Settings.Default.window_height = (int)windowHeightNumericUpDown.Value;

            // 边框设置
            Settings.Default.has_bordel_color = borderHasColorCheckBox.Checked;
            Settings.Default.border_width = (int)borderWidthNumericUpDown.Value;

            // 十字线设置
            Settings.Default.cross_length = (int)centerLengthNumericUpDown.Value;
            Settings.Default.cursor_cross_length = (int)mouseCrossLengthNumericUpDown.Value;
            Settings.Default.cursor_cross_enabled = showMouseCrossCheckBox.Checked;

            // 启动设置
            Settings.Default.enable_when_start = controlWhenStartCheckBox.Checked;

            // 中键长按行为
            Settings.Default.on_middle_button_long_pressed =
                longPressComboBox.SelectedIndex == 1 ? "exit" : "to_center";

            // 拖动键
            Settings.Default.drag_button =
                dragButtonComboBox.SelectedIndex == 1 ? "middle" : "right";

            Settings.Default.Save(); // 持久化保存
        }

        private void showMouseCrossCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.showMouseCrossCheckBox.Checked)
            {
                this.mouseCrossLengthLabel.Enabled = true;
                this.mouseCrossLengthNumericUpDown.Enabled = true;
            }
            else
            {
                this.mouseCrossLengthLabel.Enabled = false;
                this.mouseCrossLengthNumericUpDown.Enabled = false;
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            // 重置为默认值
            windowWidthNumericUpDown.Value = 200;                    // window_width 默认 200
            windowHeightNumericUpDown.Value = 200;                   // window_height 默认 200

            borderHasColorCheckBox.Checked = true;                   // has_bordel_color 默认 true
            borderWidthNumericUpDown.Value = 2;                      // border_width 默认 2

            centerLengthNumericUpDown.Value = 20;                    // cross_length 默认 20
            mouseCrossLengthNumericUpDown.Value = 18;                // cursor_cross_length 默认 18
            showMouseCrossCheckBox.Checked = true;                   // cursor_cross_enabled 默认 true

            controlWhenStartCheckBox.Checked = false;                // enable_when_start 默认 false

            longPressComboBox.SelectedIndex = 1;                     // on_middle_button_long_pressed 默认 "exit"（索引1）
            dragButtonComboBox.SelectedIndex = 0;                    // drag_button 默认 "right"（索引0）
        }
    }
}

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
            this.cancelButton.Click += CancelButton_Click; // 取消按钮点击事件
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

            // 控制器类型
            string controllerType = Settings.Default.controller_type;
            object controllerTypeSelectedItem;
            switch (controllerType)
            {
                case "dualshock4":
                    controllerTypeSelectedItem = controllerTypeComboBox.Items[1];
                    break;
                case "xbox360":
                default:
                    controllerTypeSelectedItem = controllerTypeComboBox.Items[0];
                    break;
            }
            controllerTypeComboBox.SelectedItem = controllerTypeSelectedItem;

            // 长按时间
            longPressNumericUpDown.Value = Settings.Default.long_press_threshold;
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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

            // 控制器类型
            Settings.Default.controller_type =
                controllerTypeComboBox.SelectedIndex == 1 ? "dualshock4" : "xbox360";

            // 长按时间
            Settings.Default.long_press_threshold = (int)longPressNumericUpDown.Value;

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
            controllerTypeComboBox.SelectedIndex = 0;                // controller_type 默认 "xbox360"（索引0）
            longPressNumericUpDown.Value = 300;                      // long_press_threshold 默认 300
        }
    }
}

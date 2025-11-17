# MSFSMouseYoke


## 项目背景
相信很多 Microsoft Flight Simulator 玩家都因其垃圾的键盘控制方式而感到苦恼。作为一名X-Plane玩家，我已习惯使用鼠标而非键盘操控飞机，X-Plane的操控方式令我感到方便而又舒服。

我知道市面上也有一款 *付费* 软件可以解决此问题，但我希望能有一个 **免费开源** 的替代品。因此，我决定开发 MSFSMouseYoke 项目。

本项目完全由我个人开发和维护，并承诺 **完全免费和开源** ，以便 **任何人** 都能使用和贡献。

## 功能介绍
MSFSMouseYoke 允许玩家使用鼠标来操控 Microsoft Flight Simulator 中的飞机，就如X-Plane中的鼠标操控方式一样。主要功能包括：
- 鼠标移动控制飞机的俯仰和滚转
- 鼠标左键可以开关控制（即锁住操作杆和解锁）
- 鼠标右键可拖动窗口
- 长按鼠标中键以退出程序
- 可以在设置中个性化调整软件体验

## 使用说明
1. 下载并安装 ViGEmBus 驱动程序：[ViGEmBus Releases](https://github.com/nefarius/ViGEmBus/releases)
2. 从 [Release页面](https://github.com/LannyQin/MSFSMouseYoke/releases) 下载最新版本的 MSFSMouseYoke 可执行文件。
3. 运行Microsoft Flight Simulator，您应该能够看到有手柄接入的提示，同意即可。
4. 尽情地飞行吧！

## 技术原理
- 核心技术基于 ViGEmBus，模拟一个虚拟的游戏手柄设备。
- 创建半透明的窗口，截获鼠标操作并将其转换为手柄输入信号。
- 键盘事件能够穿透并应用到 Microsoft Flight Simulator 里，以便进一步操纵飞机。

## 注意事项
1. 请确保在使用本软件前已正确安装 ViGEmBus 驱动程序。
2. 本软件仅在 Windows 操作系统上运行。
3. 本项目还在不断完善中，难免会遇到一些bug，欢迎大家提出建议和反馈。
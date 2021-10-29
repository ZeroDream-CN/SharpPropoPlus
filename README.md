# ♯PropoPlus
♯PropoPlus（又名 SharpPropoPlus）是一个基于 C# 开发的 SmartPropoPlus (by Shaul Eizikovich) 的替代品

此软件可以将您的 R/C 遥控器信号转换为虚拟手柄信号，并用于模拟器或者游戏用途。

## 使用方法

您可以点击观看 [Bilibili 的视频教程](https://www.bilibili.com/video/BV1vU4y1u7ok) 了解如何使用此软件。

这是一个开源的免费软件，允许您将 R/C 遥控器（或发射器）转换为虚拟手柄，用于游玩各种模拟游戏。

您需要将 R/C 遥控器通过音频线连接到电脑。为此您需要准备一条公对公的 3.5mm 音频线，其中一端接入到您的 R/C 遥控器的信号输出口（通常在遥控器侧面），然后将另一端接到您的电脑音频输入或麦克风输入口。

## 界面预览
<details>
  <summary>点击展开图片</summary>
  
![音频设置](https://user-images.githubusercontent.com/34357771/139359559-78bcddc2-9062-4283-b317-a396ecd4b3ef.png)
![解码设置](https://user-images.githubusercontent.com/34357771/139359605-a242a0a0-3d53-4c97-a0b8-4f54ba8bd186.png)
![过滤器设置](https://user-images.githubusercontent.com/34357771/139359640-f48e6f29-a3ca-4c56-9a6e-f2f6de6cc357.png)
![高级设置](https://user-images.githubusercontent.com/34357771/139359673-1840b544-2f55-4893-99ab-3a30ee135711.png)

</details>

## 中文汉化修改版本
您当前正在浏览的仓库为中文翻译版本的 SharpPropoPlus，此版本除了将界面汉化外，还增加了单独针对赛车竞速类游戏优化的 Filter（过滤器）。目前添加的 Filter 如下：

- RadioLink（通用车控）- 适合用于 Forza Horizon / Snow Runner 等油门刹车分开通道控制的赛车游戏
- Handbrake（手刹模式）- 增加手刹功能，轻按刹车键可以触发第 6 通道，可用于手刹，重踩刹车则取消手刹，适用于漂移。
- Handbrake（仅限手刹）- 由于部分游戏绑定键位时会发生手刹和刹车抢占情况，特别添加一个仅限手刹模式，仅手刹触发操作。

## 更多关于 SmartPropoPlus
SmartPropoPlus 站点主页：http://www.smartpropoplus.com/

SmartPropoPlus 仓库地址：https://github.com/shauleiz/SmartPropoPlus)

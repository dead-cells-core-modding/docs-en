---
sidebar_position: 1
---

# 安装 MDK

**MDK (Mod Development Kit)** 是 DCCM 提供的 Mod 开发工具包。本教程将指导你完成 MDK 的安装和基础配置

## 先决条件

- **.NET 9 SDK** ([下载地址](https://dotnet.microsoft.com/zh-cn/download/dotnet/9.0))
  - （可选） Visual Studio 2022
- [DCCM 核心文件](/docs/tutorial/install-core)

## 安装步骤

### 运行 MDK 安装脚本

- 打开 **文件管理器**，进入游戏根目录下的 `coremod/core/mdk` 文件夹
- 右键**使用 Powershell 运行** 运行 `install.ps1` PowerShell 脚本

## 验证 MDK 安装

在 PowerShell 或命令提示符中执行以下命令：

```bash
dotnet nuget list source
```

你应该能在输出结果中看到类似以下内容：

```text
Registered Sources:
  1.  DeadCoreModdingMDK [Enabled]
```

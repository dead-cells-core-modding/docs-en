---
sidebar_position: 1
---

# 安装 DCCM 核心

## 先决条件

- **.NET 9 运行时**
  - 如果你打算开发用于 **DCCM** 的 **Mods** ， 请安装 **.NET 9 SDK**。
- **Microsoft Visual C++ 可再发行组件包 (2015-2022)**

## 安装

### 获取DCCM核心

你可以从 [Github Releases](https://github.com/dead-cells-core-modding/core/releases/latest) 页面下载最新的DCCM核心。

如果你希望使用尚未正式发布的新功能，[这个](https://nightly.link/dead-cells-core-modding/core/workflows/build/dev)可能更适合你。

### 安装 DCCM 核心文件

从文件管理器中打开游戏根目录，创造一个名为 `coremod` 的文件夹。将上一步获取的DCCM核心文件解压至文件夹中。

:::tip
完成以上操作后，目录结构应该像这样：

```txt
<DeadCellsGameRoot>
├─ coremod
│  ├─ core
│  │  ├─ native
│  │  │  └─ …
│  │  ├─ mdk
│  │  │  ├─ install.ps1
│  │  │  ├─ uninstall.ps1
│  │  │  └─ …
│  │  ├─ host
│  │  │  ├─ startup
│  │  │  │  ├─ DeadCellsModding.exe
│  │  │  │  └─ …
│  │  │  └─ …
│  │  └─ …
│  └─ …
├─ deadcells.exe
├─ deadcells_gl.exe
└─ …
```

:::

:::tip
如果你愿意，你可以把 `DeadCellsModding.exe` 文件复制到游戏根目录下。
:::

## 启动游戏

当完成以上操作后，就可以通过 `DeadCellsModding.exe` 启动修改后的游戏。

:::warning
首次安装或更新后，**首次启动**可能需要较长时间生成缓存（内存占用较高）。
:::

如果一切顺利，那么游戏主菜单左下角将会显示 **DCCM** 的版本号。

![DCCM](img/{D0E4CA71-3773-4DED-9C08-A8ABF9B6E9D9}.png)

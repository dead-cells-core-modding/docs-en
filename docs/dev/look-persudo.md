---
sidebar_position: 100
---

# 查看游戏代码

本教程将指导你游戏代码流程。

:::info

**Dead Cells** 使用 **Haxe** 编写并运行在 **HashLink 虚拟机**上，而不是 **.NET 虚拟机**。

:::

:::tip

在开始之前，建议阅读 [wiki](https://github.com/HaxeFoundation/hashlink/wiki) 了解 **HashLink 虚拟机** 的基本信息。

:::

## 使用 crashlink

详见 [crashlink 文档](https://n3rdl0rd.github.io/crashlink/)。

## 使用 hlbc-gui

详见 [README](https://github.com/Gui-Yom/hlbc)。

## 使用 GamePseudocode

:::warning

- **GamePseudocode** 是由 **Dead Cells** 的 **HashLink Bytecode** 转换而来。仅仅用于查看游戏代码流程。
- 为了反编译结果的可读性， **GamePseudocode** 与 **HashLink Bytecode** 并非一一对应关系。
- **GamePseudocode** 的结果可能是错误的，请结合 [crashlink](#使用-crashlink) 或 [hlbc](#使用-hlbc-gui) 使用。

:::

### 修改配置文件

:::tip

配置文件的位置：`coremod/config/modcore.json`

初次启动后该文件会自动生成。

:::

- 打开配置文件，找到 `GeneratePseudocodeAssembly` 将其值修改为 `true`。
- 保存配置文件。

### 启动游戏

- 启动游戏，游戏会自动生成 **GamePseudocode** ，请耐心等待。
- 等待进入主菜单后，关闭游戏。

### 打开 GamePseudocode

- 进入目录 `coremod/cache` 应该可以发现 `GamePseudocode.dll` 文件。
  - 如果不存在，请确认操作是否正确。
- 使用你喜欢的 **.NET 反编译器** 打开 `GamePseudocode.dll`。
  - 推荐使用 [DnSpy](https://github.com/dnSpyEx/dnSpy)。

---
sidebar_position: 2
---

# 安装 Mods

本文以 **SampleHook** Mod为例，介绍 **Mods** 的安装方法。

:::info
本文所提到的 **Mods 目录** 的路径为`<DeadCellsGameRoot>/coremod/mods`。
:::

## 获取 Mods

你可以从任何你喜欢的渠道获取 **Mods**。

:::tip
对于任何一个**有效**的 Mod ，其根目录下都应该存在`modinfo.json`。
例如

```txt
SampleHook
├─ modinfo.json
├─ SampleHook.dll
└─ SampleHook.pdb
```

:::

## 复制 Mods 文件

将 Mod 文件夹复制到 **Mods 目录** 下。

完成上述操作后，目录结构应该类似于：

```txt
<DeadCellsGameRoot>
├─ coremod
│  ├─ mods
│  │  ├─ SampleHook
|  |  |  ├─ modinfo.json
|  |  |  └─ …
|  |  └─ …
|  └─ …
└─ …
```

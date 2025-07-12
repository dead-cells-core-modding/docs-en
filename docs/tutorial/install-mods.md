---
sidebar_position: 2
---

# 安装 Mods

本教程将指导你如何将 Mods 安装到游戏中

本文以 **SampleHook** Mod为例，介绍 **Mods** 的安装方法

:::info
本文所提到的 **Mods 目录** 的路径为`<DeadCellsGameRoot>/coremod/mods`
:::

## 获取 Mods

你可以从任何你喜欢的渠道获取 **Mods**

:::tip
对于任何一个**有效**的 Mod ，其根目录下都应该存在`modinfo.json`
例如

```txt
SampleHook
├─ modinfo.json
├─ SampleHook.dll
└─ SampleHook.pdb
```

:::

## 复制 Mods 文件

将 Mod 文件夹复制到 **Mods 目录** 下

:::tip

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

:::

## 常见问题

### Q1: Mod 无法加载

- 确认 `modinfo.json` 文件格式正确（可使用 JSON 验证工具）
- 查看游戏日志中的错误信息

### Q2: Mod 加载后无效果

- 查看游戏日志是否有警告或错误

### Q3: 多个 Mod 冲突

- 检查 Mod 依赖关系
- 尝试逐个启用 Mod 进行排查
- 查看游戏日志是否有警告或错误

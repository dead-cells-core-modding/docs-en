---
sidebar_position: 2
---

# 创造第一个 Mod

本教程将指导你创建一个简单的 Mod ，实现修改游戏内某个基础功能的功能。

:::warning

虽然编写 **Mod** 时使用 **C#** ，但请记住 **Dead Cells** 使用 **Haxe** 编写并运行在 **HashLink 虚拟机**上，而不是 **.NET 虚拟机**。

本教程假设你拥有以下技能：

- C# 编程基础
- Dead Cells 基础 Mod 制作 ([教程](https://www.bilibili.com/opus/681293864647000128))

:::

:::tip

在开始之前，建议阅读 [wiki](https://github.com/HaxeFoundation/hashlink/wiki) 了解 **HashLink 虚拟机** 的基本信息。

:::

:::info
本教程的 Mod 代码储存在 [Github](https://github.com/dead-cells-core-modding/docs-zh/blob/main/modproject/FirstDeadCellsMod) 上。
:::

## 创建 Mod 项目

- 打开命令行工具
- 创建一个新的库项目：

```bash
dotnet new classlib -n FirstDeadCellsMod -f net9.0
```

- 进入项目目录：

```bash
cd FirstDeadCellsMod
```

- 添加 Dead Cells Modding MDK 的NuGet包引用：

```bash
dotnet add package DeadCellsCoreModding.MDK
```

## 创建 Mod 主类文件

创建 Mod 主类文件`ModEntry.cs`:

```csharp
using ModCore.Events.Interfaces.Game;
using ModCore.Mods;

namespace FirstDeadCellsMod
{
    public class FirstDeadCells : ModBase,
        IOnGameExit
    {
        public FirstDeadCells(ModInfo info) : base(info) 
        {

        }
        public override void Initialize()
        {
            Logger.Information("你好，世界");
        }

        void IOnGameExit.OnGameExit()
        {
            Logger.Information("游戏正在退出");
        }
    }
}
```

## 配置 Mod 项目

编辑项目文件`FirstDeadCellsMod.csproj`，添加以下内容：

```xml
<PropertyGroup>
    <!--模组类型-->
    <ModType>mod</ModType>

    <!--模组主类的FullName-->
    <ModMain>FirstDeadCellsMod.FirstDeadCells</ModMain>

    <!--在生成时自动安装Mod-->
    <!--<AutoInstallMod>true</AutoInstallMod>-->
</PropertyGroup>

```

## 构建 Mod

在项目目录下运行构建命令：

```bash
dotnet build
```

构建成功后，会在`bin\Debug\net9.0\output`（`$(OutputPath)\output`）目录下生成 Mod 文件

## 测试 Mod

- 按照[教程](/docs/tutorial/install-mods.md)安装 Mod
- 通过 `DeadCellsModding.exe` 启动游戏
- 检查游戏日志确认Mod加载

:::info
你应该能看到类似这样的日志条目：

```text
[13:47:52 INF][FirstDeadCells] 你好，世界
```

:::

## 常见问题

### Q1: 构建失败

- 确保已安装 .NET 9 SDK
- 检查项目引用的 NuGet 包是否可用
- 尝试清理解决方案后重新构建：

```powershell
dotnet clean
dotnet build
```

### Q2: Mod 不加载

1. 检查 `modinfo.json` 中的 `name` 是否与文件夹名称完全一致
2. 确认 `ModMain` 指定的类路径正确
3. 查看日志中的错误信息

### Q3: 游戏崩溃

- 检查 Mod 代码是否有异常
- 尝试禁用其他 Mod 进行隔离测试
- 查看日志获取详细信息

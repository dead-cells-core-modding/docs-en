---
sidebar_position: 2
---

# Creating Your First Mod

This tutorial will guide you through creating a simple mod that modifies a basic in-game function.

:::warning

Although you will be writing the **Mod** in **C#**, remember that **Dead Cells** is written in **Haxe** and runs on the **HashLink virtual machine**, not the **.NET virtual machine**.

This tutorial assumes you have the following skills:

- Basic C# programming
- Basic Dead Cells mod creation ([Tutorial](https://www.bilibili.com/opus/681293864647000128))

:::

:::tip

Before you begin, it is recommended to read the [wiki](https://github.com/HaxeFoundation/hashlink/wiki) to understand the basics of the **HashLink virtual machine**.

:::

:::info
The mod code for this tutorial is stored on [Github](https://github.com/dead-cells-core-modding/docs-zh/blob/main/modproject/FirstDeadCellsMod).
:::

## Creating the Mod Project

- Open your command-line tool.
- Create a new class library project:

```bash
dotnet new classlib -n FirstDeadCellsMod -f net9.0
```

- Navigate into the project directory:

```bash
cd FirstDeadCellsMod
```

- Add a reference to the Dead Cells Modding MDK NuGet package:

```bash
dotnet add package DeadCellsCoreModding.MDK
```

## Creating the Mod's Main Class File

Create the mod's main class file, `ModEntry.cs`:

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
            Logger.Information("Hello, World");
        }

        void IOnGameExit.OnGameExit()
        {
            Logger.Information("The game is exiting");
        }
    }
}
```

## Configuring the Mod Project

Edit the project file, `FirstDeadCellsMod.csproj`, and add the following content:

```xml
<PropertyGroup>
    <!--Mod Type-->
    <ModType>mod</ModType>

    <!--The FullName of the mod's main class-->
    <ModMain>FirstDeadCellsMod.FirstDeadCells</ModMain>

    <!--Automatically install the mod on build-->
    <!--<AutoInstallMod>true</AutoInstallMod>-->
</PropertyGroup>

```

## Building the Mod

Run the build command in the project directory:

```bash
dotnet build
```

After a successful build, the mod files will be generated in the `bin\Debug\net9.0\output` (`$(OutputPath)\output`) directory.

## Testing the Mod

- Install the mod by following the [tutorial](/docs/tutorial/install-mods.md).
- Launch the game using `DeadCellsModding.exe`.
- Check the game logs to confirm the mod has loaded.

:::info
You should see a log entry similar to this:

```text
[13:47:52 INF][FirstDeadCells] Hello, World```

:::

## Common Issues

### Q1: Build fails

- Ensure the .NET 9 SDK is installed.
- Check that the NuGet packages referenced in the project are available.
- Try cleaning the solution and then rebuilding:

```powershell
dotnet clean
dotnet build
```

### Q2: Mod doesn't load

1. Check that the `name` in `modinfo.json` exactly matches the folder name.
2. Confirm that the class path specified in `ModMain` is correct.
3. Check the logs for error messages.

### Q3: Game crashes

- Check your mod code for any exceptions.
- Try disabling other mods to isolate the issue.
- Check the logs for detailed information.

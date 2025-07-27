---
sidebar_position: 3
---

# Creating Your First Weapon

This tutorial will guide you through creating a simple mod that modifies a basic in-game function.

:::info
The mod code for this tutorial is stored on [Github](https://github.com/dead-cells-core-modding/docs-zh/blob/main/modproject/FirstWeapon).

Most of the code in this chapter comes from **Frostbite**. Thanks to Frostbite for their help with this tutorial.
:::

## Creating the Mod Project

Follow the [tutorial](./first-mod.md) to create a new mod project named **FirstWeapon**.

## Making res.pak

Follow the [tutorial](https://www.bilibili.com/opus/681993184170999904) to create a **diff pak**. The **CDB** should include:

- A new weapon named **OtherDashSword** and its corresponding **item**.

:::tip
**OtherDashSword** can be replaced with another name.
:::

A pre-made [res.pak](https://github.com/dead-cells-core-modding/docs-zh/blob/main/modproject/FirstWeapon/res.pak) is available.

### Copying res.pak

- Copy the **res.pak** file obtained in the previous step to the project's root directory.
- Modify **FirstWeapon.csproj** by adding the following content:

```xml
<ItemGroup>
    <!--Copies the res.pak file to the bin\Debug\net9.0 directory-->
    <None Update="res.pak">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>

    <!--Copies the res.pak file to the final output directory-->
    <OutputFiles Include="res.pak" />
</ItemGroup>
```

## Writing the Code

### Loading res.pak

- Modify the `FirstWeaponMod.FirstWeapon` class to implement the `IOnGameEndInit` interface.

```csharp
// Load res.pak and refresh the CDB
void IOnGameEndInit.OnGameEndInit()
{
    var res = Info.ModRoot!.GetFilePath("res.pak"); // Get the absolute path of the res.pak file in the mod's root directory
    FsPak.Instance.FileSystem.loadPak(res.AsHaxeString()); // Load res.pak
    var json =  CDBManager.Class.instance.getAlteredCDB(); // Get the merged CDB JSON
    Data.Class.loadJson( // Load the merged CDB JSON
       json, 
       default);  
}
```

:::info
After successfully loading **res.pak**, you should see something similar to this in the log:

```text
[14:30:13 INF][FsPak] Loading pak from C:\SteamLibrary\steamapps\common\Dead Cells\coremod\mods\FirstWeapon\res.pak
```

:::

:::info
The `IOnGameExit` and `IOnGameEndInit` interfaces are part of the **event system**. They are called when their corresponding **events** are triggered.

- `IOnGameExit` will be called before the game exits.
- `OnGameEndInit` will be called after the game has been initialized.
- `IOnHeroUpdate` will be called once every frame within the game.

:::

### Creating the Weapon Class

Create a new class `FirstWeaponMod.OtherDashSwordWeapon` that inherits from the `DashSword` class and implements the `IHxbitSerializable<object>` interface.

```csharp
// Weapon class
internal class OtherDashSwordWeapon : 
    DashSword, // Base class
    IHxbitSerializable<object>
{

    // Default constructor
    public OtherDashSwordWeapon(Hero o, InventItem i) : base(o, i)
    {
    }

    // Leave empty
    object IHxbitSerializable<object>.GetData()
    {
        return new(); //TODO
    }

    // Leave empty
    void IHxbitSerializable<object>.SetData(object data)
    {
        //TODO
    }

     // Test effect - add 10 cells per frame
    public override void fixedUpdate()
    {
        base.fixedUpdate();
        bool noStats = false;
        this.owner.addCells(10, new HaxeProxy.Runtime.Ref<bool>(ref noStats));
    }
}
```

:::info
The `IHxbitSerializable<>` interface is used to save game object data.

For simplicity, this weapon class inherits from the `DashSword` class instead of directly from the `Weapon` class (the base class for all weapons).
:::

### Registering the New Weapon

Simply adding the new weapon's information to the **CDB** is not enough.

To make the game recognize the new weapon, you also need to modify the `FirstWeaponMod.FirstWeapon` class by adding the following:

```csharp
 public override void Initialize()
 {
     Logger.Information("Hello, World");

     Hook__Weapon.create += Hook__Weapon_create; // Hook $Weapon.create
 }

 private Weapon Hook__Weapon_create(Hook__Weapon.orig_create orig, dc.en.Hero o, InventItem i)
 {
     var id = i._itemData.id.ToString(); // Get the weapon ID
     if(id == "OtherDashSword")
     {
         return new OtherDashSwordWeapon(o, i); // Return the custom weapon
     }
     else
     {
         return orig(o, i); // Call the original method
     }
 }
```

### Getting the New Weapon

Obviously, you can't get the new weapon yet because it has no acquisition method.

You can create the item corresponding to the new weapon with the following code:

```csharp
// Spawns the item
private void SpawnWeapon(Hero hero)
{
    InventItem testItem = new InventItem(new InventItemKind.Weapon("OtherDashSword".AsHaxeString()));
    bool test_boolean = false;
    ItemDrop itemDrop = new ItemDrop(hero._level, hero.cx, hero.cy, testItem, true, new HaxeProxy.Runtime.Ref<bool>(ref test_boolean));
    // The init method must be called after creating the drop, otherwise the game will crash
    itemDrop.init();
    itemDrop.onDropAsLoot();
    itemDrop.dx = hero.dx; // Not sure why this step is necessary, but it's in the original code
}
```

For simplicity, use the following code to allow obtaining the **new weapon** by pressing the **backslash key** **in-game**.

```csharp
// Implement the IOnHeroUpdate interface
void IOnHeroUpdate.OnHeroUpdate(double dt)
{
    if(Key.Class.isPressed(0xDC /**Backslash key code**/))
    {
        SpawnWeapon(Game.Instance.HeroInstance!);
    }
}
```

## Effect Demonstration

[Video](https://github.com/dead-cells-core-modding/docs-zh/blob/main/docs/dev/videos/Dead%20Cells%20with%20Core%20Modding%202025-07-21%2015-30-36.mp4)

---
sidebar_position: 3
---

# 创造第一个武器

本教程将指导你创建一个简单的 Mod ，实现修改游戏内某个基础功能的功能。

:::info
本教程的 Mod 代码储存在 [Github](https://github.com/dead-cells-core-modding/docs-zh/blob/main/modproject/FirstWeapon) 上。

本章节大部分代码来自 **Frostbite**。感谢 Frostbite 对本教程的帮助。
:::

:::warning
本教程假设你拥有以下技能：

- C# 编程基础
- 死亡细胞基础 Mod 制作 ([教程](https://www.bilibili.com/opus/681293864647000128))

:::

## 创建 Mod 项目

按照[教程](./first-mod.md)创造名为 **FirstWeapon** 的 Mod 项目。

## 制作 res.pak

按照[教程](https://www.bilibili.com/opus/681993184170999904)制作一个**diff pak**。**CDB**中应包括：

- 一个名为 **OtherDashSword** 新武器以及它对应的 **item**

:::tip
**OtherDashSword** 可以换为其他名字。
:::

一个现成的 [res.pak](https://github.com/dead-cells-core-modding/docs-zh/blob/main/modproject/FirstWeapon/res.pak)。

### 复制 res.pak

- 将上一步获得的**res.pak**复制到项目根目录下。
- 修改**FirstWeapon.csproj**，添加以下内容

```xml
<ItemGroup>
    <!--将res.pak文件复制到bin\Debug\net9.0目录-->
    <None Update="res.pak">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>

    <!--将res.pak文件复制到最终的输出目录-->
    <OutputFiles Include="res.pak" />
</ItemGroup>
```

## 编写代码

### 加载 res.pak

- 修改`FirstWeaponMod.FirstWeapon`类，使其实现`IOnGameEndInit`接口。

```csharp
//加载res.pak并刷新CDB
void IOnGameEndInit.OnGameEndInit()
{
    var res = Info.ModRoot!.GetFilePath("res.pak"); //获取 Mod 根目录 下的 res.pak 文件的绝对路径
    FsPak.Instance.FileSystem.loadPak(res.AsHaxeString()); //加载 res.pak
    var json =  CDBManager.Class.instance.getAlteredCDB(); //获取合并后的 CDB Json
    Data.Class.loadJson( //加载合并后的 CDB Json
       json, 
       default);  
}
```

:::info
成功加载 **res.pak** 后，你可以在日志中看到类似的内容：

```text
[14:30:13 INF][FsPak] Loading pak from C:\SteamLibrary\steamapps\common\Dead Cells\coremod\mods\FirstWeapon\res.pak
```

:::

:::info
`IOnGameExit`接口和`IOnGameEndInit`接口都是**事件系统**的一部分。它们会在对应的**事件**触发时被调用。

- `IOnGameExit` 将会在游戏退出前调用。
- `OnGameEndInit` 将会在游戏初始化后被调用。
- `IOnHeroUpdate` 将会在游戏内每帧调用一次。

:::

### 创造武器类

创造一个 `FirstWeaponMod.OtherDashSwordWeapon` 类，使其继承于`DashSword`类并实现`IHxbitSerializable<object>`接口。

```csharp
//武器类
internal class OtherDashSwordWeapon : 
    DashSword, //基类
    IHxbitSerializable<object>
{

    //默认构造方法 
    public OtherDashSwordWeapon(Hero o, InventItem i) : base(o, i)
    {
    }

    //留空
    object IHxbitSerializable<object>.GetData()
    {
        return new(); //TODO
    }

    //留空
    void IHxbitSerializable<object>.SetData(object data)
    {
        //TODO
    }

     // 测试效果——每帧增加10细胞
    public override void fixedUpdate()
    {
        base.fixedUpdate();
        bool noStats = false;
        this.owner.addCells(10, new HaxeProxy.Runtime.Ref<bool>(ref noStats));
    }
}
```

:::info
`IHxbitSerializable<>`接口用于保存游戏对象的数据。

为了简单起见，武器类并没有直接继承于 `Weapon`类（所有武器的基类），而是继承于`DashSword`类。
:::

### 注册新武器

仅仅将新武器的信息添加到 **CDB** 中是仍然是不够的。

为了让游戏可以识别新武器，还需要修改`FirstWeaponMod.FirstWeapon`类，添加以下内容：

```csharp
 public override void Initialize()
 {
     Logger.Information("你好，世界");

     Hook__Weapon.create += Hook__Weapon_create; //挂钩 $Weapon.create
 }

 private Weapon Hook__Weapon_create(Hook__Weapon.orig_create orig, dc.en.Hero o, InventItem i)
 {
     var id = i._itemData.id.ToString(); //获取武器id
     if(id == "OtherDashSword")
     {
         return new OtherDashSwordWeapon(o, i); //返回自定义的武器
     }
     else
     {
         return orig(o, i); //调用原始方法
     }
 }
```

### 获取新武器

很明显，现在无法拿到新武器，因为目前它没有任何获取途径。

可以通过以下的代码创造新武器对应的物品：

```csharp
//生成物品
private void SpawnWeapon(Hero hero)
{
    InventItem testItem = new InventItem(new InventItemKind.Weapon("OtherDashSword".AsHaxeString()));
    bool test_boolean = false;
    ItemDrop itemDrop = new ItemDrop(hero._level, hero.cx, hero.cy, testItem, true, new HaxeProxy.Runtime.Ref<bool>(ref test_boolean));
    // 生成掉落物后必须调用init方法，否则游戏会崩溃
    itemDrop.init();
    itemDrop.onDropAsLoot();
    itemDrop.dx = hero.dx; // 不知道为什么要有这一步，但是原版代码这么写的
}
```

为了简单起见，使用以下代码使得在**游戏中**可以通过按下**反斜杠**来获取**新武器**

```csharp
//实现IOnHeroUpdate接口
void IOnHeroUpdate.OnHeroUpdate(double dt)
{
    if(Key.Class.isPressed(0xDC /**反斜杠键码**/))
    {
        SpawnWeapon(Game.Instance.HeroInstance!);
    }
}
```

## 效果演示

[视频](https://github.com/dead-cells-core-modding/docs-zh/blob/main/docs/dev/videos/Dead%20Cells%20with%20Core%20Modding%202025-07-21%2015-30-36.mp4)

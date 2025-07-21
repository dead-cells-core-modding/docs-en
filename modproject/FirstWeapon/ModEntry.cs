using dc;
using dc.en;
using dc.en.inter;
using dc.hxd;
using dc.tool;
using dc.tool.mod;
using ModCore.Events.Interfaces.Game;
using ModCore.Events.Interfaces.Game.Hero;
using ModCore.Mods;
using ModCore.Modules;
using ModCore.Utitities;

namespace FirstWeaponMod
{
    public class FirstWeapon : ModBase,
        IOnGameExit,
        IOnGameEndInit,
        IOnHeroUpdate
    {
        public FirstWeapon(ModInfo info) : base(info)
        {

        }
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

        void IOnGameExit.OnGameExit()
        {
            Logger.Information("游戏正在退出");
        }

        //加载res.pak并刷新CDB
        void IOnGameEndInit.OnGameEndInit()
        {
            var res = Info.ModRoot!.GetFilePath("res.pak"); //获取 Mod 根目录 下的 res.pak 文件的绝对路径
            FsPak.Instance.FileSystem.loadPak(res.AsHaxeString()); //加载 res.pak
            var json = CDBManager.Class.instance.getAlteredCDB(); //获取合并后的 CDB Json
            Data.Class.loadJson( //加载合并后的 CDB Json
               json,
               default);
        }

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

        void IOnHeroUpdate.OnHeroUpdate(double dt)
        {
            if(Key.Class.isPressed(0xDC /**反斜杠键码**/))
            {
                SpawnWeapon(Game.Instance.HeroInstance!);
            }
        }
    }
}
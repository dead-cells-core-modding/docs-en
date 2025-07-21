using dc.en;
using dc.tool;
using dc.tool.weap;
using ModCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWeaponMod
{
    //武器类
    internal class OtherDashSwordWeapon : 
        DashSword, 
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
}

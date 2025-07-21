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
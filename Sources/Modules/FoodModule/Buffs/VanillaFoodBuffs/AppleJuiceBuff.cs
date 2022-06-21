﻿namespace Everglow.Sources.Modules.FoodModule.Buffs.VanillaFoodBuffs
{
    public class AppleJuiceBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AppleJuiceBuff");
            Description.SetDefault(" 短时间内增加80%减伤\n“一天一苹果，医生远离我”");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; // 添加这个，这样护士在治疗时就不会去除buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance += 0.8f;// 加80%减伤
            player.wellFed = true;
        }
    }
}

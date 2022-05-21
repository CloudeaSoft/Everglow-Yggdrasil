﻿namespace Everglow.Sources.Modules.Food.Buffs
{
    public class BloodOrangeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BloodOrangeBuff");
            Description.SetDefault("这里不是崽饿 \n 加25血量上限");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; // 添加这个，这样护士在治疗时就不会去除buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 += 25; // 加25血量上限
        }
    }
}


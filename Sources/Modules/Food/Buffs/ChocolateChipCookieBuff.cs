﻿namespace Everglow.Sources.Modules.Food.Buffs
{
    public class ChocolateChipCookieBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ChocolateChipCookieBuff");
            Description.SetDefault("补充能量 \n 短时间内快速恢复生命与魔力");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; // 添加这个，这样护士在治疗时就不会去除buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 50; // 加50生命恢复
            player.manaRegen += 50; // 加50魔力恢复
        }
    }
}


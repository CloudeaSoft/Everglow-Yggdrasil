﻿namespace Everglow.Sources.Modules.Food.Buffs
{
    public class PotatoChipsBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("PotatoChipsBuff");
            Description.SetDefault("高油高盐 \n 加4防御，4%伤害");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; // 添加这个，这样护士在治疗时就不会去除buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 4; // 加4防御
            player.GetDamage(DamageClass.Melee) += 4; // 加4%伤害
        }
    }
}


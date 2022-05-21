﻿namespace Everglow.Sources.Modules.Food.Buffs
{
    public class PlumBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("PlumBuff");
            Description.SetDefault("至于我的意见，那还是再等等吧 \n 加600仇恨值，8%攻速");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; // 添加这个，这样护士在治疗时就不会去除buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.aggro += 600;//加600仇恨值
            player.GetAttackSpeed(DamageClass.Generic) += 0.08f; // 加8%攻速
        }
    }
}


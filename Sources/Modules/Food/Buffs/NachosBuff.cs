﻿namespace Everglow.Sources.Modules.Food.Buffs
{
    public class NachosBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("NachosBuff");
            Description.SetDefault("爆米花 \n 攻击造成涂油以及所有火焰减益");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; // 添加这个，这样护士在治疗时就不会去除buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FoodBuffModPlayer FoodBuffModPlayer = player.GetModPlayer<FoodBuffModPlayer>();
            FoodBuffModPlayer.NachosBuff = true;
        }
    }
}


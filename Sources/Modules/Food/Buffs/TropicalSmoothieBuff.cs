﻿namespace Everglow.Sources.Modules.Food.Buffs
{
    public class TropicalSmoothieBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("TropicalSmoothieBuff");
            Description.SetDefault("热带风暴 \n 短时间内不消耗魔力，加50%魔力攻击，50%暴击");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; // 添加这个，这样护士在治疗时就不会去除buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.manaCost *= 0f;//不消耗魔力
            player.GetDamage(DamageClass.Magic) += 0.5f;//加50%攻击
            player.GetCritChance(DamageClass.Magic) += 50;//加50%暴击
        }
    }
}


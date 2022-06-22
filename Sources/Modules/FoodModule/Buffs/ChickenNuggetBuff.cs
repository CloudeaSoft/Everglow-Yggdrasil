﻿namespace Everglow.Sources.Modules.FoodModule.Buffs
{
    public class ChickenNuggetBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("ChickenNuggetBuff");
            //Description.SetDefault("增加1生命回复、8%攻速\n“数一数二的鸡块！”");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; // 添加这个，这样护士在治疗时就不会去除buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 1; // 加1生命回复
            player.GetAttackSpeed(DamageClass.Generic) += 0.08f;// 加8%攻速
            player.wellFed = true;
        }
    }
}

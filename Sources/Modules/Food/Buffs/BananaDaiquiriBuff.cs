﻿using Terraria;
using Terraria.ModLoader;
using Everglow.Sources.Modules.Food;

namespace Everglow.Sources.Modules.Food.Buffs
{
	public class BananaDaiquiriBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BananaDaiquiriBuff");
			Description.SetDefault("低体温血压 \n 短时间内不消耗子弹，极大增加远程攻击");
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false; // 添加这个，这样护士在治疗时就不会去除buff
		}

		public override void Update(Player player, ref int buffIndex)
		{
			FoodModPlayer FoodModPlayer = player.GetModPlayer<FoodModPlayer>();
			FoodModPlayer.BananaDaiquiriBuff = true;
			player.GetDamage(DamageClass.Melee).Base += 1f; // 加100%伤害
		}
	}
}

	
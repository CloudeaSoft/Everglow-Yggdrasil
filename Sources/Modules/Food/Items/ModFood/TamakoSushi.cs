﻿using Everglow.Sources.Modules.FoodModule.Buffs.VanillaFoodBuffs;
using Everglow.Sources.Modules.FoodModule.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Everglow.Food.Buffs.ModFoodBuffs;

namespace Everglow.Food.Items.ModFood
{
	public class TamakoSushi : FoodBase
	{
		public override FoodInfo FoodInfo
		{
			get
			{
				return new FoodInfo()
				{
					Satiety = 10,
					BuffType = ModContent.BuffType<TamakoSushiBuff>(),
					BuffTime = new FoodDuration(4, 0, 0),
					Name = "TamakoSushiBuff"
				};
			}
		}
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;

			Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));

			ItemID.Sets.FoodParticleColors[Item.type] = new Color[3] {
				new Color(219, 184, 70),
				new Color(104, 127, 86),
				new Color(211, 202, 175)
			};

			ItemID.Sets.IsFood[Type] = true;
		}

		public override void SetDefaults()
		{

			Item.DefaultToFood(22, 22, BuffID.WellFed3, 57600);
			Item.value = Item.buyPrice(0, 3);
			Item.rare = ItemRarityID.Blue;
		}
	}
}
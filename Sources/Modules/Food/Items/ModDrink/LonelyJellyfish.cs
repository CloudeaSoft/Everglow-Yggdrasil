﻿using Everglow.Food;
using Everglow.Food.Buffs.ModDrinkBuffs;
using Everglow.Sources.Modules.FoodModule.Buffs.ModFoodBuffs;
using Everglow.Sources.Modules.FoodModule.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Everglow.Food.Items.ModDrink
{
	public class LonelyJellyfish : DrinkBase
	{
		public override DrinkInfo DrinkInfo
		{
			get
			{
				return new DrinkInfo()
				{
					Thirsty = false,
					BuffType = ModContent.BuffType<LonelyJellyfishBuff>(),
					BuffTime = new FoodDuration(0, 10, 0),
					Name = "LonelyJellyfishBuff"
				};
			}
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("孤独的水母");

			Tooltip.SetDefault("{$CommonItemTooltip.MediumStats}\n'观赏完就一饮而尽吧'");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;

			Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));

			ItemID.Sets.FoodParticleColors[Item.type] = new Color[3] {
				new Color(193, 245, 255),
				new Color(0, 96, 193),
				new Color(112, 126, 216)
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
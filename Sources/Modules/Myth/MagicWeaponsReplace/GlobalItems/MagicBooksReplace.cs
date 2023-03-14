﻿using Terraria.DataStructures;
using Terraria.Localization;

namespace Everglow.Sources.Modules.MythModule.MagicWeaponsReplace.GlobalItems
{
	public class MagicBooksReplace : GlobalItem
	{
		private static string SpellbookExtra = "Mods.Everglow.ExtraTooltip.Spellbook.";
		private static string SpellbookGTV(string key) => Language.GetTextValue(SpellbookExtra + key);
		public override void SetDefaults(Item item)
		{
			base.SetDefaults(item);
		}

		public override bool PreDrawTooltipLine(Item item, DrawableTooltipLine line, ref int yOffset)
		{
			if (Main.LocalPlayer.TryGetModPlayer(out MagicBookPlayer modplayer))
			{
				if (modplayer.MagicBookLevel == 1)
				{
					if (item.type == ItemID.WaterBolt)
					{
					}
				}
			}
			return base.PreDrawTooltipLine(item, line, ref yOffset);
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (Main.LocalPlayer.GetModPlayer<MagicBookPlayer>().MagicBookLevel == 1)
			{
				if (item.type == ItemID.WaterBolt)
				{
					tooltips.Add(new TooltipLine(ModLoader.GetMod("Everglow"), "ExtraTooltip", SpellbookGTV("WaterBolt")));
				}
				if (item.type == ItemID.DemonScythe)
				{
					tooltips.Add(new TooltipLine(ModLoader.GetMod("Everglow"), "ExtraTooltip", SpellbookGTV("DemonScythe")));
				}
				if (item.type == ItemID.BookofSkulls)
				{
					tooltips.Add(new TooltipLine(ModLoader.GetMod("Everglow"), "ExtraTooltip", SpellbookGTV("BookofSkulls")));
				}
				if (item.type == ItemID.CrystalStorm)
				{
					tooltips.Add(new TooltipLine(ModLoader.GetMod("Everglow"), "ExtraTooltip", SpellbookGTV("CrystalStorm")));
				}
				if (item.type == ItemID.CursedFlames)
				{
					tooltips.Add(new TooltipLine(ModLoader.GetMod("Everglow"), "ExtraTooltip", SpellbookGTV("CursedFlames")));
				}
				if (item.type == ItemID.GoldenShower)
				{
					tooltips.Add(new TooltipLine(ModLoader.GetMod("Everglow"), "ExtraTooltip", SpellbookGTV("GoldenShower")));
				}
				if (item.type == ItemID.MagnetSphere)
				{
					tooltips.Add(new TooltipLine(ModLoader.GetMod("Everglow"), "ExtraTooltip", SpellbookGTV("MagnetSphere")));
				}
				if (item.type == ItemID.RazorbladeTyphoon)
				{
					tooltips.Add(new TooltipLine(ModLoader.GetMod("Everglow"), "ExtraTooltip", SpellbookGTV("RazorbladeTyphoon")));
				}
				if (item.type == ItemID.LunarFlareBook)
				{
					tooltips.Add(new TooltipLine(ModLoader.GetMod("Everglow"), "ExtraTooltip", SpellbookGTV("LunarFlareBook")));
				}
			}
		}

		public override bool? UseItem(Item item, Player player)
		{
			if (player.GetModPlayer<MagicBookPlayer>().MagicBookLevel == 0)
			{
				if (item.type == ItemID.WaterBolt)
				{
					item.noUseGraphic = false;
				}
				if (item.type == ItemID.BookofSkulls)
				{
					item.noUseGraphic = false;
				}
				if (item.type == ItemID.DemonScythe)
				{
					item.autoReuse = false;
					item.noUseGraphic = false;
				}
				if (item.type == ItemID.CursedFlames)
				{
					item.noUseGraphic = false;
				}
				if (item.type == ItemID.GoldenShower)
				{
					item.noUseGraphic = false;
				}
				if (item.type == ItemID.CrystalStorm)
				{
					item.noUseGraphic = false;
				}
				if (item.type == ItemID.MagnetSphere)
				{
					item.noUseGraphic = false;
				}
				if (item.type == ItemID.RazorbladeTyphoon)
				{
					item.noUseGraphic = false;
				}
				if (item.type == ItemID.LunarFlareBook)
				{
					item.noUseGraphic = false;
				}
				if (item.type == ModContent.ItemType<TheFirefly.Items.Weapons.DreamWeaver>())
				{
					item.noUseGraphic = false;
				}
				return base.UseItem(item, player);
			}
			if (item.type == ItemID.WaterBolt)
			{
				item.noUseGraphic = true;
			}
			if (item.type == ItemID.BookofSkulls)
			{
				item.noUseGraphic = true;
			}
			if (item.type == ItemID.DemonScythe)
			{
				item.autoReuse = true;
				item.noUseGraphic = true;
			}
			if (item.type == ItemID.CursedFlames)
			{
				item.noUseGraphic = true;
			}
			if (item.type == ItemID.GoldenShower)
			{
				item.noUseGraphic = true;
			}
			if (item.type == ItemID.CrystalStorm)
			{
				item.noUseGraphic = true;
			}
			if (item.type == ItemID.MagnetSphere)
			{
				item.autoReuse = true;
				item.noUseGraphic = true;
			}
			if (item.type == ItemID.RazorbladeTyphoon)
			{
				item.noUseGraphic = true;
			}
			if (item.type == ItemID.LunarFlareBook)
			{
				item.noUseGraphic = true;
			}
			if (item.type == ModContent.ItemType<TheFirefly.Items.Weapons.DreamWeaver>())
			{
				item.noUseGraphic = true;
			}
			// Aim Types
			if (item.type == ItemID.WaterBolt)
			{
				int aimType = ModContent.ProjectileType<Projectiles.WaterBolt.WaterBoltBook>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
				aimType = ModContent.ProjectileType<Projectiles.WaterBolt.WaterBoltArray>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
			}
			if (item.type == ItemID.DemonScythe)
			{
				int aimType = ModContent.ProjectileType<Projectiles.DemonScythe.DemonScytheBook>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
				aimType = ModContent.ProjectileType<Projectiles.DemonScythe.DemonScytheArray>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
			}
			if (item.type == ItemID.MagnetSphere)
			{
				int aimType = ModContent.ProjectileType<Projectiles.MagnetSphere.MagnetSphereBook>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
				aimType = ModContent.ProjectileType<Projectiles.MagnetSphere.MagnetSphereArray>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
			}
			if (item.type == ItemID.RazorbladeTyphoon)
			{
				int aimType = ModContent.ProjectileType<Projectiles.RazorbladeTyphoon.RazorbladeTyphoonBook>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, player.HeldItem.damage, 0, player.whoAmI);
				}
				aimType = ModContent.ProjectileType<Projectiles.RazorbladeTyphoon.RazorbladeTyphoonArray>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
			}
			if (item.type == ItemID.CursedFlames)
			{
				int aimType = ModContent.ProjectileType<Projectiles.CursedFlames.CursedFlamesBook>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
				aimType = ModContent.ProjectileType<Projectiles.CursedFlames.CursedFlamesArray>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
			}
			if (item.type == ItemID.CrystalStorm)
			{
				int aimType = ModContent.ProjectileType<Projectiles.CrystalStorm.CrystalStormBook>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
				aimType = ModContent.ProjectileType<Projectiles.CrystalStorm.CrystalStormArray>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
			}
			if (item.type == ItemID.BookofSkulls)
			{
				int aimType = ModContent.ProjectileType<Projectiles.BookofSkulls.BookofSkullsBook>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
				aimType = ModContent.ProjectileType<Projectiles.BookofSkulls.BookofSkullsArray>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
			}
			if (item.type == ItemID.GoldenShower)
			{
				int aimType = ModContent.ProjectileType<Projectiles.GoldenShower.GoldenShowerBook>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
				aimType = ModContent.ProjectileType<Projectiles.GoldenShower.GoldenShowerArray>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
			}
			if (item.type == ItemID.LunarFlareBook)
			{
				int aimType = ModContent.ProjectileType<Projectiles.LunarFlare.LunarFlareBook>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
				aimType = ModContent.ProjectileType<Projectiles.LunarFlare.LunarFlareArray>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
			}
			if (item.type == ModContent.ItemType<TheFirefly.Items.Weapons.DreamWeaver>())
			{
				int aimType = ModContent.ProjectileType<Projectiles.DreamWeaver.DreamWeaverBook>();
				if (player.ownedProjectileCounts[aimType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, aimType, 0, 0, player.whoAmI);
				}
			}
			return base.UseItem(item, player);
		}

		public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.GetModPlayer<MagicBookPlayer>().MagicBookLevel == 0)
			{
				return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
			}
			if (item.type == ItemID.WaterBolt)
			{
				return false;
			}
			if (item.type == ItemID.DemonScythe)
			{
				return false;
			}
			if (item.type == ItemID.BookofSkulls)
			{
				return false;
			}
			if (item.type == ItemID.GoldenShower)
			{
				return false;
			}
			if (item.type == ItemID.CursedFlames)
			{
				return false;
			}
			if (item.type == ItemID.CrystalStorm)
			{
				return false;
			}
			if (item.type == ItemID.MagnetSphere)
			{
				return false;
			}
			if (item.type == ItemID.RazorbladeTyphoon)
			{
				return false;
			}
			if (item.type == ItemID.LunarFlareBook)
			{
				return false;
			}
			return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
		}

		public override void HoldItem(Item item, Player player)
		{
			MagicBookPlayer mplayer = player.GetModPlayer<MagicBookPlayer>();
			if (player.GetModPlayer<MagicBookPlayer>().MagicBookLevel == 1)
			{
				if (item.type == ItemID.BookofSkulls)
				{
					if (Main.mouseRight && Main.mouseRightRelease && mplayer.HandCooling <= 0 && player.statMana > player.HeldItem.mana * 2)
					{
						for (int g = -5; g < 150; g++)
						{
							if (Collision.SolidCollision(Main.MouseWorld + new Vector2(0, g * 5 * player.gravDir), 1, 1))
							{
								Vector2 ReleasePoint = Main.MouseWorld + new Vector2(0, g * 5 * player.gravDir);
								Projectile p = Projectile.NewProjectileDirect(item.GetSource_FromAI(), ReleasePoint, Vector2.Zero, ModContent.ProjectileType<Projectiles.BookofSkulls.SkullHand>(), player.HeldItem.damage * 3, player.HeldItem.knockBack * 6, player.whoAmI);
								p.CritChance = player.GetWeaponCrit(player.HeldItem);

								mplayer.HandCooling = 18;
								player.statMana -= player.HeldItem.mana * 4;
								break;
							}
						}
					}
				}
			}
		}
	}

	internal class MagicBookPlayer : ModPlayer
	{
		public int MagicBookLevel = 0;
		public int WaterBoltHasHit = 0;
		public int HandCooling = 0;

		public override void PreUpdate()
		{
			MagicBookLevel = 0;
			if (HandCooling > 0)
			{
				HandCooling--;
			}
			base.PreUpdate();
		}

		public override bool PreItemCheck()
		{
			//MagicBookLevel = 0;
			if (WaterBoltHasHit > 0)
			{
				if (Player.HeldItem.type != ItemID.WaterBolt || MagicBookLevel == 0)
				{
					WaterBoltHasHit = 0;
					/*foreach(Projectile p in Main.projectile)
                    {
                        if(p.owner == Player.whoAmI && p.type == ModContent.ProjectileType<Projectiles.WaterBolt.WaterTeleport>())
                        {
                            p.Kill();
                        }
                    }*/
				}
			}
			return base.PreItemCheck();
		}
	}
}
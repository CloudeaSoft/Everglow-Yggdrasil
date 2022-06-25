﻿using Everglow.Sources.Modules.MEACModule.Projectiles;
using Everglow.Sources.Modules.MythModule.TheFirefly.Projectiles;
using Everglow.Sources.Modules.MythModule.TheFirefly.WorldGeneration;
namespace Everglow.Sources.Modules.MythModule.TheFirefly.Items.Weapons
{
    public class ScaleWingBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            Item.useStyle = 1;
            Item.width = 1;
            Item.height = 1;
            Item.useAnimation = 5;
            Item.useTime = 5;
            Item.shootSpeed = 5f;
            Item.knockBack = 2.5f;
            Item.damage = 30;
            Item.rare = ItemRarityID.Green;

            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.value = Item.sellPrice(gold: 1);
        }
        public override bool CanUseItem(Player player)
        {
            if(base.CanUseItem(player))
            {
                if(Main.myPlayer==player.whoAmI)
                {
                    if (player.altFunctionUse != 2)
                    {
                        Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, Vector2.Zero, ModContent.ProjectileType<ScaleWingBladeProj>(), player.GetWeaponDamage(Item), Item.knockBack, player.whoAmI);
                    }
                    else//右键
                    {
                        Projectile proj=Projectile.NewProjectileDirect(player.GetSource_ItemUse(Item), player.Center, Vector2.Zero, ModContent.ProjectileType<ScaleWingBladeProj>(), player.GetWeaponDamage(Item), Item.knockBack, player.whoAmI);
                        (proj.ModProjectile as MeleeProj).attackType = 100;
                        (proj.ModProjectile as MeleeProj).isRightClick = true;
                        proj.netUpdate2 = true;
                    }
                }
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void AddRecipes()
        {

        }
    }
}

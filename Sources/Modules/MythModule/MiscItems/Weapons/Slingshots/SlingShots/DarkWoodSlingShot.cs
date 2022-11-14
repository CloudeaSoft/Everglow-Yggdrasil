﻿using Terraria.DataStructures;

namespace MythMod.Items.Weapons.SlingShots
{
    public class DarkWoodSlingShot : ModItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Shadewood SlingShot");
            //DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "暗影木弹弓");
            //Tooltip.SetDefault("Shoot little stones\nDon't need ammos");
            //Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "射出石子\n免弹药远程武器");
        }
        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.crit = 4;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 30;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.autoReuse = false;
            Item.value = Item.sellPrice(0, 0, 0, 50);
            Item.rare = 1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.Slingshots.SlingshotAmmo>();
            Item.noUseGraphic = true;
            Item.shootSpeed = 8f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Ranged.Slingshots.DarkWoodSlingShot>()] < 1)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<Projectiles.Ranged.Slingshots.DarkWoodSlingShot>(), damage, knockback, player.whoAmI, Item.shootSpeed, Item.useAnimation);
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Cobweb, 14)
                .AddIngredient(ItemID.Shadewood, 7)
                .AddTile(18)
                .Register();
        }
    }
}

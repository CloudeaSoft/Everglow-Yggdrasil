﻿using Terraria.DataStructures;

namespace MythMod.Items.Weapons.SlingShots
{
    public class AmbiguousNight : ModItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Ambiguous Night");
            //DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "幻夜弹弓");
            //Tooltip.SetDefault("Shoot little stones\nDon't need ammos");
            //Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "射出石子\n免弹药远程武器");
        }
        public override void SetDefaults()
        {
            Item.damage = 54;
            Item.crit = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 30;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.autoReuse = false;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 5;
            Item.UseSound = SoundID.Item5;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.AmbiguousNight>();
            Item.shootSpeed = 12f;
            Item.autoReuse = false;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Ranged.Slingshots.AmbiguousNightSling>()] < 1)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<Projectiles.Ranged.Slingshots.AmbiguousNightSling>(), damage, knockback, player.whoAmI, Item.shootSpeed, Item.useAnimation);
            }
            return false;
        }
    }
}

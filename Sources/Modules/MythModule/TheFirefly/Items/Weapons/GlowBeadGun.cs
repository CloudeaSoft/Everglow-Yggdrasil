﻿using Everglow.Sources.Modules.MythModule.TheFirefly.Projectiles;
using Everglow.Sources.Modules.MythModule.TheFirefly.WorldGeneration;
using Everglow.Sources.Modules.MythModule.Common;
using Terraria.DataStructures;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.ID;

namespace Everglow.Sources.Modules.MythModule.TheFirefly.Items.Weapons
{
    public class GlowBeadGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            GetGlowMask = MythContent.SetStaticDefaultsGlowMask(this);
        }
        public static short GetGlowMask = 0;
        public override void SetDefaults()
        {
            Item.glowMask = GetGlowMask;
            Item.damage = 22;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
            Item.width = 104;
            Item.height = 38;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item132 with {MaxInstances = 3};
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.GlowBeadGun>();
            Item.shootSpeed = 8;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(player.ownedProjectileCounts[type] > 0)
            {
                return false;
            }
            return true;
        }
    }
}
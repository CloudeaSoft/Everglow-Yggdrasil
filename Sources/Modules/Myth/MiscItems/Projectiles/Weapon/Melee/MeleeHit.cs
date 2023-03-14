﻿using Terraria.Localization;

namespace Everglow.Myth.MiscItems.Projectiles.Weapon.Melee;

public class MeleeHit : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Null");
			}
	public override void SetDefaults()
	{
		Projectile.width = 200;
		Projectile.height = 200;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.ignoreWater = true;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 1;
		Projectile.alpha = 0;
		Projectile.penetrate = 1;
		Projectile.scale = 1;
	}
	public override void AI()
	{
	}
}
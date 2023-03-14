﻿namespace Everglow.Myth.MagicWeaponsReplace.Projectiles.BlackHole.Dust
{
	[Pipeline(typeof(WCSPipeline))]
	internal class DarkDust : Particle
	{
		public int time;
		public int time_max;
		public override void AI()
		{
			time++;
			if (time > time_max - 20)
			{
				velocity *= 0.9f;
				scale *= 0.9f;
			}
			if (time > time_max)
				Kill();
			else
			{
				velocity = velocity.RotatedBy(0.05f);
			}
		}
		public override void Draw()
		{
			Texture2D tex = ModContent.Request<Texture2D>("Everglow/Sources/Modules/MythModule/MagicWeaponsReplace/Projectiles/BlackHole/Dust/BlackDust").Value;
			for (int i = 0; i < 5; i++)
			{
				float a = (5 - i) / 5f;
				VFXManager.spriteBatch.Draw(tex, position - velocity * i * 0.5f, null, Color.White * a, velocity.ToRotation(), tex.Size() / 2, new Vector2(1.2f, 0.8f) * scale, 0);
			}
		}
	}
}

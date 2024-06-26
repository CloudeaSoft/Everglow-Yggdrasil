using Terraria;
using Terraria.DataStructures;

namespace Everglow.Myth.LanternMoon.Projectiles.LanternKing;

class GoldLanternRay : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 20;
		Projectile.height = 20;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 400;
		Projectile.tileCollide = false;
		Projectile.DamageType = DamageClass.Melee;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
	}
	public override void OnSpawn(IEntitySource source)
	{
		Projectile.rotation = Projectile.ai[0];
	}
	public override void AI()
	{
		Projectile.velocity *= 0.96f;

		Projectile.rotation = Projectile.rotation * 0.96f + Projectile.ai[1] * 0.04f;
		if (Projectile.timeLeft < 60)
			yd = Projectile.timeLeft / 60f;
		if (Projectile.timeLeft > 340)
			yd = (400 - Projectile.timeLeft) / 60f;
		CirR0 += 0.001f;
		CirPro0 += 0.3f;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}
	public static float timer = 0;
	public static int WHOAMI = -1;
	public static int Typ = -1;
	int TrueL = 1;
	float CirR0 = 0;
	float CirPro0 = 0;
	float yd = 1;
	Vector2[] Vlaser = new Vector2[508];
	public override void PostDraw(Color lightColor)
	{
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
		var bars = new List<Vertex2D>();

		float step = 4;
		int Count = 0;
		for (int m = 0; m < 500; ++m)
		{
			if (Collision.SolidCollision(Projectile.Center + new Vector2(0, step).RotatedBy(Projectile.rotation) * m, 1, 1))
				break;
			Vlaser[m] = Projectile.Center + new Vector2(0, step).RotatedBy(Projectile.rotation) * m;
			++Count;
		}
		for (int i = 1; i < Count; ++i)
		{
			if (Vlaser[i] == Vector2.Zero)
				break;
			var normalDir = Vlaser[i - 1] - Vlaser[i];
			normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));

			var factor = (float)Math.Sqrt(i + 1) / TrueL / 9f + Projectile.timeLeft / 60f;
			var w = MathHelper.Lerp(1f, 0.05f, factor);
			float width = 60;
			if (i <= 25)
				width = 60 * (float)Math.Sqrt(i) / 5f;

			width *= yd;
			Lighting.AddLight(Vlaser[i], (255 - Projectile.alpha) * 1.2f / 50f * yd, 0, 0);
			if (Count - i < 5)
			{
				bars.Add(new Vertex2D(Vlaser[i] + normalDir * width - Main.screenPosition, new Color(Math.Clamp((int)(255 * (Count - i - 1) / 5f), 0, 255), 0, 0, 0), new Vector3(factor % 1f, 1, w)));
				bars.Add(new Vertex2D(Vlaser[i] + normalDir * -width - Main.screenPosition, new Color(Math.Clamp((int)(255 * (Count - i - 1) / 5f), 0, 255), 0, 0, 0), new Vector3(factor % 1f, 0, w)));
			}
			else
			{
				bars.Add(new Vertex2D(Vlaser[i] + normalDir * width - Main.screenPosition, new Color(254, 254, 254, 0), new Vector3(factor % 1f, 1, w)));
				bars.Add(new Vertex2D(Vlaser[i] + normalDir * -width - Main.screenPosition, new Color(254, 254, 254, 0), new Vector3(factor % 1f, 0, w)));
			}
		}
		var Vx = new List<Vertex2D>();
		if (bars.Count > 2)
		{
			Vx.Add(bars[0]);
			var vertex = new Vertex2D((bars[0].position + bars[1].position) * 0.5f + new Vector2(-5, 0).RotatedBy(Projectile.rotation), new Color(254, 254, 254, 0), new Vector3(0, 0.5f, 1));
			Vx.Add(bars[1]);
			Vx.Add(vertex);
			for (int i = 0; i < bars.Count - 2; i += 2)
			{
				Vx.Add(bars[i]);
				Vx.Add(bars[i + 2]);
				Vx.Add(bars[i + 1]);

				Vx.Add(bars[i + 1]);
				Vx.Add(bars[i + 2]);
				Vx.Add(bars[i + 3]);
			}
		}
		Texture2D t = ModContent.Request<Texture2D>("Everglow/Myth/LanternMoon/Projectiles/LanternKing/GoldLaser").Value;
		Main.graphics.GraphicsDevice.Textures[0] = t;//GlodenBloodScaleMirror
		Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, Vx.ToArray(), 0, Vx.Count / 3);

		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
		var Vx2 = new List<Vertex2D>();

		Vector2 vf = Vlaser[Math.Clamp(Count - 1, 0, 507)] - Main.screenPosition;
		float ACircleR = 150 * yd;
		for (int h = 0; h < 100; h++)
		{
			var color3 = new Color(254, 254, 254, 0);
			Vector2 v0 = new Vector2(0, ACircleR).RotatedBy(h / 50d * Math.PI + CirR0);
			Vector2 v1 = new Vector2(0, ACircleR).RotatedBy((h + 1) / 50d * Math.PI + CirR0);
			if (h % 20 >= 10)
			{
				Vx2.Add(new Vertex2D(vf + v0, color3, new Vector3((0.999f + CirPro0) / 25f % 1f, 0, 0)));
				Vx2.Add(new Vertex2D(vf + v1, color3, new Vector3(CirPro0 / 25f % 1f, 0, 0)));
				Vx2.Add(new Vertex2D(vf, color3, new Vector3((0.5f + CirPro0) / 25f % 1f, 1, 0)));
			}
			else
			{
				Vx2.Add(new Vertex2D(vf + v0, color3, new Vector3(CirPro0 / 25f % 1f, 0, 0)));
				Vx2.Add(new Vertex2D(vf + v1, color3, new Vector3((0.999f + CirPro0) / 25f % 1f, 0, 0)));
				Vx2.Add(new Vertex2D(vf, color3, new Vector3((0.5f + CirPro0) / 25f % 1f, 1, 0)));
			}
		}
		Texture2D t1 = ModContent.Request<Texture2D>("Everglow/Myth/UIImages/VisualTextures/LightCrackGold").Value;
		Main.graphics.GraphicsDevice.Textures[0] = t1;//GlodenBloodScaleMirror
		Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, Vx2.ToArray(), 0, Vx2.Count / 3);
	}
}

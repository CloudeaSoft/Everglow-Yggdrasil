using Terraria;
using Terraria.Localization;

namespace Everglow.Myth.TheTusk.NPCs.Bosses.BloodTusk;

public class LittleMouth : ModNPC
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("");
			}
	public override void SetDefaults()
	{
		NPC.behindTiles = true;
		NPC.width = 60;
		NPC.height = 60;
		NPC.defense = 0;
		NPC.lifeMax = 5;
		NPC.knockBackResist = 0f;
		NPC.value = Item.buyPrice(0, 0, 0, 0);
		NPC.aiStyle = -1;
		NPC.alpha = 0;
		NPC.lavaImmune = true;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.dontTakeDamage = true;
		NPC.damage = 80;
		if (Main.expertMode)
			NPC.damage = 120;
		if (Main.masterMode)
			NPC.damage = 150;
	}
	private int Ty = 0;
	private int Tokill = -1;
	private int cooling = 20;
	public override void AI()
	{
		NPC.rotation = (float)(Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + Math.PI / 2d);
		if (!Collision.SolidCollision(NPC.Center, 1, 1) && Ty == 0)
			Ty += 1;
		if (Ty >= 1)
			cooling--;
		if (Collision.SolidCollision(NPC.Center, 1, 1) && Ty == 1 && cooling <= 0)
		{
			Ty += 1;
			Tokill = 20;
		}
		if (Ty == 1)
		{
			NPC.velocity.Y += 0.15f;
			NPC.velocity *= 0.99f;
		}
		if (Tokill > 0)
		{
			Tokill--;
			if (Tokill <= 3)
				NPC.active = false;
		}
		if (Big < BigMax)
			Big += 0.02f;
		if (Big >= BigMax && !MaxB)
			MaxB = true;
		if (MaxB)
		{
			Big -= 0.15f;
			Big *= 0.9f;
		}
		if (Big < 0 && MaxB)
		{
			Big *= 0.95f;
			if (Big > -0.05f)
			{
				Big += 0.06f;
				MaxB = false;
			}
		}
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(BuffID.Bleeding, 120);
	}

	private float Big = 0;
	private float BigMax = 1.2f;
	private bool MaxB = false;
	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Color color = Lighting.GetColor((int)(NPC.Center.X / 16d), (int)(NPC.Center.Y / 16d));
		color = NPC.GetAlpha(color) * ((255 - NPC.alpha) / 255f);
		Texture2D t1 = ModContent.Request<Texture2D>("Everglow/Myth/TheTusk/NPCs/Bosses/BloodTusk/LittleMouth1").Value;
		Main.spriteBatch.Draw(t1, NPC.Center - Main.screenPosition, null, color, NPC.rotation + Big - (float)Math.PI / 2f, new Vector2(6, t1.Height / 2f), 1f, SpriteEffects.None, 0f);
		Texture2D t2 = ModContent.Request<Texture2D>("Everglow/Myth/TheTusk/NPCs/Bosses/BloodTusk/LittleMouth2").Value;
		Main.spriteBatch.Draw(t2, NPC.Center - Main.screenPosition, null, color, NPC.rotation - Big - (float)Math.PI / 2f, new Vector2(6, t1.Height / 2f), 1f, SpriteEffects.None, 0f);

		return false;
	}
}
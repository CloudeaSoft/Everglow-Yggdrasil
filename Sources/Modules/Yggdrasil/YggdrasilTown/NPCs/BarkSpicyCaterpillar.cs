using Everglow.Yggdrasil.YggdrasilTown.Dusts;
using Terraria.DataStructures;

namespace Everglow.Yggdrasil.YggdrasilTown.NPCs;

public class BarkSpicyCaterpillar : Caterpillar
{
	public override void SetDefaults()
	{
		SegmentBehavioralSize = 10;
		SegmentHitBoxSize = 30;
		SegmentCount = 10;
		AnimationSpeed = 2;
		base.SetDefaults();
	}
	public override void OnSpawn(IEntitySource source)
	{
		base.OnSpawn(source);
	}
	public override Rectangle GetDrawFrame(int Style)
	{
		Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
		int height = texture.Height;
		if (Style == 0)
		{
			return new Rectangle(0, 0, 32, height);
		}
		if (Style == 1)
		{
			return new Rectangle(34, 0, 10, height);
		}
		if (Style == 2)
		{
			return new Rectangle(56, 0, 24, height);
		}
		return base.GetDrawFrame(Style);
	}
	public override void SetStaticDefaults()
	{
		NPCSpawnManager.RegisterNPC(Type);
	}
	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		YggdrasilTownBiome YggdrasilTownBiome = ModContent.GetInstance<YggdrasilTownBiome>();
		if (!YggdrasilTownBiome.IsBiomeActive(Main.LocalPlayer))
			return 0f;
		return 3f;
	}
	public override bool PreKill()
	{
		for (int j = 0; j < Segments.Count; j++)
		{
			Vector2 v0 = new Vector2(0, Main.rand.NextFloat(0, 6f)).RotatedByRandom(MathHelper.TwoPi);
			int type = ModContent.Find<ModGore>("Everglow/BrownCaterpillar_gore1").Type;
			if (j == 0)
			{
				type = ModContent.Find<ModGore>("Everglow/BrownCaterpillar_gore0").Type;
			}
			if (j == Segments.Count - 1)
			{
				type = ModContent.Find<ModGore>("Everglow/BrownCaterpillar_gore2").Type;
			}
			Gore.NewGore(NPC.GetSource_Death(), NPC.Center + Segments[j].SelfPosition, v0, type, NPC.scale);
			if (Main.rand.NextBool(2))
			{
				v0 = new Vector2(0, Main.rand.NextFloat(0, 6f)).RotatedByRandom(MathHelper.TwoPi);
				type = ModContent.Find<ModGore>("Everglow/BrownCaterpillar_gore3").Type;
				Gore.NewGore(NPC.GetSource_Death(), NPC.Center + Segments[j].SelfPosition, v0, type, NPC.scale);
			}
		}
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < Segments.Count; j++)
			{
				Vector2 pos = NPC.Center + Segments[j].SelfPosition;
				Dust.NewDustDirect(pos - new Vector2(SegmentHitBoxSize / 2), SegmentHitBoxSize / 2, SegmentHitBoxSize / 2, ModContent.DustType<VerdantBlood>());
			}
		}
		return base.PreKill();
	}
}

using Everglow.Sources.Modules.MythModule.TheFirefly.Dusts;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.ObjectData;

namespace Everglow.Sources.Modules.MythModule.TheFirefly.Tiles.Furnitures
{
	public class GlowWoodCandle : ModTile
	{
		private Asset<Texture2D> flameTexture;

		public override void SetStaticDefaults()
		{
			// Properties
			Main.tileTable[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.InteractibleByNPCs[Type] = true; // Town NPCs will palm their hand at this tile
			TileID.Sets.IsValidSpawnPoint[Type] = true;

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);

			DustType = ModContent.DustType<BlueGlow>();
			AdjTiles = new int[] { TileID.Candles };
			ItemDrop = ModContent.ItemType<Items.Furnitures.GlowWoodCandle>();
			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1); // this style already takes care of direction for us
			TileObjectData.addTile(Type);

			// Etc
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("GlowWood Candle");

			AddMapEntry(new Color(0, 14, 175), name);
			if (!Main.dedServ)
			{
				if (!Main.dedServ)
				{
					flameTexture = ModContent.Request<Texture2D>("Everglow/Sources/Modules/MythModule/TheFirefly/Tiles/Furnitures/GlowWoodCandle_Flame");
				}
			}
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.1f;
			g = 0.9f;
			b = 1f;
		}

		public override void HitWire(int i, int j)
		{
			FurnitureUtils.LightHitwire(i, j, Type, 1, 1);
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			// The following code draws multiple flames on top our placed torch.

			int offsetY = 0;

			if (WorldGen.SolidTile(i, j - 1))
			{
				offsetY = 2;

				if (WorldGen.SolidTile(i - 1, j + 1) || WorldGen.SolidTile(i + 1, j + 1))
				{
					offsetY = 4;
				}
			}

			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);

			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}

			ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (uint)i); // Don't remove any casts.
			Color color = new Color(100, 100, 100, 0);
			int width = 20;
			int height = 20;
			var tile = Main.tile[i, j];
			int frameX = tile.TileFrameX;
			int frameY = tile.TileFrameY;

			for (int k = 0; k < 7; k++)
			{
				float xx = Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
				float yy = Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;

				spriteBatch.Draw(flameTexture.Value, new Vector2(i * 16 - (int)Main.screenPosition.X - (width - 16f) / 2f + xx, j * 16 - (int)Main.screenPosition.Y + offsetY + yy) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default, 1f, SpriteEffects.None, 0f);
			}
		}

		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
		{
			offsetY = 2;
		}

		public override void KillMultiTile(int x, int y, int frameX, int frameY)
		{
			Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 48, 32, ModContent.ItemType<Items.Furnitures.GlowWoodCandle>());
		}
	}
}
﻿namespace Everglow.Myth.TheFirefly.Tiles
{
	internal class FireflyTreeEntity : ModTileEntity
	{
		public override bool IsTileValidForEntity(int x, int y)
		{
			return Main.tile[x, y].TileType == ModContent.TileType<FluorescentTree>();
		}
	}
}
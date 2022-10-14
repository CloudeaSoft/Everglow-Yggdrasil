using Everglow.Sources.Commons.Function.ImageReader;
using Everglow.Sources.Modules.ZYModule.Commons.Function.MapIO;
using Terraria.IO;
using Terraria.WorldBuilding;
using Everglow.Sources.Modules.YggdrasilModule.Common;
using Everglow.Sources.Modules.YggdrasilModule.YggdrasilTown.Tiles;
using Everglow.Sources.Modules.YggdrasilModule.KelpCurtain.Tiles;
using Everglow.Sources.Modules.YggdrasilModule.HurricaneMaze.Tiles;

using Everglow.Sources.Modules.YggdrasilModule.YggdrasilTown.Walls;
using Everglow.Sources.Modules.YggdrasilModule.KelpCurtain.Walls;


namespace Everglow.Sources.Modules.YggdrasilModule.WorldGeneration
{
    public class YggdrasilWorldGeneration : ModSystem
    {
        internal class YggdrasilWorldGenPass : GenPass
        {
            public YggdrasilWorldGenPass() : base("Yggdrasil, the Tree World", 500)
            {
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                Main.statusText = Terraria.Localization.Language.GetTextValue("Mods.Everlow.Common.WorldSystem.BuildtheTreeWorld");
                BuildtheTreeWorld();
            }
        }

        //public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight) => tasks.Add(new YggdrasilWorldGenPass());
        /// <summary>
        /// type = 0:Kill,type = 1:place Tiles,type = 2:place Walls
        /// </summary>
        /// <param name="Shapepath"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="type"></param>
        public static void ShapeTile(string Shapepath, int a, int b, int type)
        {
            var imageData = ImageReader.Read<SixLabors.ImageSharp.PixelFormats.Rgb24>("Everglow/Sources/Modules/YggdrasilModule/WorldGeneration/" + Shapepath);
            imageData.ProcessPixelRows(accessor =>
            {
                for (int y = 0; y < accessor.Height; y++)
                {
                    var pixelRow = accessor.GetRowSpan(y);
                    for (int x = 0; x < pixelRow.Length; x++)
                    {
                        ref var pixel = ref pixelRow[x];
                        Tile tile = Main.tile[x + a, y + b];
                        switch (type)//21������
                        {
                            case 0:
                                if (pixel.R == 255 && pixel.G == 0 && pixel.B == 0)
                                {
                                    if (tile.TileType != 21 && Main.tile[x + a, y + b - 1].TileType != 21)
                                    {
                                        tile.ClearEverything();
                                    }
                                }
                                break;
                            case 1:
                                //���ŵ�
                                if (pixel.R == 44 && pixel.G == 40 && pixel.B == 37)//ʯ������ľ
                                {
                                    tile.TileType = (ushort)ModContent.TileType<StoneScaleWood>();
                                    tile.HasTile = true;
                                }
                                if (pixel.R == 155 && pixel.G == 173 && pixel.B == 183)//��п�
                                {
                                    tile.TileType = (ushort)ModContent.TileType<CyanVineStone>();
                                    tile.HasTile = true;
                                }
                                if (pixel.R == 195 && pixel.G == 217 && pixel.B == 229)//����п�
                                {
                                    YggdrasilUtils.PlaceFrameImportantTiles(x + a,y + b,5,4, ModContent.TileType<CyanVineOre>(), Main.rand.Next(4) * 90);
                                }
                                if (pixel.R == 31 && pixel.G == 26 && pixel.B == 45)//������
                                {
                                    tile.TileType = (ushort)ModContent.TileType<DarkMud>();
                                    tile.HasTile = true;
                                }

                                //��̦����
                                if (pixel.R == 82 && pixel.G == 62 && pixel.B == 44)//����ľ
                                {
                                    tile.TileType = (ushort)ModContent.TileType<DragonScaleWood>();
                                    tile.HasTile = true;
                                }
                                if (pixel.R == 81 && pixel.G == 107 && pixel.B == 18)//��̦޺
                                {
                                    tile.TileType = (ushort)ModContent.TileType<OldMoss>();
                                    tile.HasTile = true;
                                }
                                if (pixel.R == 0 && pixel.G == 0 && pixel.B == 255)
                                {
                                    tile.LiquidType = LiquidID.Water;
                                    tile.LiquidAmount = 200;
                                    tile.HasTile = false;
                                }



                                //쫷��Թ�
                                if (pixel.R == 65 && pixel.G == 84 && pixel.B == 63)//�����
                                {
                                    tile.TileType = (ushort)ModContent.TileType<CyanWindGranite>();
                                    tile.HasTile = true;
                                }
                                break;
                            case 2:
                                if (pixel.R == 24 && pixel.G == 0 && pixel.B == 0)
                                {
                                    if (tile.TileType != 21 && Main.tile[x + a, y + b - 1].TileType != 21)
                                    {
                                        tile.WallType = (ushort)ModContent.WallType<StoneDragonScaleWoodWall>();
                                    }
                                }
                                break;
                            case 3://���ŵ�����
                                if (pixel.R == 121 && pixel.G == 5 && pixel.B == 255)//FolkHouseofChineseStyle TypeA  28x11
                                {
                                    QuickBuild(x, y, "YggdrasilTown/MapIOs/1FolkHouseofChineseStyleTypeA28x11.mapio");
                                }
                                if (pixel.R == 120 && pixel.G == 5 && pixel.B == 255)//FolkHouseofChineseStyle TypeB  28x11
                                {
                                    QuickBuild(x, y, "YggdrasilTown/MapIOs/1FolkHouseofChineseStyleTypeB28x11.mapio");
                                }

                                if (pixel.R == 122 && pixel.G == 5 && pixel.B == 255)//FolkHouseofWood��StoneStruture TypeA  28x11
                                {
                                    QuickBuild(x, y, "YggdrasilTown/MapIOs/2FolkHouseofWood��StoneStrutureTypeA28x11.mapio");
                                }
                                if (pixel.R == 123 && pixel.G == 5 && pixel.B == 255)//FolkHouseofWood��StoneStruture TypeB  28x11
                                {
                                    QuickBuild(x, y, "YggdrasilTown/MapIOs/2FolkHouseofWood��StoneStrutureTypeA28x11.mapio");
                                }

                                if (pixel.R == 124 && pixel.G == 5 && pixel.B == 255)//Smithy TypeA  22x8
                                {
                                    QuickBuild(x, y, "YggdrasilTown/MapIOs/3SmithyTypeA22x8.mapio");
                                }
                                if (pixel.R == 125 && pixel.G == 5 && pixel.B == 255)//Smithy TypeB  22x8
                                {
                                    QuickBuild(x, y, "YggdrasilTown/MapIOs/3SmithyTypeB22x8.mapio");
                                }

                                if (pixel.R == 126 && pixel.G == 5 && pixel.B == 255)//FolkHouseofWoodStruture TypeA  22x10
                                {
                                    QuickBuild(x, y, "YggdrasilTown/MapIOs/4FolkHouseofWoodStrutureTypeA22x10.mapio");
                                }
                                if (pixel.R == 127 && pixel.G == 5 && pixel.B == 255)//FolkHouseofWoodStruture TypeB  22x10
                                {
                                    QuickBuild(x, y, "YggdrasilTown/MapIOs/4FolkHouseofWoodStrutureTypeB22x10.mapio");
                                }
                                if (pixel.R == 128 && pixel.G == 5 && pixel.B == 255)//FolkHouseofWoodStruture TypeC  22x10
                                {
                                    QuickBuild(x, y, "YggdrasilTown/MapIOs/4FolkHouseofWoodStrutureTypeC22x10.mapio");
                                }
                                if (pixel.R == 129 && pixel.G == 5 && pixel.B == 255)//FolkHouseofWoodStruture TypeD  22x10
                                {
                                    QuickBuild(x, y, "YggdrasilTown/MapIOs/4FolkHouseofWoodStrutureTypeD22x10.mapio");
                                }

                                if (pixel.R == 130 && pixel.G == 5 && pixel.B == 255)//FolkHouseofWoodStruture TypeA  23x13
                                {
                                    QuickBuild(x, y, "YggdrasilTown/MapIOs/5TwoStoriedFolkHouseTypeA23x13.mapio");
                                }
                                if (pixel.R == 131 && pixel.G == 5 && pixel.B == 255)//FolkHouseofWoodStruture TypeB  23x13
                                {
                                    QuickBuild(x, y, "YggdrasilTown/MapIOs/5TwoStoriedFolkHouseTypeB23x13.mapio");
                                }
                                if (pixel.R == 132 && pixel.G == 5 && pixel.B == 255)//FolkHouseofWoodStruture TypeC  23x13
                                {
                                    QuickBuild(x, y, "YggdrasilTown/MapIOs/5TwoStoriedFolkHouseTypeC23x13.mapio");
                                }

                                if (pixel.R == 133 && pixel.G == 5 && pixel.B == 255)//Church 80x51
                                {
                                    QuickBuild(x, y, "YggdrasilTown/MapIOs/Church80x51.mapio");
                                }
                                break;
                        }
                    }
                }
            });   
        }
        public static void QuickBuild(int x, int y, string Path)
        {
            MapIO mapIO = new MapIO(x, y);

            mapIO.Read(Everglow.Instance.GetFileStream("Sources/Modules/YggdrasilModule/" + Path));

            var it = mapIO.GetEnumerator();
            while (it.MoveNext())
            {
                WorldGen.SquareTileFrame(it.CurrentCoord.X, it.CurrentCoord.Y);
                WorldGen.SquareWallFrame(it.CurrentCoord.X, it.CurrentCoord.Y);
            }
        }
        /// <summary>
        /// ���������
        /// </summary>
        public static void BuildtheTreeWorld()
        {
            Main.statusText = "YggdrasilStart";
            ShapeTile("Tree.bmp", 0, 0, 1);
            Main.statusText = "YggdrasilWall";
            ShapeTile("TreeWall.bmp", 0, 0, 2);
            SmoothTile();

            Main.statusText = "YggdrasilTown";
            ShapeTile("Tree.bmp", 0, 0, 3);
        }
        private static void SmoothTile(int a = 0, int b = 0, int c = 0, int d = 0)
        {
            for (int x = 20 + b; x < 980 - d; x += 1)
            {
                for (int y = 20 + a; y < 11980 - c; y += 1)
                {
                
                    Tile.SmoothSlope(x + a, y + b, false);
                    WorldGen.TileFrame(x + a, y + b, true, false);
                    WorldGen.SquareWallFrame(x + a, y + b, true);
                }
            }
        }
    }
}


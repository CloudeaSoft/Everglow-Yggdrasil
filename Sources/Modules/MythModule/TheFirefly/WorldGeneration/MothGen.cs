using Everglow.Sources.Modules.MythModule.Common;
using System.Drawing;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria.ModLoader.IO;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Everglow.Sources.Modules.MythModule.TheFirefly.WorldGeneration
{
    //TODO ����С�������Ժ���������һ��ModPlayer����
    public class MothLandSyncPlayer : ModPlayer
    {
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            if(newPlayer)
            {
                //����ҽ��������Ƿ�������
                Everglow.PacketResolver.Send(new MothPositionPacket());
            }
        }
    }

    public class MothLand : ModSystem
    {
        private class MothLandGenPass : GenPass
        {
            public MothLandGenPass() : base("MothLand", 500)
            {
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                BuildMothCave();
            }
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight) => tasks.Add(new MothLandGenPass());
        /// <summary>
        /// ������������
        /// </summary>
        public int fireflyCenterX = 2000;
        public int fireflyCenterY = 500;

        public override void SaveWorldData(TagCompound tag)
        {
            tag["FIREFLYcenterX"] = fireflyCenterX;
            tag["FIREFLYcenterY"] = fireflyCenterY;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            fireflyCenterX = tag.GetAsInt("FIREFLYcenterX");
            fireflyCenterY = tag.GetAsInt("FIREFLYcenterY");
        }
        
        /// <summary>
        /// type = 0:Kill,type = 1:place Tiles,type = 2:place Walls
        /// </summary>
        /// <param name="Shapepath"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="type"></param>
        public static void ShapeTile(string Shapepath, int a, int b, int type)
        {
            if (!OperatingSystem.IsWindows())
            {
                throw new Exception("Windows�޶�");
            }
            using Stream Img = Everglow.Instance.GetFileStream("Sources/Modules/MythModule/TheFirefly/WorldGeneration/" + Shapepath);
            Bitmap cocoon = new Bitmap(Img);
            for (int y = 0; y < cocoon.Height; y += 1)
            {
                for (int x = 0; x < cocoon.Width; x += 1)
                {
                    Tile tile = Main.tile[x + a, y + b];
                    switch (type)//21������
                    {
                        case 0:
                            if (CheckColor(cocoon.GetPixel(x, y), new Vector4(255, 0, 0, 255)))
                            {
                                if (tile.TileType != 21 && Main.tile[x + a, y + b - 1].TileType != 21)
                                {
                                    tile.ClearEverything();
                                }
                            }
                            break;
                        case 1:
                            if (CheckColor(cocoon.GetPixel(x, y), new Vector4(56, 48, 61, 255)))
                            {
                                if (tile.TileType != 21 && Main.tile[x + a, y + b - 1].TileType != 21)
                                {
                                    tile.TileType = (ushort)ModContent.TileType<Tiles.DarkCocoon>();
                                    tile.HasTile = true;
                                }
                            }
                            if (CheckColor(cocoon.GetPixel(x, y), new Vector4(0, 0, 255, 255)))
                            {
                                if (tile.TileType != 21 && Main.tile[x + a, y + b - 1].TileType != 21)
                                {
                                    tile.LiquidType = LiquidID.Water;
                                    tile.LiquidAmount = 200;
                                    tile.HasTile = false;
                                    //WorldGen.PlaceLiquid(x, y, byte.MaxValue, 255);
                                }
                            }
                            break;
                        case 2:
                            if (CheckColor(cocoon.GetPixel(x, y), new Vector4(0, 0, 5, 255)))
                            {
                                if (tile.TileType != 21 && Main.tile[x + a, y + b - 1].TileType != 21)
                                {
                                    tile.WallType = (ushort)ModContent.WallType<Walls.DarkCocoonWall>();
                                }
                            }
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// ������ө֮��
        /// </summary>
        public static void BuildMothCave()
        {
            Point16 AB = CocoonPos();
            int a = AB.X;
            int b = AB.Y;
            MothLand mothLand = ModContent.GetInstance<MothLand>();
            mothLand.fireflyCenterX = a + 140;
            mothLand.fireflyCenterY = b + 140;
            ShapeTile("CocoonKill.bmp", a, b, 0);
            ShapeTile("Cocoon.bmp", a, b, 1);
            ShapeTile("CocoonWall.bmp", a, b, 2);
            SmoothMothTile(a, b);
        }
        private static int GetCrash(int PoX, int PoY)
        {
            int CrashCount = 0;
            ushort[] DangerTileType = new ushort[]
            {
                41,//������ש
                43,//�̵���ש
                44,//�۵���ש
                48,//���
                49,//ˮ����
                50,//��
                137,//�������
                226,//����ʯש
                232,//ľ��
                237,//�����̳
                481,//��������ש
                482,//���̵���ש
                483//��۵���ש
            };
            for (int x = -256; x < 257; x += 8)
            {
                for (int y = -128; y < 129; y += 8)
                {
                    if (Array.Exists(DangerTileType, Ttype => Ttype == Main.tile[x + PoX, y + PoY].TileType))
                    {
                        CrashCount++;
                    }
                }
            }
            return CrashCount;
        }

        private static int GetMergeToJungle(int PoX, int PoY)
        {
            int CrashCount = 0;
            ushort[] MustHaveTileType = new ushort[]
            {
                60,//���ֲݷ���
                61,//���ֲ�
                62,//������
                74,//�ߴ���ֲ�
                233//���ֻ�
            };
            for (int x = -256; x < 257; x += 8)
            {
                for (int y = -128; y < 129; y += 8)
                {
                    if (Array.Exists(MustHaveTileType, Ttype => Ttype == Main.tile[x + PoX, y + PoY].TileType))
                    {
                        CrashCount++;
                    }
                }
            }
            return CrashCount;
        }
        /// <summary>
        /// ��ȡһ������ԭ����γ�ͻ�ĵ�
        /// </summary>
        /// <returns></returns>
        private static Point16 CocoonPos()
        {
            int PoX = Main.rand.Next(300, Main.maxTilesX - 600);
            int PoY = Main.rand.Next(500, Main.maxTilesY - 700);

            while (GetCrash(PoX, PoY) > 0 || GetMergeToJungle(PoX, PoY) <= 10)
            {
                PoX = Main.rand.Next(300, Main.maxTilesX - 600);
                PoY = Main.rand.Next(500, Main.maxTilesY - 700);
            }
            return new Point16(PoX, PoY);
        }
        private static void SmoothMothTile(int a, int b)
        {
            for (int y = 0; y < 256; y += 1)
            {
                for (int x = 0; x < 512; x += 1)
                {
                    if (Main.tile[x + a, y + b].TileType == (ushort)ModContent.TileType<Tiles.DarkCocoon>())
                    {
                        Tile.SmoothSlope(x + a, y + b, false);
                        WorldGen.TileFrame(x + a, y + b, true, false);
                    }
                    else
                    {
                        WorldGen.TileFrame(x + a, y + b, true, false);
                    }
                    WorldGen.SquareWallFrame(x + a, y + b, true);
                }
            }
        }
        /// <summary>
        /// �ж���ɫ�Ƿ��Ǻ�
        /// </summary>
        /// <param name="c0"></param>
        /// <param name="RGBA"></param>
        /// <returns></returns>
        private static bool CheckColor(System.Drawing.Color c0, Vector4 RGBA)
        {
            Vector4 v0 = new Vector4(c0.R, c0.G, c0.B, c0.A);
            return v0 == RGBA;
        }
    }
}


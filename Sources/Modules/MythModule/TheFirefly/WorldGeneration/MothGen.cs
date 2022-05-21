using System.Drawing;
using Terraria.DataStructures;

namespace Everglow.Sources.Modules.MythModule.TheFirefly.WorldGeneration
{
    public class MothLand : ModSystem
    {
        public override void PostWorldGen()
        {
            BuildMothCave();
        }
        /// <summary>
        /// type = 0:Kill,type = 1:place Tiles,type = 2:place Walls
        /// </summary>
        /// <param name="Shapepath"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="type"></param>
        public void ShapeTile(string Shapepath, int a, int b, int type)
        {
            using (Stream Img = Everglow.Instance.GetFileStream("Sources/Modules/MythModule/TheFirefly/WorldGeneration/" + Shapepath))
            {
                Bitmap cocoon = new Bitmap(Img);
                for (int y = 0; y < cocoon.Height; y += 1)
                {
                    for (int x = 0; x < cocoon.Width; x += 1)
                    {
                        switch (type)
                        {
                            case 0:
                                if (CheckColor(cocoon.GetPixel(x, y), new Vector4(255, 0, 0, 255)))
                                {
                                    if (Main.tile[x + a, y + b].TileType != 21 && Main.tile[x + a, y + b - 1].TileType != 21)
                                    {
                                        Main.tile[x + a, y + b].ClearEverything();
                                    }
                                }
                                break;
                            case 1:
                                if (CheckColor(cocoon.GetPixel(x, y), new Vector4(56, 48, 61, 255)))
                                {
                                    if (Main.tile[x + a, y + b].TileType != 21 && Main.tile[x + a, y + b - 1].TileType != 21)
                                    {
                                        Main.tile[x + a, y + b].TileType = (ushort)ModContent.TileType<Tiles.DarkCocoon>();
                                        ((Tile)Main.tile[x + a, y + b]).HasTile = true;
                                    }
                                }
                                break;
                            case 2:
                                if (CheckColor(cocoon.GetPixel(x, y), new Vector4(0, 0, 5, 255)))
                                {
                                    if (Main.tile[x + a, y + b].TileType != 21 && Main.tile[x + a, y + b - 1].TileType != 21)
                                    {
                                        Main.tile[x + a, y + b].WallType = (ushort)ModContent.WallType<Walls.DarkCocoonWall>();
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// ������ө֮��
        /// </summary>
        public void BuildMothCave()
        {
            Point16 AB = CocoonPos();
            int a = 800;
            int b = 600;
            ShapeTile("CocoonKill.bmp", a, b, 0);
            ShapeTile("Cocoon.bmp", a, b, 1);
            ShapeTile("CocoonWall.bmp", a, b, 2);
        }
        private int GetCrash(int PoX, int PoY)
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
                    if (Array.Exists<ushort>(DangerTileType, Ttype => Ttype == Main.tile[x + PoX, y + PoY].TileType))
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
        private Point16 CocoonPos()
        {
            int PoX = Main.rand.Next(300, Main.maxTilesX - 600);
            int PoY = Main.rand.Next(400, Main.maxTilesY - 300);

            while (GetCrash(PoX, PoY) > 0)
            {
                PoX = Main.rand.Next(300, Main.maxTilesX - 600);
                PoY = Main.rand.Next(400, Main.maxTilesY - 300);
            }
            return new Point16(PoX, PoY);
        }
        private void SmoothMothTile(int a, int b)
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
        private bool CheckColor(System.Drawing.Color c0, Vector4 RGBA)
        {
            Vector4 v0 = new Vector4(c0.R, c0.G, c0.B, c0.A);
            return v0 == RGBA;
        }
    }
}


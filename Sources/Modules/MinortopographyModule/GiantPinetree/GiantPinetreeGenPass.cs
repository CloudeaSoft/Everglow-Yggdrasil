using Terraria.DataStructures;
using Terraria.IO;
using Everglow.Sources.Modules.MinortopographyModule.GiantPinetree.TilesAndWalls;
using Terraria.WorldBuilding;

namespace Everglow.Sources.Modules.MinortopographyModule.GiantPinetree
{
    public class GiantPinetree : ModSystem
    {
        private class GiantPinetreeGenPass : GenPass
        {
            public GiantPinetreeGenPass() : base("GiantPinetree", 500)
            {
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                //Todo:���룺����޴��ѩ��
                Main.statusText = Terraria.Localization.Language.GetTextValue("Mods.Everlow.Common.WorldSystem.BuildMothCave");
                BuildGiantPinetree();
            }
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight) => tasks.Add(new GiantPinetreeGenPass());
        /// <summary>
        /// ����޴��ѩ��
        /// </summary>
        public static void BuildGiantPinetree()
        {
            
            Point16 CenterPoint = RandomPointInSurfaceSnow();
            int X0 = CenterPoint.X;
            int Y0 = CenterPoint.Y - 26;//����26��

            if (Main.snowBG[2] == 260)//�����������£�����������δ������ɵ�����
            {

            }
            float Width = Main.rand.NextFloat(24f, 32f);//���ҡ���
            int j = 0;
            for (int a = -3; a <= 3; a++)
            {
                GenerateRoots(new Point16(X0, Y0), 0, a / 2f);//�����������
            }
            while (Width > 0)
            {
                j--;
                if (j + Y0 >= Main.maxTilesY - 10 || j + Y0 <= 10 || -10 + X0 <= 10 || 10 + X0 >= Main.maxTilesX + 10)//��ֹ����
                {
                    break;
                }
                for (int i = (int)-Width; i <= (int)Width; i++)
                {
                    Tile tile = Main.tile[i + X0, j + Y0];
                    if (i <= -Width + 4 || i >= Width - 4)
                    {
                        tile.TileType = TileID.PineTree;
                        tile.HasTile = true;
                    }
                    if (i > -Width + 2 && i < Width - 2)
                    {
                        tile.WallType = (ushort)ModContent.WallType<PineLeavesWall>();
                    }
                }
                Width -= (float)(Math.Sin(j * 0.8) * 0.5 + 0.2);//��������һ��һ���Ч��
            }
            GenerateRoots(new Point16(X0, Y0), 3.14159f, 3.14159f, false);//����������������Ϊ����
        }

        /// <summary>
        /// �����ϵ,��ʼ�Ƕ�=0ʱ����,��ϵ������ʼλ������ʼ�Ƕȷ���,����ת��Ŀ��Ƕ�,�����Ȼ����,��ϵ����ĩβ���ٷ���һ�ι���
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="startRotation"></param>
        /// <param name="trendRotation"></param>
        /// <param name="naturalCurve"></param>
        public static void GenerateRoots(Point16 Start, float startRotation = 0, float trendRotation = 0, bool naturalCurve = true)
        {
            int X0 = Start.X;
            int Y0 = Start.Y;
            float Width = Main.rand.NextFloat(8f, 10f);//���ҡ���
            Vector2 IJ = new Vector2(0, 0);
            Vector2 RootVelocity = new Vector2(0, 1).RotatedBy(startRotation);//��ϵ��ǰ�ٶ�
            Vector2 RootTrendVelocity = new Vector2(0, 1).RotatedBy(trendRotation);//��ϵ�ȶ������ٶ�
            float omega = Main.rand.NextFloat(-0.2f, 0.2f);//ĩ����ת�Ľ��ٶ�
            if (!naturalCurve)//�����ֹ����Ȼ��ת,���ٶ�=0
            {
                omega = 0;
            }
            float StartToRotatedByOmega = Main.rand.NextFloat(1.81f, 3.62f);//����ĩ�˵���ʼλ�ã�������ʣ����ͳ��
            while (Width > 0)
            {
                for (int a = (int)-Width; a <= (int)Width; a++)
                {
                    Vector2 RootBuildingPosition = IJ + a * (RootVelocity).RotatedBy(MathHelper.PiOver2) * 0.6f;
                    int i = (int)(RootBuildingPosition.X);
                    int j = (int)(RootBuildingPosition.Y);
                    if (j + Y0 >= Main.maxTilesY - 10 || j + Y0 <= 10 || -10 + X0 <= 10 || 10 + X0 >= Main.maxTilesX + 10)//��ֹ����
                    {
                        break;
                    }
                    Tile tile = Main.tile[i + X0, j + Y0];
                    if (a <= -Width + 4 || a >= Width - 4)
                    {
                        if (tile.WallType != ModContent.WallType<PineWoodWall>())//��ֹ�����黥���غ�
                        {
                            tile.TileType = (ushort)ModContent.TileType<PineWood>();
                            tile.HasTile = true;
                        }

                    }
                    else
                    {
                        tile.HasTile = false;
                        tile.LiquidAmount = 0;
                    }
                    if (a > -Width + 2 && a < Width - 2)
                    {
                        tile.WallType = (ushort)ModContent.WallType<PineWoodWall>();
                    }
                }
                IJ += RootVelocity;
                if (Width > StartToRotatedByOmega)//û��������ĩ��
                {
                    RootVelocity = RootVelocity * 0.95f + RootTrendVelocity * 0.05f;
                }
                else//�Ѿ�������ĩ��
                {
                    RootVelocity = RootVelocity.RotatedBy(omega * (StartToRotatedByOmega - Width) / StartToRotatedByOmega);
                }
                if (naturalCurve)//ֻ����Ȼ�����Żᵼ����������
                {
                    //��������Ҳ��Ӱ���ϵ,�����ж���ϵ���ճ̶�
                    int AroundTileCount = 0;//�����ж���Χ���ڷ�����������ƶ����ճ̶ȣ����ڷ���Խ��Խ����
                    for (int b = 0; b < 12; b++)
                    {
                        Vector2 RootBuildingPosition = IJ + 3 * (RootVelocity).RotatedBy(b / 6d * Math.PI);
                        int i = (int)(RootBuildingPosition.X);
                        int j = (int)(RootBuildingPosition.Y);
                        Tile tile = Main.tile[i + X0, j + Y0];
                        if (tile.HasTile || tile.WallType == (ushort)ModContent.WallType<PineWoodWall>()/*��һ����Ϊ�˷�ֹ�Լ������Լ�*/)
                        {
                            AroundTileCount++;
                        }
                    }
                    if (AroundTileCount < 6)
                    {
                        RootVelocity += new Vector2(0, (6 - AroundTileCount) / 16f);//������Ȼ�´�
                        RootVelocity = Vector2.Normalize(RootVelocity);//������λ����
                        Width += (6 - AroundTileCount) / 50f;//��ֹ�½����̸�ϵ��������
                    }
                    else if (AroundTileCount > 9)
                    {
                        Width -= (AroundTileCount - 9) / 20f;//��Χ���̫�࣬�����������ӿ�����
                    }
                }
                Width -= 0.1f;
                if(Width < 1.8f)//̫ϸ�ˣ��Ƶ���
                {
                    break;
                }
            }
        }
        /// <summary>
        /// ��ѩ�ر��������ȡһ��
        /// </summary>
        /// <returns></returns>
        public static Point16 RandomPointInSurfaceSnow()
        {
            List<Point16> AimPoint = new List<Point16>();
            int Jmin = Main.maxTilesY - 100;
            for (int i = 33; i < Main.maxTilesX - 34; i += 33)
            {
                for (int j = 12; j < Main.maxTilesY - 100; j += 6)
                {
                    Tile tile = Main.tile[i, j];
                    if (tile.TileType == TileID.SnowBlock && tile.HasTile)
                    {
                        AimPoint.Add(new Point16(i, j));
                        if(j < Jmin)
                        {
                            Jmin = j;
                        }
                        break;
                    }
                }
            }
            List<Point16> newAimPoint = new List<Point16>();
            foreach (Point16 point in AimPoint)
            {
                if(point.Y <= Jmin + 30)
                {
                    newAimPoint.Add(point);
                }
            }
            return newAimPoint[Main.rand.Next(newAimPoint.Count)];
        }
    }
}


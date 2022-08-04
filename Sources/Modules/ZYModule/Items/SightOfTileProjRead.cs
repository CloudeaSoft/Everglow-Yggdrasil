﻿using Everglow.Sources.Modules.MythModule.Common;
using Everglow.Sources.Commons.Function.Vertex;
using Everglow.Sources.Modules.MythModule.Bosses.CorruptMoth.Dusts;
using Everglow.Sources.Modules.MythModule.TheFirefly.Dusts;
using Everglow.Sources.Modules.ZYModule.Commons.Function.MapIO;
using Terraria.GameContent;

namespace Everglow.Sources.Modules.ZYModule.Items
{
    internal class SightOfTileProjRead : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10000;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }
        int Left = 0;
        int Up = 0;
        int Down = 0;
        int Right = 0;
        private void UpdateFourCoord()
        {
            MapIO mapIO = new MapIO((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16));
            int Count = 0;
            while (File.Exists("MapTiles" + Count.ToString() + ".mapio"))
            {
                Count++;
            }
            if (Count == 0)
            {
                return;
            }
            Count -= 1;
            int MapWidth = mapIO.ReadWidth("MapTiles" + Count.ToString() + ".mapio");
            int MapHeight = mapIO.ReadHeight("MapTiles" + Count.ToString() + ".mapio");
            int HalfMapWidth = MapWidth / 2;
            int HalfMapHeight = MapHeight / 2;
            int MouseWX = (int)(Main.MouseWorld.X / 16f);
            int MouseWY = (int)(Main.MouseWorld.Y / 16f);
            int X1 = MouseWX - HalfMapWidth;
            int X2 = MouseWX - HalfMapWidth + MapWidth - 1;
            int Y1 = MouseWY - HalfMapHeight;
            int Y2 = MouseWY - HalfMapHeight + MapHeight - 1;

            Left = X1;
            Right = X2;
            Up = Y1;
            Down = Y2;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.itemTime = 5;
            player.itemAnimation = 5;
            Projectile.position = player.MountedCenter - new Vector2(17);
            player.heldProj = Projectile.whoAmI;
            UpdateFourCoord();
            if (Main.mouseLeft)
            {
                Projectile.timeLeft = 5;
            }
            if(Main.mouseRight)
            {
                Projectile.Kill();
            }
        }
        public void DrawDoubleLine(Vector2 StartPos, Vector2 EndPos, Color color1, Color color2)
        {
            float Wid = 1f;
            Vector2 Width = Vector2.Normalize(StartPos - EndPos).RotatedBy(Math.PI / 2d) * Wid;

            List<Vertex2D> vertex2Ds = new List<Vertex2D>();

            for (int x = 0; x < 3; x++)
            {
                vertex2Ds.Add(new Vertex2D(StartPos + Width + new Vector2(x / 3f).RotatedBy(x), color1, new Vector3(0, 0, 0)));
                vertex2Ds.Add(new Vertex2D(EndPos + Width + new Vector2(x / 3f).RotatedBy(x), color2, new Vector3(0, 0, 0)));
                vertex2Ds.Add(new Vertex2D(StartPos - Width + new Vector2(x / 3f).RotatedBy(x), color1, new Vector3(0, 0, 0)));

                vertex2Ds.Add(new Vertex2D(EndPos + Width + new Vector2(x / 3f).RotatedBy(x), color2, new Vector3(0, 0, 0)));
                vertex2Ds.Add(new Vertex2D(EndPos - Width + new Vector2(x / 3f).RotatedBy(x), color2, new Vector3(0, 0, 0)));
                vertex2Ds.Add(new Vertex2D(StartPos - Width + new Vector2(x / 3f).RotatedBy(x), color1, new Vector3(0, 0, 0)));
            }


            Main.graphics.GraphicsDevice.Textures[0] = TextureAssets.MagicPixel.Value;
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertex2Ds.ToArray(), 0, vertex2Ds.Count / 3);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];

            Vector2 Vdr = Main.MouseWorld - Projectile.Center;
            
            Vdr = Vdr / Vdr.Length() * 7;

            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (float)(Math.Atan2(Vdr.Y, Vdr.X) - Math.PI / 2d));
            Texture2D t = ModContent.Request<Texture2D>("Everglow/Sources/Modules/ZYModule/Items/SightOfTileProjRead").Value;
            Color color = Lighting.GetColor((int)Projectile.Center.X / 16, (int)((double)Projectile.Center.Y / 16.0));
            SpriteEffects S = SpriteEffects.None;
            if (Math.Sign(Vdr.X) == -1)
            {
                player.direction = -1;
            }
            else
            {
                player.direction = 1;
            }

            Main.spriteBatch.Draw(t, player.MountedCenter - Main.screenPosition + Vdr * 5f, null, color, (float)(Math.Atan2(Vdr.Y, Vdr.X) + Math.PI / 4d), t.Size() / 2f, Projectile.scale, S, 0f);
            Vector2 ULInt = new Vector2(Left, Up) * 16 + new Vector2(0);
            Vector2 DRInt = new Vector2(Right, Down) * 16 + new Vector2(0);
            Vector2 DLInt = new Vector2(Left, Down) * 16 + new Vector2(0);
            Vector2 URInt = new Vector2(Right, Up) * 16 + new Vector2(0);

            URInt.X += 16;
            DLInt.Y += 16;
            DRInt.X += 16;
            DRInt.Y += 16;
            ULInt += new Vector2(1, 1);
            URInt += new Vector2(-1, 1);
            DLInt += new Vector2(1, -1);
            DRInt += new Vector2(-1, -1);
            DrawDoubleLine(player.MountedCenter - Main.screenPosition + Vdr * 8f, ULInt - Main.screenPosition, new Color(40, 240, 255, 100), new Color(0, 0, 65, 0));
            DrawDoubleLine(player.MountedCenter - Main.screenPosition + Vdr * 8f, URInt - Main.screenPosition, new Color(40, 240, 255, 100), new Color(0, 0, 65, 0));
            DrawDoubleLine(player.MountedCenter - Main.screenPosition + Vdr * 8f, DLInt - Main.screenPosition, new Color(40, 240, 255, 100), new Color(0, 0, 65, 0));
            DrawDoubleLine(player.MountedCenter - Main.screenPosition + Vdr * 8f, DRInt - Main.screenPosition, new Color(40, 240, 255, 100), new Color(0, 0, 65, 0));

            DrawNinePiecesForTiles(Left, Right, Up, Down);
            return false;
        }
        private void DrawNinePiecesForTiles(int LeftX, int RightX, int UpY, int DownY)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            Texture2D t = ModContent.Request<Texture2D>("Everglow/Sources/Modules/ZYModule/Items/Rectangle").Value;
            Color baseColor = new Color(0, 30, 120, 180);
            if (LeftX == RightX)
            {
                if (UpY == DownY)
                {
                    int ScPosX = (int)Main.screenPosition.X;
                    int ScPosY = (int)Main.screenPosition.Y;
                    Rectangle source = new Rectangle(0, 0, 8, 8);
                    Main.spriteBatch.Draw(t, new Rectangle(LeftX * 16 - ScPosX, UpY * 16 - ScPosY, 8, 8), source, GetTileColor(LeftX, UpY, baseColor));
                    source = new Rectangle(40, 0, 8, 8);
                    Main.spriteBatch.Draw(t, new Rectangle(LeftX * 16 - ScPosX + 8, UpY * 16 - ScPosY, 8, 8), source, GetTileColor(LeftX, UpY, baseColor));
                    source = new Rectangle(0, 40, 8, 8);
                    Main.spriteBatch.Draw(t, new Rectangle(LeftX * 16 - ScPosX, UpY * 16 - ScPosY + 8, 8, 8), source, GetTileColor(LeftX, UpY, baseColor));
                    source = new Rectangle(40, 40, 8, 8);
                    Main.spriteBatch.Draw(t, new Rectangle(LeftX * 16 - ScPosX + 8, UpY * 16 - ScPosY + 8, 8, 8), source, GetTileColor(LeftX, UpY, baseColor));
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    return;
                }
                for (int y = UpY; y < DownY + 1; y++)
                {
                    int ScPosX = (int)Main.screenPosition.X;
                    int ScPosY = (int)Main.screenPosition.Y;
                    Rectangle source = new Rectangle(0, 16, 8, 16);
                    Rectangle source2 = new Rectangle(40, 16, 8, 16);
                    if (y == UpY)
                    {
                        source.Y = 0;
                        source2.Y = 0;
                    }
                    if (y == DownY)
                    {
                        source.Y = 32;
                        source2.Y = 32;
                    }
                    Main.spriteBatch.Draw(t, new Rectangle(LeftX * 16 - ScPosX, y * 16 - ScPosY, 8, 16), source, GetTileColor(LeftX, y, baseColor));
                    Main.spriteBatch.Draw(t, new Rectangle(LeftX * 16 - ScPosX + 8, y * 16 - ScPosY, 8, 16), source2, GetTileColor(LeftX, y, baseColor));
                }
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                return;
            }
            if (UpY == DownY)
            {
                for (int x = LeftX; x < RightX + 1; x++)
                {
                    int ScPosX = (int)Main.screenPosition.X;
                    int ScPosY = (int)Main.screenPosition.Y;
                    Rectangle source = new Rectangle(16, 0, 16, 8);
                    Rectangle source2 = new Rectangle(16, 40, 16, 8);
                    if (x == LeftX)
                    {
                        source.X = 0;
                        source2.X = 0;
                    }
                    if (x == RightX)
                    {
                        source.X = 32;
                        source2.X = 32;
                    }
                    Main.spriteBatch.Draw(t, new Rectangle(x * 16 - ScPosX, UpY * 16 - ScPosY, 16, 8), source, GetTileColor(x, UpY, baseColor));
                    Main.spriteBatch.Draw(t, new Rectangle(x * 16 - ScPosX, UpY * 16 - ScPosY + 8, 16, 8), source2, GetTileColor(x, UpY, baseColor));
                }
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                return;
            }


            for (int x = LeftX; x < RightX + 1; x++)
            {
                for (int y = UpY; y < DownY + 1; y++)
                {
                    int ScPosX = (int)Main.screenPosition.X;
                    int ScPosY = (int)Main.screenPosition.Y;
                    Rectangle source = new Rectangle(16, 16, 16, 16);
                    if (x == LeftX)
                    {
                        source.X = 0;
                    }
                    if (x == RightX)
                    {
                        source.X = 32;
                    }
                    if (y == UpY)
                    {
                        source.Y = 0;
                    }
                    if (y == DownY)
                    {
                        source.Y = 32;
                    }
                    Main.spriteBatch.Draw(t, new Rectangle(x * 16 - ScPosX, y * 16 - ScPosY, 16, 16), source, GetTileColor(x, y, baseColor));
                }
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
        }
        private Color GetTileColor(int i, int j, Color baseColor)
        {
            if (Main.tile[i, j].HasTile)
            {
                if (Main.tile[i, j].WallType > 0)
                {
                    return new Color(255, 255, 0, 200);
                }
                return new Color(100, 100, 0, 10);
            }
            if (Main.tile[i, j].WallType > 0)
            {
                return new Color(255, 55, 0, 10);
            }

            return baseColor;
        }
        public override void Kill(int timeLeft)
        {
            if(timeLeft > 0)
            {
                return;
            }
            MapIO mapIO = new MapIO(Left, Up);
            int Count = 0;
            while (File.Exists("MapTiles" + Count.ToString() + ".mapio"))
            {
                Count++;
            }
            if(Count == 0)
            {
                return;
            }
            Count -= 1;

            mapIO.Read("MapTiles" + Count.ToString() + ".mapio");
            var it = mapIO.GetEnumerator();
            while (it.MoveNext())
            {
                WorldGen.SquareTileFrame(it.CurrentCoord.X, it.CurrentCoord.Y);
                WorldGen.SquareWallFrame(it.CurrentCoord.X, it.CurrentCoord.Y);
            }
            base.Kill(timeLeft);
        }
    }
}

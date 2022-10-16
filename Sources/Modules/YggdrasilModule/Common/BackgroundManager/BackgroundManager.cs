using Everglow.Sources.Commons.Function.ImageReader;
using Everglow.Sources.Commons.Function.Vertex;

namespace Everglow.Sources.Modules.YggdrasilModule.Common.BackgroundManager
{
    public class BackgroundManager
    {
        public class WorldBackground
        {
            public float Layer = 2;//���Ʋ�,����
            public int Priority = 0;//ͬ���»������ȼ�
            public Vector2 Center;//��������
            public Color Color;//��ɫ
            public Rectangle DrawRectangle;//������
            public bool Active;//�Ƿ���ƣ����ȼ����
            public Texture2D Texture;//ͼƬ
            public float scale = 1;//��С
        }
        public class BoardBackground : WorldBackground
        {
            public bool XClamp = false;
            public bool YClamp = true;
        }
        public class PointBackground : WorldBackground
        {
            public float Rotation = 0;
            public Vector2 Velocity;
            public float[] ai = new float[8];
            public void Update()
            {

            }
            public void SpecialDraw(SpriteBatch spriteBatch)
            {

            }
        }

        public static void QuickDrawBG(Texture2D tex, Rectangle drawArea, Color baseColor, int Ymin, int Ymax, bool Xclamp = false, bool Yclmap = true, bool ZoomMatrix = false)
        {

            Main.spriteBatch.End();
            if(ZoomMatrix)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.EffectMatrix);
            }
            else
            {
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }

            Effect bgW = YggdrasilContent.QuickEffect("Common/BackgroundManager/BackgroundXWarp");
            if (Xclamp && Yclmap)
            {
                bgW = YggdrasilContent.QuickEffect("Common/BackgroundManager/BackgroundXYClamp");
            }
            if (Xclamp && !Yclmap)
            {
                bgW = YggdrasilContent.QuickEffect("Common/BackgroundManager/BackgroundYWarp");
            }
            if (!Xclamp && !Yclmap)
            {
                bgW = YggdrasilContent.QuickEffect("Common/BackgroundManager/BackgroundXYWarp");
            }
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            bgW.Parameters["uTransform"].SetValue(projection);
            bgW.Parameters["uTime"].SetValue(0.34f);
            bgW.CurrentTechnique.Passes[0].Apply();

            //�����������ͼ���޵Ĳ���
            int DrawMaxY = Main.screenHeight;
            int DrawMinY = 0;
            float YSqueezeValueUp = 0f;
            float YSqueezeValueDown = 1f;
            if (Main.screenPosition.Y + Main.screenHeight > Ymax)
            {
                DrawMaxY = Ymax - (int)Main.screenPosition.Y;
                YSqueezeValueDown = (float)DrawMaxY / Main.screenHeight;
            }
            if (Main.screenPosition.Y < Ymin)
            {
                DrawMinY = Ymin - (int)Main.screenPosition.Y;
                YSqueezeValueUp = DrawMinY / (float)Main.screenHeight;
            }

            List<Vertex2D> CloseII = new List<Vertex2D>
            {
                new Vertex2D(new Vector2(0, DrawMinY), baseColor, new Vector3(drawArea.X / (float)tex.Width, (drawArea.Y + (drawArea.Height * YSqueezeValueUp)) / tex.Height, 0)),
                new Vertex2D(new Vector2(Main.screenWidth, DrawMinY), baseColor, new Vector3((drawArea.X + drawArea.Width) / (float)tex.Width, (drawArea.Y + drawArea.Height * YSqueezeValueUp)/ tex.Height, 0)),
                new Vertex2D(new Vector2(0, DrawMaxY), baseColor, new Vector3(drawArea.X / (float)tex.Width, (drawArea.Y + drawArea.Height * YSqueezeValueDown) / tex.Height, 0)),

                new Vertex2D(new Vector2(0, DrawMaxY), baseColor, new Vector3(drawArea.X / (float)tex.Width, (drawArea.Y + drawArea.Height * YSqueezeValueDown) / tex.Height, 0)),
                new Vertex2D(new Vector2(Main.screenWidth, DrawMinY), baseColor, new Vector3((drawArea.X + drawArea.Width) / (float)tex.Width, (drawArea.Y + drawArea.Height * YSqueezeValueUp) / tex.Height, 0)),
                new Vertex2D(new Vector2(Main.screenWidth, DrawMaxY), baseColor, new Vector3((drawArea.X + drawArea.Width) / (float)tex.Width, (drawArea.Y + drawArea.Height * YSqueezeValueDown) / tex.Height, 0))
            };
            if (CloseII.Count > 2)
            {
                Main.graphics.GraphicsDevice.Textures[0] = tex;
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, CloseII.ToArray(), 0, 2);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
    }
}
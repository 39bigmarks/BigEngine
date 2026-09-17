using BigEngine.Game;
using BigEngine.Types;
using Raylib_cs;

namespace BigEngine.Engine
{
    internal class Renderer
    {
        public Camera3D camera;
        public RenderTexture2D renderTexture;
        public Rectangle rec;

        public Renderer(Camera3D camera, Rectangle rec)
        {
            this.camera = camera;
            this.rec = new(rec.Position, new(rec.Width, -rec.Height));
        }

        public void Load()
        {
            renderTexture = Raylib.LoadRenderTexture((int)rec.Width, (int)-rec.Height);
        }

        public void DrawToRenderTexture()
        {
            Raylib.BeginTextureMode(renderTexture);
            Raylib.BeginMode3D(camera);

            Raylib.ClearBackground(FrameManager.currentFrame.clearColor);

            Raylib.DrawGrid(10, 1);

            ShaderSettings.UpdateShaders(camera);
            for (int i = 0; i < FrameManager.currentFrame.objects.Count; i++)
            {
                IObject obj = FrameManager.currentFrame.objects[i];
                obj.Update();
                obj.UpdateGizmos(camera);
                obj.UpdateShaders(camera);
            }

            Raylib.EndMode3D();
            Raylib.EndTextureMode();
        }

        public void Unload()
        {
            Raylib.UnloadRenderTexture(renderTexture);
        }
    }
}

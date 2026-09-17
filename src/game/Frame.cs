using BigEngine.Engine;
using BigEngine.Types;
using Raylib_cs;
using System.Numerics;

namespace BigEngine.Game
{
    internal class Frame
    {
        public string Name;
        public Color clearColor;
        public List<IObject> objects = [];
        public List<Camera3D> cameras = [];
        private List<Renderer> renderers = [];
        //public static Shader fisheyeShader = Raylib.LoadShader(null, "resources/shaders/post process/fisheye.glsl");
        //public static int timeLoc;

        // add destroy queue later

        public Frame(string name, Color clearColor)
        {
            Name = name;
            this.clearColor = clearColor;
        }

        public void Load()
        {
            //timeLoc = Raylib.GetShaderLocation(fisheyeShader, "time");

            if(cameras.Count > 0)
            {
                for (int i = 0; i < objects.Count; i++)
                    objects[i].Load();
            
                renderers = [ new Renderer(cameras[0], new Rectangle(new(0, 0), new(BigEngine.windowWidth, BigEngine.windowHeight))) ];
                for (int i = 0; i < renderers.Count; i++)
                    renderers[i].Load();
            }
        }

        public void Update()
        {
            if (cameras.Count != 0)
            {
                // render camera 0
                renderers[0].camera = cameras[0];
                renderers[0].DrawToRenderTexture();

                Raylib.BeginDrawing();
                
                //if(BigEngine.srgb) Raylib.BeginShaderMode(fisheyeShader);
                //Raylib.SetShaderValue(fisheyeShader, timeLoc, BigEngine.timer, ShaderUniformDataType.Float);
                Raylib.DrawTextureRec(renderers[0].renderTexture.Texture, renderers[0].rec, renderers[0].rec.Position, Color.White);
                //if(BigEngine.srgb) Raylib.EndShaderMode();
                Raylib.EndDrawing();
            }
            else
            {
                Raylib.DrawTextPro(Raylib.GetFontDefault(), "No cameras available.", new Vector2(BigEngine.windowWidth / 2 - Raylib.MeasureText("No cameras available.", 28) / 2, BigEngine.windowHeight / 2 - 28), Vector2.Zero, 0, 28, 0.5f, Color.White);
            }
        }
        
        public void SwitchToCamera(Camera3D camera)
        {
            cameras.Remove(camera);
            cameras.Insert(0, camera);
        }

        public void CreateCamera(Camera3D camera, bool makePrimary = false)
        {
            if (makePrimary)
            {
                cameras.Insert(0, camera);
                return;
            }

            cameras.Add(camera);
        }

        public void Unload()
        {
            for (int i = 0; i < objects.Count; i++)
                objects[i].Unload();

            for (int i = 0; i < renderers.Count; i++)
                renderers[i].Unload();
        }

        public bool Equals(Frame frame)
        {
            bool ret = true;
            ret &= Name == frame.Name;
            ret &= clearColor == frame.clearColor;
            ret &= objects == frame.objects;
            ret &= cameras == frame.cameras;
            return ret;
        }
    }
}
using BigEngine.Game;
using Raylib_cs;
using Model = BigEngine.Objects.Model;
using Object = BigEngine.Objects.Object;
using System.Numerics;

namespace BigEngine
{
    internal static class BigEngine
    {
        internal static int windowWidth = 1280;
        internal static int windowHeight = 720;

        internal static Material defaultMaterial;
        internal static Shader defaultShader;

        internal static void Main(string[] args)
        {
            Raylib.InitWindow(windowWidth, windowHeight, "BigEngine");

            //Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
            defaultMaterial = Raylib.LoadMaterialDefault();

            defaultShader = Raylib.LoadShader("resources/shaders/default/vert.vert", "resources/shaders/default/frag.frag");
            defaultMaterial.Shader = defaultShader;

            Raylib_cs.Model mdl = Raylib.LoadModel("resources/models/suzanne.obj");
            Object suzanne = new("Suzanne", []);
            suzanne.components.Add(new Model(mdl, suzanne.transform, defaultMaterial));

            FrameManager.currentFrame.Name = "Frame 1";
            FrameManager.currentFrame.clearColor = new(12, 12, 12);

            FrameManager.currentFrame.objects.Add(suzanne);
            FrameManager.CreateFrame(new("Frame 2", new(180, 12, 12), [suzanne]));
            FrameManager.CreateFrame(new("Frame 3", new(12, 12, 128), []));
            
            FrameManager.currentFrame.Start();

            while (!Raylib.WindowShouldClose())
            {
                if (Raylib.IsKeyPressed(KeyboardKey.One))
                    FrameManager.LoadFrame(FrameManager.frames[0]);

                if (Raylib.IsKeyPressed(KeyboardKey.Two))
                    FrameManager.LoadFrame(FrameManager.frames[1]);

                if (Raylib.IsKeyPressed(KeyboardKey.Three))
                    FrameManager.LoadFrame(FrameManager.frames[2]);


                Raylib.BeginDrawing();

                FrameManager.currentFrame.Update();

                Raylib.EndDrawing();
            }
            Raylib.UnloadShader(defaultShader);

            FrameManager.currentFrame.Unload();

            Raylib.CloseWindow();
        }
    }
}
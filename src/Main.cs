using BigEngine.Engine;
using BigEngine.Game;
using BigEngine.Types;
using Raylib_cs;
using System.Numerics;

namespace BigEngine
{
    internal static class BigEngine
    {
        public static int windowWidth = 1280;
        public static int windowHeight = 720;

        internal static void Main(string[] args)
        {
            Raylib.InitWindow(windowWidth, windowHeight, "BigEngine");

            GameObject suzanne = new("Suzanne", components: []);
            suzanne.components.Add(new ModelRenderer("resources/models/suzanne.obj", suzanne.transform));
            
            FrameManager.currentFrame.Awake();


            // make some test frames
            FrameManager.currentFrame.Name = "Frame 1";
            FrameManager.currentFrame.clearColor = new(12, 12, 12);
            FrameManager.currentFrame.gameObjects.Add(suzanne);
            FrameManager.CreateFrame(new("Frame 2", clearColor: new(180, 12, 12), gameObjects: [suzanne]));
            FrameManager.CreateFrame(new("Frame 3", clearColor: new(12, 12, 128), gameObjects: []));

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
            Renderer.Unload();
            FrameManager.currentFrame.Unload();

            Raylib.CloseWindow();
        }
    }
}
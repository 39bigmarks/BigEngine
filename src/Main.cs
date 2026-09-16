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
        public static float time = 0;

        public static Texture2D lightGizmo;

        internal static void Main(string[] args)
        {
            //Raylib.SetTraceLogLevel(TraceLogLevel.Warning);

            Raylib.InitWindow(windowWidth, windowHeight, "BigEngine");
            lightGizmo = Raylib.LoadTexture("resources/icons/light.png");
            Renderer.Init();

            GameObject suzanne = new("Suzanne", components: []);
            suzanne.components.Add(new ModelRenderer("resources/models/smoothmonkey.obj", suzanne));

            GameObject room = new("room", components: []);
            room.components.Add(new ModelRenderer("resources/models/room.obj", room));

            // make some test frames
            FrameManager.currentFrame.Name = "Frame 1";
            FrameManager.currentFrame.clearColor = new(12, 12, 12);
            FrameManager.currentFrame.gameObjects.Add(suzanne);
            
            List<PointLight> lights = [
                new()
                {
                    color = new(84, 122, 255),
                    strength = 0.5f,
                    position = new(1.5f, 1.5f, 0)
                }, new()
                {
                    color = new(230, 107, 0),
                    strength = 0.7f,
                    position = new(-1.5f, 0.25f, 0)
                }, new()
                {
                    color = new(255, 0, 0),
                    strength = 1.8f,
                    position = new(-1.5f, 0.75f, -3)
                }, new()
                {
                    color = new(255, 255, 255),
                    strength = 0.9f,
                    position = new(0, 5, 0)
                }
            ];

            FrameManager.currentFrame.CreateObject(new("Light 0", [lights[0]]));
            FrameManager.currentFrame.CreateObject(new("Light 1", [lights[1]]));
            FrameManager.currentFrame.CreateObject(new("Light 2", [lights[2]]));
            FrameManager.currentFrame.CreateObject(new("Light 3", [lights[3]]));

            FrameManager.currentFrame.Awake();

            // created frames will have a camera by default
            // maybe change system to use a default frame file so you can create an empty frame too?
            FrameManager.CreateFrame(new("Frame 2", clearColor: new(180, 12, 12), gameObjects: [room]));
            FrameManager.CreateFrame(new("Frame 3", clearColor: new(12, 12, 128), gameObjects: [suzanne, room]));
            FrameManager.CreateFrame(new("Frame 4", clearColor: new(128, 128, 180), gameObjects: []));

            FrameManager.LoadFrame(FrameManager.currentFrame);


            while (!Raylib.WindowShouldClose())
            {
                time += Raylib.GetFrameTime();

                if (Raylib.IsKeyPressed(KeyboardKey.One))
                    FrameManager.LoadFrame(FrameManager.frames[0]);

                if (Raylib.IsKeyPressed(KeyboardKey.Two))
                    FrameManager.LoadFrame(FrameManager.frames[1]);
                
                if (Raylib.IsKeyPressed(KeyboardKey.Three))
                    FrameManager.LoadFrame(FrameManager.frames[2]);

                if (Raylib.IsKeyPressed(KeyboardKey.Four))
                    FrameManager.LoadFrame(FrameManager.frames[3]);

                Raylib.BeginDrawing();
                
                FrameManager.currentFrame.Update();
                Raylib.DrawTextureRec(Renderer.renderTex.Texture, Renderer.textureRec, Vector2.Zero, Color.White);

                Raylib.EndDrawing();
            }

            Renderer.Unload();
            FrameManager.currentFrame.Unload();

            Raylib.CloseWindow();
        }
    }
}
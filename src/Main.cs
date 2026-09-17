using BigEngine.Engine;
using BigEngine.Game;
using BigEngine.Types;
using Raylib_cs;

namespace BigEngine
{
    internal static class BigEngine
    {
        public static int windowWidth = 1280;
        public static int windowHeight = 720;
        public static float timer = 0;
        public static bool srgb = true;

        internal static void Main(string[] args)
        {
            Raylib.SetTraceLogLevel(TraceLogLevel.Warning);

            Raylib.SetConfigFlags(ConfigFlags.VSyncHint);
            Raylib.InitWindow(windowWidth, windowHeight, "BigEngine");

            Raylib.SetTargetFPS(60);

            Prop3D room = new("resources/models/room.obj");
            Prop3D suzanne = new("resources/models/smoothmonkey.obj");
            PointLight light1 = new(Color.Beige)
            {
                position = new(0, 0, 0),
            };
            PointLight light2 = new(Color.Green)
            {
                position = new(3, 3, 3),
            };

            Frame frame1 = new("Frame 1", new(85, 12, 12))
            {
                objects = [
                    new PointLight(light1){
                        position = new(0, 1, 0),
                        intensity = 1f
                    },
                    new Prop3D(room){
                        transform = new(){
                            position = new(0, 0, 0)
                        }
                    }
                ],
                cameras = [
                    new(){
                        Position = new(3, 2, 7),
                        Target = new(0, 1, 0),
                        Up = new(0, 1, 0),
                        Projection = CameraProjection.Perspective,
                        FovY = 55f
                    }
                ]
            };
            Frame frame2 = new("Frame 2", new(70, 12, 85))
            {
                objects = [
                    suzanne,
                    new PointLight(light2){
                        position = new(3, 3, 3)
                    }
                ],
                cameras = [
                    new(){
                        Position = new(-3, 1, 7),
                        Target = new(0, 0, 0),
                        Up = new(0, 1, 0),
                        Projection = CameraProjection.Perspective,
                        FovY = 55f
                    }
                ]
            };
            Frame frame3 = new("Frame 3", new(70, 56, 85))
            {
                objects = [
                    new PointLight(light2){
                        position = new(0, 0, 0)
                    },
                    new Prop3D(room){
                        transform = new(){
                            position = new(0, 2, 0)
                        }
                    },
                    new Prop3D(suzanne){
                        transform = new(){
                            position = new(3, 0, 0),
                            scale = new(0.5f)
                        }
                    },
                    new Prop3D(suzanne){
                        transform = new(){
                            position = new(-3, 0, 0),
                            scale = new(0.5f)
                        }
                    }
                ],
                cameras = [
                    new(){
                        Position = new(-3, 2, 7),
                        Target = new(0, 1, 0),
                        Up = new(0, 1, 0),
                        Projection = CameraProjection.Perspective,
                        FovY = 55f
                    }
                ]
            };

            FrameManager.CreateFrame(frame1);
            FrameManager.CreateFrame(frame2);
            FrameManager.CreateFrame(frame3);

            FrameManager.LoadFrame(FrameManager.currentFrame, startup: true);

            Camera3D cam = FrameManager.frames[2].cameras[0];

            while (!Raylib.WindowShouldClose())
            {
                cam.Position = new(MathF.Sin(timer / 5) * 3.5f, 1.67f, MathF.Cos(timer / 5) * 3.5f);
                timer += Raylib.GetFrameTime();

                if (Raylib.IsKeyPressed(KeyboardKey.One))
                    FrameManager.LoadFrame(FrameManager.frames[0]);
                
                if (Raylib.IsKeyPressed(KeyboardKey.Two))
                    FrameManager.LoadFrame(FrameManager.frames[1]);
                
                if (Raylib.IsKeyPressed(KeyboardKey.Three))
                    FrameManager.LoadFrame(FrameManager.frames[2]);

                FrameManager.currentFrame.Update();

                FrameManager.currentFrame.cameras[0] = cam;
                if(FrameManager.currentFrame.Name == FrameManager.frames[0].Name)
                {
                    //((PointLight)FrameManager.currentFrame.objects[0]).position.X = 0;
                    //((PointLight)FrameManager.currentFrame.objects[0]).position.Y = MathF.Sin(timer) * 3;
                    //((PointLight)FrameManager.currentFrame.objects[0]).position.Z = 0;
                }
                if(FrameManager.currentFrame.Name == FrameManager.frames[2].Name)
                {
                    ((PointLight)FrameManager.currentFrame.objects[0]).position.X = 0;
                    ((PointLight)FrameManager.currentFrame.objects[0]).position.Y = MathF.Sin(timer * 5);
                    ((PointLight)FrameManager.currentFrame.objects[0]).position.Z = 0;
                }
            }
            FrameManager.UnloadFrame();

            Raylib.CloseWindow();
        }
    }
}
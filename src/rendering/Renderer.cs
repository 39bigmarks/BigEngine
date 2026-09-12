using Raylib_cs;
using BigEngine.Types;

namespace BigEngine.Engine
{
    internal static class Renderer
    {
        //                                      turn into memory later
        internal static Shader defaultShader = Raylib.LoadShader("resources/shaders/default/vert.glsl", "resources/shaders/default/frag.glsl");

        public static Material GetDefaultMaterial()
        {
            var defaultMat = Raylib.LoadMaterialDefault();
            defaultMat.Shader = defaultShader;
            return defaultMat;
        }

        internal static void Render(Camera3D camera, Color clearColor, List<GameObject> gameObjects)
        {
            Raylib.ClearBackground(clearColor);

            Raylib.BeginMode3D(camera);

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }

            Raylib.EndMode3D();
        }

        internal static void Unload()
        {
            Raylib.UnloadShader(defaultShader);
        }
    }
}
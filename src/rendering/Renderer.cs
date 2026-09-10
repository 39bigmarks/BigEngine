using Raylib_cs;
using Object = BigEngine.Objects.Object;
using Model = BigEngine.Objects.Model;

namespace BigEngine.Engine
{
    internal static class Renderer
    {
        internal static void Render(Camera3D camera, Color clearColor, List<Object> objects)
        {
            Raylib.ClearBackground(clearColor);

            Raylib.BeginMode3D(camera);

            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].components.Count == 0) continue;

                for (int j = 0; j < objects[i].components.Count; j++)
                {
                    if (objects[i].components[j] is not Model) continue;

                    objects[i].components[j].Update();
                }
            }

            Raylib.EndMode3D();
        }
    }
}
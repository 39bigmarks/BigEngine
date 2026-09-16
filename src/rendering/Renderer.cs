using Raylib_cs;
using BigEngine.Types;
using System.Numerics;
using BigEngine.Game;

namespace BigEngine.Engine
{
    internal static class Renderer
    {
        public enum ReflectionModel
        {
            GOURAUD,
            PHONG,
            BLINN_PHONG
        }

        public static Material defaultMaterial = Raylib.LoadMaterialDefault();

        // turn into memory later?
        private const string shaderPath = "resources/shaders/default/{NAME}/{NAME}_{TYPE}.glsl";
        internal static Shader reflectionShader;
        internal static int reflectionLoc_ambientColor = Raylib.GetShaderLocation(reflectionShader, "ambientColor");
        internal static int reflectionLoc_viewPos = Raylib.GetShaderLocation(reflectionShader, "viewPos");
        internal static int reflectionLoc_matSpec = Raylib.GetShaderLocation(reflectionShader, "material.specular");
        internal static int reflectionLoc_matShininess = Raylib.GetShaderLocation(reflectionShader, "material.shininess");

        //vec3 color;
        // vec3 position;
        // float strength;

        internal static int reflectionLoc_lightColor(int index) => Raylib.GetShaderLocation(reflectionShader, $"lights[{index}].color");
        internal static int reflectionLoc_lightPosition(int index) => Raylib.GetShaderLocation(reflectionShader, $"lights[{index}].position");
        internal static int reflectionLoc_lightStrength(int index) => Raylib.GetShaderLocation(reflectionShader, $"lights[{index}].strength");

        public static ReflectionModel reflectionModel = ReflectionModel.BLINN_PHONG;
        
        internal static RenderTexture2D renderTex = Raylib.LoadRenderTexture(BigEngine.windowWidth, BigEngine.windowHeight);
        //Raylib.SetTextureFilter(renderTex.Texture, TextureFilter.Trilinear);

        internal static Rectangle textureRec = new()
        {
            X = 0,
            Y = 0,
            Width = renderTex.Texture.Width,
            Height = -renderTex.Texture.Height,
        };
        
        internal static void Init()
        {
            string path = shaderPath.Replace("{NAME}", reflectionModel.ToString().ToLower().Replace("_", "-"));

            reflectionShader = Raylib.LoadShader(path.Replace("{TYPE}", "vert"), path.Replace("{TYPE}", "frag"));

            defaultMaterial.Shader = reflectionShader;

            reflectionLoc_matShininess = Raylib.GetShaderLocation(reflectionShader, "material.shininess");
        }

        private static Vector3 forward;
        private static Vector3 up;
        private static Vector3 right;

        internal static void Render(Camera3D camera, Color clearColor, List<GameObject> gameObjects)
        {
            Raylib.BeginTextureMode(renderTex);

            camera.Position = new(MathF.Sin(BigEngine.time / 4) * 15, 5, MathF.Cos(BigEngine.time / 4) * 15);

            Raylib.DrawFPS(12, 12);

            Raylib.ClearBackground(clearColor);

            Raylib.BeginMode3D(camera);
            
            forward = Raymath.Vector3Subtract(camera.Target, camera.Position);
            up = new(0, 1, 0);
            right = Raymath.Vector3CrossProduct(up, forward);
            up = Raymath.Vector3CrossProduct(forward, right);
            up = Raymath.Vector3Normalize(up);

            List<GameObject> objLights = gameObjects.FindAll(obj => obj.components[0] is Light);
            List<PointLight> lights = [];
            for (int i = 0; i < objLights.Count; i++)
                lights.Add((PointLight)objLights[i].components[0]);

            for (int i = 0; i < lights.Count; i++)
            {
                Raylib.DrawBillboardPro(camera, BigEngine.lightGizmo, new(0, 0, BigEngine.lightGizmo.Width, BigEngine.lightGizmo.Height), lights[i].position, up, new Vector2(0.5f), new(0, 0), 0, lights[i].color);
            }

            UpdateShader(camera, lights);
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }

            Raylib.EndMode3D();
            Raylib.EndTextureMode();
        }

        internal static void UpdateShader(Camera3D camera, List<PointLight> lights)
        {
            Raylib.SetShaderValue(reflectionShader, reflectionLoc_matSpec, new Vector3(5, 5, 5), ShaderUniformDataType.Vec3);
            Raylib.SetShaderValue(reflectionShader, reflectionLoc_matShininess, 128f, ShaderUniformDataType.Float);

            for (int i = 0; i < lights.Count; i++)
            {
                Raylib.SetShaderValue(reflectionShader, reflectionLoc_lightColor(i), new Vector3(lights[i].color.R / 255f, lights[i].color.G / 255f, lights[i].color.B / 255f), ShaderUniformDataType.Vec3);
                Raylib.SetShaderValue(reflectionShader, reflectionLoc_lightPosition(i), lights[i].position, ShaderUniformDataType.Vec3);
                Raylib.SetShaderValue(reflectionShader, reflectionLoc_lightStrength(i), lights[i].strength, ShaderUniformDataType.Float);
            }

            Raylib.SetShaderValue(reflectionShader, reflectionLoc_ambientColor, new Vector4(1, 1, 1, 1f), ShaderUniformDataType.Vec4);
            Raylib.SetShaderValue(reflectionShader, reflectionLoc_viewPos, camera.Position, ShaderUniformDataType.Vec3);
        }

        internal static void Unload()
        {
            Raylib.UnloadShader(reflectionShader);
            Raylib.UnloadTexture(BigEngine.lightGizmo);
        }
    }
}
using BigEngine.Game;
using BigEngine.Types;
using Raylib_cs;
using System.Numerics;

namespace BigEngine.Engine
{
    public enum ReflectionModel
    {
        None,
        Gouraud,
        Blinn_Phong,
        Phong
    }

    internal static class ShaderSettings
    {
        public static ReflectionModel refModel = ReflectionModel.Blinn_Phong;
        public static Shader defaultShader = Raylib.LoadShader(GetModelPath(frag: false),
                                                               GetModelPath(frag: true));

        public static int viewPosLoc = Raylib.GetShaderLocation(defaultShader, "viewPos");
        public static int roughnessLoc = Raylib.GetShaderLocation(defaultShader, "material.roughness");
        public static int specularLoc = Raylib.GetShaderLocation(defaultShader, "material.specular");
        public static int ambientColorLoc = Raylib.GetShaderLocation(defaultShader, "ambientColor");
        public static int gammaLoc = Raylib.GetShaderLocation(defaultShader, "gamma");
        public static int LightPositionLoc(int index) => Raylib.GetShaderLocation(defaultShader, $"lights[{index}].position");
        public static int LightColorLoc(int index) => Raylib.GetShaderLocation(defaultShader, $"lights[{index}].color");
        public static int LightIntensityLoc(int index) => Raylib.GetShaderLocation(defaultShader, $"lights[{index}].intensity");

        public static void UpdateShaders(Camera3D camera)
        {
            Raylib.SetShaderValue(defaultShader, viewPosLoc, camera.Position, ShaderUniformDataType.Vec3);
            Raylib.SetShaderValue(defaultShader, ambientColorLoc, new Vector3(0.1f, 0.1f, 0.1f), ShaderUniformDataType.Vec3);

            int index = 0;
            for (int i = 0; i < FrameManager.currentFrame.objects.Count; i++)
            {
                if (FrameManager.currentFrame.objects[i] is not PointLight) continue;
                PointLight light = (PointLight)FrameManager.currentFrame.objects[i];

                Raylib.SetShaderValue(defaultShader, LightPositionLoc(index), light.position, ShaderUniformDataType.Vec3);
                Raylib.SetShaderValue(defaultShader, LightColorLoc(index), new Vector3(light.color.R / 255f, light.color.G / 255f, light.color.B / 255f), ShaderUniformDataType.Vec3);
                Raylib.SetShaderValue(defaultShader, LightIntensityLoc(index), light.intensity, ShaderUniformDataType.Float);
                index++;
            }
            //Raylib.SetShaderValue(defaultShader, gammaLoc, (float)Raylib.GetMouseY() / (float)Raylib.GetScreenHeight() * 3f, ShaderUniformDataType.Float);
        }

        private static string GetModelPath(bool frag)
        {
            string model = refModel.ToString();
            return $"resources/shaders/default/{model}/{model}_{(frag ? "frag" : "vert")}.glsl";
        }
    }
}

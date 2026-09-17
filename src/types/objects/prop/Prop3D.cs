using BigEngine.Engine;
using Raylib_cs;
using System.Numerics;

namespace BigEngine.Types
{
    internal class Prop3D : IObject
    {
        public bool enabled { get; set; } = true;

        public string modelPath { get; set; }
        public Model model { get; set; }
        public Transform transform { get; set; } = new();
        public BDRFMaterial material { get; set; } = new();

        public Prop3D(string modelPath)
        {
            this.modelPath = modelPath;
            material.roughnessMap = "resources/textures/rough1.png";
        }
        public Prop3D(Prop3D dupe)
        {
            modelPath = dupe.modelPath;
            transform = dupe.transform;
            material = dupe.material;
        }

        public void Load()
        {
            model = Raylib.LoadModel(modelPath);
            unsafe
            {
                for (int i = 0; i < model.MaterialCount; i++)
                {
                    if (ShaderIsDefault(model.Materials[i].Shader)) continue;
                    model.Materials[i].Shader = ShaderSettings.defaultShader;
                }
            }
        }

        public void Update()
        {
            unsafe
            {
                for (int i = 0; i < model.MaterialCount; i++)
                {
                    Shader shader = model.Materials[i].Shader;
                    if (!ShaderIsDefault(shader)) continue;

                    Raylib.SetShaderValue(shader, ShaderSettings.roughnessLoc, material._roughnessMap, ShaderUniformDataType.Sampler2D);
                    Raylib.SetShaderValue(shader, ShaderSettings.specularLoc, material.specular, ShaderUniformDataType.Float);
                }
            }

            unsafe
            {
                Vector3 axis;
                float angle;
                Raymath.QuaternionToAxisAngle(Raymath.QuaternionFromEuler(transform.rotation.X, transform.rotation.Y, transform.rotation.Z), &axis, &angle);
                Raylib.DrawModelEx(model, transform.position, axis, angle, transform.scale, Color.White);
            }
        }

        public void Unload() => Raylib.UnloadModel(model);

        private bool ShaderIsDefault(Shader shader)
        {
            unsafe
            {
                return shader.Id == ShaderSettings.defaultShader.Id;
            }
        }
    }
}
using BigEngine.Engine;
using System.Numerics;
using Raylib_cs;

namespace BigEngine.Types
{
    internal class ModelRenderer(string meshPath, GameObject parent) : IComponent
    {
        public bool enabled { get; set; } = true;

        public string meshPath = meshPath;
        public List<Material> materials = [ Renderer.defaultMaterial ];

        public bool loaded { get; private set; } = false;

        public GameObject gameObject { get; } = parent;

        private Model model;

        public void Awake()
        {
            OnLoad();
        }

        public void OnLoad()
        {
            model = Raylib.LoadModel(meshPath);
            loaded = true;

            unsafe
            {
                for(int i = 0; i < model.MaterialCount; i++)
                    model.Materials[i].Shader = Renderer.defaultMaterial.Shader;
            }
        }

        public void Update()
        {
            if (!loaded) return;

            //                       turn into editor console later
            if (model.MeshCount == 0) { Console.WriteLine("<<BigEngine Warning>> Mesh is missing"); return; }

            unsafe
            {
                Vector3 axis;
                float angle;

                Raymath.QuaternionToAxisAngle(gameObject.transform.rotation, &axis, &angle);
                //Raylib.DrawModelEx(model, gameObject.transform.position, axis, angle, gameObject.transform.scale, Color.White);
                Raylib.DrawModel(model, Vector3.Zero, 1, Color.White);
            }
        }

        internal void Unload()
        {
            loaded = false;
            Raylib.UnloadModel(model);

            unsafe
            {
                for (int i = 0; i < model.MaterialCount; i++)
                    Raylib.UnloadMaterial(model.Materials[i]);
            }
        }
    }
}
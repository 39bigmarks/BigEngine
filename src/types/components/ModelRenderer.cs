using BigEngine.Engine;
using Raylib_cs;
using System.Numerics;
using System.Runtime.InteropServices;

namespace BigEngine.Types
{
    internal class ModelRenderer(string meshPath, Transform transform) : IComponent
    {
        public bool enabled { get; set; } = true;

        public string meshPath = meshPath;
        public List<Material> materials = [ Renderer.GetDefaultMaterial() ];

        public bool loaded { get; private set; } = false;

        private Model model;

        public void Awake()
        {
            Load();
        }

        public void Load()
        {
            model = Raylib.LoadModel(meshPath);
        }

        public void Start()
        {
            // SUPER UGLY!!! :(
            unsafe
            {
                fixed (Material* mats = materials.ToArray())
                {
                    model.Materials = mats;
                }
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
                Raymath.QuaternionToAxisAngle(transform.rotation, &axis, &angle);

                Raylib.DrawModelEx(model, transform.position, axis, angle, transform.scale, Color.White);
            }
        }

        internal void Unload()
        {
            loaded = false;
            Raylib.UnloadModel(model);
        }
    }
}
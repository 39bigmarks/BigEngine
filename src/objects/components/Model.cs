using Raylib_cs;
using System.Numerics;

namespace BigEngine.Objects
{
    internal class Model(Raylib_cs.Model model, Transform transform, Material material) : IComponent
    {
        // rework this to support reloading models please
        public Raylib_cs.Model model = model;
        public Material material = material;
        private bool unloaded = false;

        public void Awake() { }

        public void Start()
        {
            unsafe
            {
                model.Materials[0] = this.material;
            }
        }

        public void Update()
        {
            if (unloaded) return;

            //                                             make this global later
            if (model.MeshCount == 0) { Console.WriteLine("<<BigEngine Warning>> Mesh is missing"); return; }

            unsafe
            {
                Vector3 axis;
                float angle;
                Raymath.QuaternionToAxisAngle(transform.rotation, &axis, &angle);

                Raylib.DrawModelEx(model, transform.position, axis, angle, transform.scale, Color.White);
            }
        }

        public void Unload()
        {
            unloaded = true;
            Raylib.UnloadModel(model);
        }
    }
}
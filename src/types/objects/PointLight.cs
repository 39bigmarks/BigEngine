using Raylib_cs;
using System.Numerics;
using static BigEngine.Engine.Gizmos;

namespace BigEngine.Types
{
    internal class PointLight : IObject
    {
        public bool enabled { get; set; } = true;

        public Vector3 position = Vector3.Zero;
        public Color color = Color.White;
        public float intensity = 1f;
        public Gizmo gizmo { get; } = new("resources/icons/light.png");
        
        public PointLight(Color color, float intensity = 1)
        {
            position = Vector3.Zero;
            this.color = color;
            this.intensity = intensity;
        }
        public PointLight(PointLight dupe)
        {
            enabled = dupe.enabled;
            position = dupe.position;
            color = dupe.color;
            intensity = dupe.intensity;
        }

        public void Load()
        {
            gizmo.Load();
        }

        public void UpdateGizmos(Camera3D camera)
        {
            gizmo.tint = color;
            gizmo.position = position;
            gizmo.Draw(camera);
        }

        public void Unload()
        {
            gizmo.Unload();
        }
    }
}

    using Raylib_cs;
using System.Numerics;

namespace BigEngine.Types
{
    internal class Light : IComponent
    {
        public bool enabled { get; set; }
        public GameObject gameObject { get; }
    }
    
    // not yet implemented
    internal sealed class DirectionalLight : Light
    {
        public new bool enabled { get; set; }
        public new GameObject gameObject { get; }

        public Vector3 direction = Vector3.Zero;
        public Color color = Color.White;
        public float strength = 1f;
    }
    
    internal sealed class PointLight : Light
    {
        public new bool enabled { get; set; }
        public new GameObject gameObject { get; }

        public Vector3 position = Vector3.Zero;
        public Color color = Color.White;
        public float strength = 1f;
    }

    // not yet implemented
    internal sealed class SpotLight : Light
    {
        public new bool enabled { get; set; }
        public new GameObject gameObject { get; }

        public Vector3 position = Vector3.Zero;
        public Vector3 direction = Vector3.Zero;
        public Color color = Color.White;
        public float strength = 1f;
        public float innerCone = 30f;
        public float outerCone = 35f;
        public float distance = 10f;
    }
}

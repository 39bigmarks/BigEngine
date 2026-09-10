using System.Numerics;

namespace BigEngine.Objects
{
    internal class Object(string name, List<IComponent> components)
    {
        public string name { get; set; } = name;
        public Transform transform { get; set; } = new(Vector3.Zero, Quaternion.Identity, Vector3.One);
        public List<IComponent> components { get; set; } = components;
    }
}
using System.Numerics;

namespace BigEngine.Types
{
    internal class GameObject(string name, List<IComponent> components)
    {
        public string name { get; set; } = name;
        public Transform transform { get; set; } = new();
        public List<IComponent> components { get; set; } = components;

        public void Update()
        {
            for (int i = 0; i < components.Count; i++)
                components[i].Update();
        }

        public void Unload()
        {
            foreach (var component in components)
                component.Unload();
        }
    }
}
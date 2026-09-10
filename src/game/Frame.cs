using Object = BigEngine.Objects.Object;
using BigEngine.Engine;
using Raylib_cs;
using System.Numerics;

namespace BigEngine.Game
{
    internal class Frame(string name, Color clearColor, List<Object> objects)
    {
        public string Name = name;
        public Color clearColor = clearColor;

        // camera at index 0 will be prioritized
        public List<Camera3D> cameras { get; private set; } = [
            new(){
                Position = new(3, 3, 7),
                Target = Vector3.Zero,
                Up = new(0, 1, 0),
                Projection = CameraProjection.Perspective,
                FovY = 50f
            }
        ];
        public List<Object> objects { get; private set; } = objects;

        private List<Object> destroyQueue = [];
        
        public void Awake()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].components.Count == 0) continue;

                for (int j = 0; j < objects[i].components.Count; j++)
                    objects[i].components[j].Awake();
            }
        }
        public void Start()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].components.Count == 0) continue;

                for (int j = 0; j < objects[i].components.Count; j++)
                    objects[i].components[j].Start();
            }
        }
        public void Update()
        {
            if(destroyQueue.Count != 0)
                for (int i = 0; i < destroyQueue.Count; i++)
                {
                    foreach (var component in destroyQueue[i].components)
                        component.Unload();

                    objects.Remove(destroyQueue[i]);
                }

            if (cameras.Count > 0)
                Renderer.Render(cameras[0], clearColor, objects);
            //else
            //{
            // throw some error
            //}
        }
        
        public void CreateObject(Object obj)
        {
            objects.Add(obj);
        }
        public void CreateCamera(Camera3D camera, bool makePrimary = false)
        {
            if (makePrimary)
            {
                cameras.Insert(0, camera);
                return;
            }

            cameras.Add(camera);
        }

        public void Unload()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].components.Count == 0) continue;

                for (int j = 0; j < objects[i].components.Count; j++)
                    objects[i].components[j].Unload();
            }
        }
    }
}
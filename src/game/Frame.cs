using GameObject = BigEngine.Types.GameObject;
using BigEngine.Engine;
using Raylib_cs;
using System.Numerics;

namespace BigEngine.Game
{
    internal class Frame(string name, Color clearColor, List<GameObject> gameObjects)
    {
        public string Name = name;
        public Color clearColor = clearColor;

        public List<GameObject> gameObjects { get; private set; } = gameObjects;

        // camera at index 0 will be prioritized (unless specified later)
        public List<Camera3D> cameras { get; private set; } = [
            new(){
                Position = new(3, 3, 7),
                Target = Vector3.Zero,
                Up = new(0, 1, 0),
                Projection = CameraProjection.Perspective,
                FovY = 50f
            }
        ];

        private List<GameObject> destroyQueue = [];
        
        public void Awake()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                for (int j = 0; j < gameObjects[i].components.Count; j++)
                    gameObjects[i].components[j].Awake();
            }
        }
        public void Start()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                for (int j = 0; j < gameObjects[i].components.Count; j++)
                    gameObjects[i].components[j].Start();
            }
        }
        public void Update()
        {
            if(destroyQueue.Count != 0)
                for (int i = 0; i < destroyQueue.Count; i++)
                {

                    gameObjects.Remove(destroyQueue[i]);
                }

            if (cameras.Count > 0)
                Renderer.Render(cameras[0], clearColor, gameObjects);
            //else
            //{
            // throw some error
            //}
        }
        
        public void CreateObject(GameObject obj)
        {
            gameObjects.Add(obj);
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
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Unload();
            }
        }
    }
}
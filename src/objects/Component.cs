namespace BigEngine.Objects
{
    internal interface IComponent
    {
        public void Awake();
        public void Start();
        public void Update();
        public void Unload();
    }
}
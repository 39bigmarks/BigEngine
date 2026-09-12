namespace BigEngine.Types
{
    internal interface IComponent
    {
        public bool enabled { get; set; }
        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void PhysUpdate() { }
        public virtual void Load() { }
        public virtual void Unload() { }
    }
}
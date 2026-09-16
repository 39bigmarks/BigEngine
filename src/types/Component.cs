namespace BigEngine.Types
{
    internal interface IComponent
    {
        public bool enabled { get; set; }
        public GameObject gameObject { get; }

        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void OnLoad() { }
        public virtual void OnUnload() { }
    }
}
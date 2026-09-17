using Raylib_cs;
namespace BigEngine.Types
{
    internal interface IObject
    {
        public bool enabled { get; set; }
        public virtual void Update() { }
        public virtual void UpdateGizmos(Camera3D camera) { }
        public virtual void Load() { }
        public virtual void Unload() { }
        public virtual void UpdateShaders(Camera3D camera) { }
    }
}

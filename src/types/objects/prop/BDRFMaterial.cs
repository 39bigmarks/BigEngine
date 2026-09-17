using Raylib_cs;

namespace BigEngine.Types
{
    internal class BDRFMaterial
    {
        public Color tint = Color.White;
        public float specular = 5.0f;
        public string diffuseMap = "";
        public string roughnessMap = "";
        public string normalMap = "";
        public Texture2D _diffuseMap   { get; private set; }
        public Texture2D _roughnessMap { get; private set; }
        public Texture2D _normalMap    { get; private set; }
        //public float roughness = 0.5f;

        public void Load()
        {
            _diffuseMap = Raylib.LoadTexture(diffuseMap);
            _roughnessMap = Raylib.LoadTexture(roughnessMap);
            _normalMap = Raylib.LoadTexture(normalMap);
        }
        public void Unload()
        {
            Raylib.UnloadTexture(_diffuseMap);
            Raylib.UnloadTexture(_roughnessMap);
            Raylib.UnloadTexture(_normalMap);
        }
    }
}

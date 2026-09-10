using System.Numerics;

namespace BigEngine.Objects
{
    internal class Transform(Vector3 position = default, Quaternion rotation = default, Vector3 scale = default)
    {
        public Vector3 position = position;
        public Quaternion rotation = rotation;
        public Vector3 scale = scale;
    }
}

using System.Numerics;

namespace BigEngine.Types
{
    internal class Transform()
    {
        public Vector3 position = Vector3.Zero;
        public Quaternion rotation = Quaternion.Identity;
        public Vector3 scale = Vector3.One;
    }
}

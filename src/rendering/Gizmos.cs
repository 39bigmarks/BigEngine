using Raylib_cs;
using System.Numerics;

namespace BigEngine.Engine
{
    internal static class Gizmos
    {
        public static bool drawGizmos = true;
        public static float gizmoSize = 0.3f;
        
        private static Vector3 forward = Vector3.Zero;
        private static Vector3 up = Vector3.Zero;
        private static Vector3 right = Vector3.Zero;

        internal static void UnloadGizmos(List<Gizmo> gizmos)
        {
            for (int i = 0; i < gizmos.Count; i++)
                Raylib.UnloadTexture(gizmos[i].icon);
        }
        
        public class Gizmo
        {
            public Texture2D icon { get; private set; }
            public string iconPath { get; set; }
            public Vector3 position { get; set; } = Vector3.Zero;
            public Color tint { get; set; } = Color.White;

            public Gizmo(string iconPath)
            {
                this.iconPath = iconPath;
            }

            public void Load() => icon = Raylib.LoadTexture(iconPath);
            public void Unload() => Raylib.UnloadTexture(icon);

            public virtual void Draw(Camera3D camera)
            {
                if (!drawGizmos) return;

                forward = Raymath.Vector3Subtract(camera.Target, camera.Position);
                up = new(0, 1, 0);
                right = Raymath.Vector3CrossProduct(up, forward);
                up = Raymath.Vector3CrossProduct(forward, right);
                up = Raymath.Vector3Normalize(up);

                Raylib.DrawBillboardPro(camera, icon, new(0, 0, icon.Width, icon.Height), position, up, new Vector2(gizmoSize), new(gizmoSize / 2), 0, tint);
            }
        }
    }
}

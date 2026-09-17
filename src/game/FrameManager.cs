using BigEngine.Editor;

namespace BigEngine.Game
{
    internal static class FrameManager
    {
        public static List<Frame> frames { get; private set; } = [];
        public static Frame currentFrame { get; set; } = null;
        public static bool loading { get; private set; } = false;
        
        public static void CreateFrame(Frame frame)
        {
            frames.Add(frame);
            currentFrame ??= frame;
        }
        public static void RemoveFrame(Frame frame) => frames.Remove(frame);
        public static void RemoveFrame(int index) => frames.RemoveAt(index);

        public static void LoadFrame(Frame frame, bool startup = false)
        {
            if (loading) return;
            BigEngine.timer = 0;
            
            if (frame.cameras.Count == 0)
            {
                /*
                    temporary fix for now.

                    for some reason, when
                    i press "4" to go to
                    the frame with 0 cameras,
                    it thinks i'm holding 4,
                    so i don't exactly know
                    what's going on with that.
                */
                Debug.Log($"{frame.Name} has no cameras, so it cannot be loaded.", "error");
                return;
            }

            loading = true;

            if (!startup)
            {
                if (frame.Equals(currentFrame))
                {
                    Debug.Log($"Reloading {currentFrame.Name}...");
                    UnloadFrame();
                    currentFrame.Load();
                    loading = false;
                    return;
                }
                UnloadFrame();
                currentFrame = null;
            }

            Debug.Log($"Loading {frame.Name}...");

            if (!frames.Contains(frame))
                CreateFrame(frame);
            else
                currentFrame = frames[frames.IndexOf(frame)];

            currentFrame.Load();
            loading = false;
        }

        public static void UnloadFrame()
        {
            Debug.Log($"Unloading {currentFrame.Name}...");
            currentFrame.Unload();
        }
    }
}

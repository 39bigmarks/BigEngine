namespace BigEngine.Game
{
    internal static class FrameManager
    {
        public static List<Frame> frames { get; private set; } = [
            new("New Frame", new(12, 12, 12), [])
            ];
        public static Frame currentFrame { get; private set; } = frames[0];
        
        public static void CreateFrame(Frame frame) => frames.Add(frame);
        public static void RemoveFrame(Frame frame) => frames.Remove(frame);
        public static void RemoveFrame(int index) => frames.RemoveAt(index);
        
        public static void LoadFrame(Frame frame)
        {
            frames.Add(frame);
            currentFrame.Unload();
            Thread.Sleep(50);
            currentFrame = frame;
            currentFrame.Awake();
        }
    }
}

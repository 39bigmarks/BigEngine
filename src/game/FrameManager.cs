namespace BigEngine.Game
{
    internal static class FrameManager
    {
        public static List<Frame> frames { get; private set; } = [
            new("New Frame", clearColor: new(12, 12, 12), gameObjects: [])
            ];
        public static Frame currentFrame { get; set; } = frames[0];
        
        public static void CreateFrame(Frame frame) => frames.Add(frame);
        public static void RemoveFrame(Frame frame) => frames.Remove(frame);
        public static void RemoveFrame(int index) => frames.RemoveAt(index);
        
        public static void LoadFrame(Frame frame)
        {
            Console.WriteLine("Unloading current frame...");
            currentFrame.Unload();
            Console.WriteLine("Loading new frame...");
            //Thread.Sleep(50);
            frames.Add(frame);
            currentFrame = frame;
            currentFrame.Awake();
            Console.WriteLine("Loaded new frame");
            currentFrame.Start();
        }
    }
}

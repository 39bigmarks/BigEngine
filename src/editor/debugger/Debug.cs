namespace BigEngine.Editor
{
    internal static class Debug
    {
        public static bool useEditorConsole = false; // does nothing for now
        public static bool showStackTraceOnError = false;

        public static Dictionary<string, ConsoleColor> logHighlight = new() {
            {"INFO", ConsoleColor.Yellow },
            {"WARN", ConsoleColor.DarkYellow },
            {"ERROR", ConsoleColor.Red },
            {"FINISH", ConsoleColor.Green } };

        
        // will definitely change this to use the editor console
        public static void Log(object message) => Log(message, "INFO");
        public static void Log(object message, string type)
        {
            if (string.IsNullOrWhiteSpace(type)) 
                type = "INFO";
            else 
                type = type.ToUpper();

            try
            {
                var color = logHighlight.TryGetValue(type, out var c) ? c : (ConsoleColor?)null;
                if (color.HasValue) Console.ForegroundColor = logHighlight[type];

                Console.WriteLine($"BigEngine [{type}]: {message.ToString()}");

                if (color.HasValue) Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"BigEngine [EXCEPTION]: {e.Message}");
                Console.ResetColor();

                if (showStackTraceOnError)
                {
                    Console.WriteLine("└─ Stack trace:\n" + e.StackTrace);
                }
            }
        }
    }
}
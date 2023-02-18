namespace Testowe_Backend_Client.Common
{
    public class SysConsole
    {
        public SysConsole()
        {
        }
        public static void WriteQuestionLine(string question, int newlines = 0)
        {
            Write(ConsoleColor.Yellow, Console.WriteLine, question, newlines);
        }

        private static void Write(ConsoleColor color, Action<string> writeDelegate, string text, int newlines)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            writeDelegate(text);
            Console.ForegroundColor = currentColor;
            foreach(var i in newlines)
                Console.WriteLine();
        }
        public static void WriteLine(string str, int newlines = 0)
        {
            Write(ConsoleColor.White, Console.WriteLine, str, newlines);
        }
        public static void Write(string str, int newlines = 0)
        {
            Write(ConsoleColor.White, Console.Write, str, newlines);
        }
    }
    static class Int32Extension
    { 
        public static IEnumerator<int> GetEnumerator(this int number) {
        
            for(int i = 0; i < number; i++)
            {
                yield return i;
            }
        }
    }

}

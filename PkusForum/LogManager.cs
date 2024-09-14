namespace PkusForum
{
    public class LogManager
    {
        string owner;

        public LogManager(string owner)
        {
            this.owner = owner;
        }

        public void PrintLog(object? message)
        {
            PrintLog(owner, message);
        }

        public static void PrintLog(string owner, object? message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"[{DateTime.Now} | {owner}]");
            Console.ResetColor();
            Console.WriteLine($" {message}");
        }
    }
}

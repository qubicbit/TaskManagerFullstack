using System.Net;

namespace TestRunner;

public static class ConsoleFormatter
{
    public static void PrintResult(string prefix, HttpStatusCode status, string endpoint)
    {
        switch ((int)status)
        {
            case 200:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case 403:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
        }

        Console.WriteLine($"{prefix} [{(int)status}]  {endpoint}");
        Console.ResetColor();
    }
}

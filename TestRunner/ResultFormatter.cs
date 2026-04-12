using TestRunner.Models;

namespace TestRunner;

public static class ResultFormatter
{
    public static void PrintGrouped(string role, List<(ApiEndpoint ep, int code)> results)
    {
        Console.WriteLine($"\n=== RESULTAT FÖR {role.ToUpper()} ===\n");

        var groups = results
            .GroupBy(r => r.ep.Url.Split('/')[2]) // controller-namn
            .OrderBy(g => g.Key);

        int success = 0, clientErr = 0, serverErr = 0;

        foreach (var group in groups)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n--- {group.Key.ToUpper()} ---");
            Console.ResetColor();

            foreach (var (ep, code) in group)
            {
                var explanation = StatusCodes.Explain(code);

                Console.ForegroundColor = code switch
                {
                    >= 200 and < 300 => ConsoleColor.Green,     // Success
                    401 => ConsoleColor.Yellow,                 // Unauthorized
                    403 => ConsoleColor.Red,                    // Forbidden
                    404 => ConsoleColor.DarkGray,               // Not Found
                    415 => ConsoleColor.DarkGray,               // Unsupported Media Type
                    >= 500 => ConsoleColor.Red,                 // Server errors
                    _ => ConsoleColor.Gray
                };


                Console.WriteLine($"[{code}] {ep.Method} {ep.Url} → {explanation}");
                Console.ResetColor();

                if (code is >= 200 and < 300) success++;
                else if (code is >= 400 and < 500) clientErr++;
                else if (code >= 500) serverErr++;
            }
        }

        Console.WriteLine("\n=== SAMMANFATTNING ===");
        Console.WriteLine($"Totalt: {results.Count}");
        Console.WriteLine($"✔ Lyckade: {success}");
        Console.WriteLine($"⚠ Klientfel: {clientErr}");
        Console.WriteLine($"❌ Serverfel: {serverErr}");
    }
}

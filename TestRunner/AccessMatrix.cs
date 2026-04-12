using TestRunner.Models;

namespace TestRunner;

public static class AccessMatrix
{
    public static void Print(
        List<(ApiEndpoint ep, int code)> admin,
        List<(ApiEndpoint ep, int code)> user,
        List<(ApiEndpoint ep, int code)> pub)
    {
        Console.WriteLine("\n=== ACCESS-MATRIX ===\n");

        var endpoints = admin.Select(a => a.ep).ToList();

        foreach (var ep in endpoints)
        {
            var a = admin.First(x => x.ep == ep).code;
            var u = user.First(x => x.ep == ep).code;
            var p = pub.First(x => x.ep == ep).code;

            string Access(int code) =>
                code switch
                {
                    >= 200 and < 300 => "✔",   // Access
                    401 => "🔒",               // Kräver login
                    403 => "⛔",               // Rollen saknar rättigheter
                    _ => "–"                  // 404, 415 etc
                };

            Console.WriteLine(
                $"{ep.Method,-6} {ep.Url,-40}  " +
                $"Public: {Access(p)}   User: {Access(u)}   Admin: {Access(a)}");
        }

        Console.WriteLine("\nLegend:");
        Console.WriteLine("✔  = Access");
        Console.WriteLine("🔒 = Kräver inloggning");
        Console.WriteLine("⛔ = Rollen saknar rättigheter");
        Console.WriteLine("–  = Ej relevant / 404");
    }


    public static void PrintSingle(string role, List<(ApiEndpoint ep, int code)> results)
    {
        Console.WriteLine($"\n=== ACCESS-MATRIX ({role.ToUpper()}) ===\n");

        foreach (var (ep, code) in results)
        {
            string Access(int c) =>
                c switch
                {
                    >= 200 and < 300 => "✔",
                    401 => "🔒",
                    403 => "⛔",
                    _ => "–"
                };

            Console.WriteLine($"{ep.Method,-6} {ep.Url,-40}  {role}: {Access(code)}");
        }

        Console.WriteLine("\nLegend:");
        Console.WriteLine("✔  = Access");
        Console.WriteLine("🔒 = Kräver inloggning");
        Console.WriteLine("⛔ = Rollen saknar rättigheter");
        Console.WriteLine("–  = Ej relevant / 404");
    }

}

namespace TestRunner;

public static class StatusCodes
{
    private static readonly Dictionary<int, string> _map = new()
    {
        { 200, "OK – Allt fungerade" },
        { 201, "Created – Resurs skapad" },
        { 204, "No Content – Lyckades utan data" },

        { 400, "Bad Request – Felaktig input" },
        { 401, "Unauthorized – Kräver inloggning" },
        { 403, "Forbidden – Rollen saknar access" },
        { 404, "Not Found – Resurs saknas eller ID ogiltigt" },
        { 405, "Method Not Allowed – Fel HTTP‑metod" },
        { 415, "Unsupported Media Type – Body saknas eller fel format" },

        { 500, "Server Error – Något gick fel i API:t" }
    };

    public static string Explain(int code)
        => _map.TryGetValue(code, out var text)
            ? text
            : "Okänd statuskod";
}

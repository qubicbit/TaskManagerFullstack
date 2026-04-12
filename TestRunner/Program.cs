using TestRunner;
using TestRunner.Services;
using TestRunner.Models;

var mode = args.ElementAtOrDefault(0) ?? "all";

Console.WriteLine($"=== Kör testläge: {mode.ToUpper()} ===");

// Neutral klient för swagger
var bootstrapApi = new ApiClient("https://localhost:7238");
var swaggerLoader = new SwaggerLoader(bootstrapApi.HttpClient);
var swagger = await swaggerLoader.LoadAsync();
var allEndpoints = EndpointDiscovery.FromSwagger(swagger);

// Resultat för matrisen
var adminResults = new List<(ApiEndpoint ep, int code)>();
var userResults = new List<(ApiEndpoint ep, int code)>();
var publicResults = new List<(ApiEndpoint ep, int code)>();

// ===============================
// ADMIN
// ===============================
if (mode is "admin" or "all")
{
    Console.WriteLine("\n--- ADMIN TESTER ---");

    var api = new ApiClient("https://localhost:7238");
    var token = await api.LoginAsync("a", "a");

    if (!string.IsNullOrWhiteSpace(token))
    {
        foreach (var ep in allEndpoints)
        {
            var response = await api.Send(ep);
            adminResults.Add((ep, (int)response.StatusCode));
        }

        ResultFormatter.PrintGrouped("Admin", adminResults);
    }
}

// ===============================
// USER
// ===============================
if (mode is "user" or "all")
{
    Console.WriteLine("\n--- USER TESTER ---");

    var api = new ApiClient("https://localhost:7238");
    var token = await api.LoginAsync("user1@example.com", "Password123!");

    if (!string.IsNullOrWhiteSpace(token))
    {
        foreach (var ep in allEndpoints)
        {
            var response = await api.Send(ep);
            userResults.Add((ep, (int)response.StatusCode));
        }

        ResultFormatter.PrintGrouped("User", userResults);
    }
}

// ===============================
// PUBLIC
// ===============================
if (mode is "public" or "all")
{
    Console.WriteLine("\n--- PUBLIC TESTER ---");

    var api = new ApiClient("https://localhost:7238");

    foreach (var ep in allEndpoints)
    {
        var response = await api.Send(ep);
        publicResults.Add((ep, (int)response.StatusCode));
    }

    ResultFormatter.PrintGrouped("Public", publicResults);
}

// ===============================
// ACCESS-MATRIX (ALLTID VISA)
// ===============================
if (mode == "admin")
{
    AccessMatrix.PrintSingle("Admin", adminResults);
}
else if (mode == "user")
{
    AccessMatrix.PrintSingle("User", userResults);
}
else if (mode == "public")
{
    AccessMatrix.PrintSingle("Public", publicResults);
}
else if (mode == "all")
{
    AccessMatrix.Print(adminResults, userResults, publicResults);
}


Console.WriteLine("\nKLART.");

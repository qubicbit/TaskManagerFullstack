using TestRunner.Models;

namespace TestRunner.Services;

public static class EndpointDiscovery
{
    public static List<ApiEndpoint> FromSwagger(SwaggerDoc swagger)
    {
        var list = new List<ApiEndpoint>();

        foreach (var path in swagger.Paths)
        {
            var url = path.Key;
            var item = path.Value;

            if (item.Get != null)
                list.Add(new ApiEndpoint { Method = "GET", Url = url, Description = item.Get.Summary ?? "" });

            if (item.Post != null)
                list.Add(new ApiEndpoint { Method = "POST", Url = url, Description = item.Post.Summary ?? "" });

            if (item.Put != null)
                list.Add(new ApiEndpoint { Method = "PUT", Url = url, Description = item.Put.Summary ?? "" });

            if (item.Delete != null)
                list.Add(new ApiEndpoint { Method = "DELETE", Url = url, Description = item.Delete.Summary ?? "" });
        }

        return list;
    }
}

namespace TaskManagerApi.Swagger
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class AdminOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAdminRole = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Any(a => a.Roles?.Contains("Admin") == true);

            if (!hasAdminRole)
                return;

            // 🔒 Ikon i summary
            operation.Summary = $"🔒 ADMIN — {operation.Summary ?? ""}";

            // 🟥 Badge i description
            var adminWarning =
                "<div style='padding:6px 10px; background:#ffdddd; border-left:4px solid #cc0000; margin-bottom:10px;'>" +
                "<strong style='color:#cc0000;'>ADMIN ENDPOINT</strong><br/>" +
                "Endast användare med rollen <strong>Admin</strong> kan anropa denna endpoint." +
                "</div>";

            operation.Description = adminWarning + (operation.Description ?? "");

            // ❌ Viktigt: vi rör INTE operation.Tags
            // Controller-taggarna ska vara kvar
        }
    }
}

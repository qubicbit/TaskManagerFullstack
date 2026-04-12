using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerApi.Data;
using TaskManagerApi.Extensions;
using TaskManagerApi.Middlewares;
using TaskManagerApi.Profiles;
using TaskManagerApi.Services.Admin;
using TaskManagerApi.Services.Auth;
using TaskManagerApi.Services.Categories;
using TaskManagerApi.Services.Comments;
using TaskManagerApi.Services.Tags;
using TaskManagerApi.Services.Tasks;
using TaskManagerApi.Services.TaskTags;
using TaskManagerApi.Services.Users;
using TaskManagerApi.Swagger;



Console.WriteLine(">>> PROGRAM STARTED <<<");

var builder = WebApplication.CreateBuilder(args);

// DbContext (SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity 
builder.Services.AddIdentityServices();

//// HttpContextAccessor
//builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<IUserContext, UserContext>();


// JWT Authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

//// Authorization Policies, använd kanske senare
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", policy =>
//        policy.RequireRole("Admin"));
//});


// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ITaskTagService, TaskTagService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ITaskService, TaskService>();



// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwt();

builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<AdminOperationFilter>();
});

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

// CORS
builder.Services.AddCorsPolicy();

var app = builder.Build();

// seed
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    // Kör seed bara om databasen är tom
    if (!context.Users.Any())
    {
        Console.WriteLine(">>> FIRST RUN: Seeding database...");
        await TaskManagerApi.Data.Dev.DevDatabaseReset.ResetAsync(services);
    }
}


Console.WriteLine(">>> RUNNING SEED <<<");

// Swagger only in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// pipeline
app.UseHttpsRedirection();
app.UseCorsPolicy();

// globall error handling
app.UseMiddleware<ExceptionMiddleware>();

// Authentication + Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

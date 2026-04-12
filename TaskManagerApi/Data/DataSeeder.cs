using Microsoft.AspNetCore.Identity;
using TaskManagerApi.Data;
using TaskManagerApi.Models;

public static class DataSeeder
{
    public static async Task SeedUsersAndTasks(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var context = services.GetRequiredService<AppDbContext>();

        var categories = context.Categories.ToList();
        var tags = context.Tags.ToList();

        var genericComments = new[]
        {
            "I'll work on this later today.",
            "Making progress on this one.",
            "This task looks straightforward.",
            "Adding this to my priority list.",
            "I need to finish this soon.",
            "This might take longer than expected.",
            "I'll revisit this tomorrow.",
            "Looks good so far.",
            "Almost done with this.",
            "This is a bit tricky but manageable."
        };

        var keywordComments = new Dictionary<string, string[]>
        {
            ["update"] = new[] { "I should check for the latest version first.", "Hope this fixes the performance issues." },
            ["clean"] = new[] { "Time to tidy things up.", "This will make everything feel fresh again." },
            ["email"] = new[] { "Lots of messages today.", "I need to stay on top of communication." },
            ["study"] = new[] { "This chapter is interesting.", "I should take notes while reading." },
            ["workout"] = new[] { "Feeling motivated today.", "Let's push for a good session." }
        };

        var random = new Random();

        // Create 5 users
        for (int i = 1; i <= 5; i++)
        {
            string email = $"user{i}@example.com";
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FullName = $"User {i}",
                    IsActive = true
                };

                await userManager.CreateAsync(user, "Password123!");
                await userManager.AddToRoleAsync(user, "User");
            }

            // Create 3 tasks per user
            for (int t = 1; t <= 3; t++)
            {
                string title = $"Task {t} for {user.FullName}";

                if (!context.Tasks.Any(x => x.Title == title && x.UserId == user.Id))
                {
                    var createdAt = DateTime.UtcNow.AddDays(-t);

                    var task = new TaskItem
                    {
                        Title = title,
                        Description = $"Auto-generated task {t} for {user.FullName}",
                        IsCompleted = false,
                        CreatedAt = createdAt,
                        UserId = user.Id,
                        CategoryId = categories.OrderBy(_ => Guid.NewGuid()).First().Id,

                        // NEW: Deadline
                        Deadline = createdAt.AddDays(random.Next(1, 11))
                    };

                    context.Tasks.Add(task);
                    await context.SaveChangesAsync();

                    // Add 2–3 tags
                    var randomTags = tags.OrderBy(_ => Guid.NewGuid()).Take(3).ToList();
                    foreach (var tag in randomTags)
                    {
                        context.TaskTags.Add(new TaskTag
                        {
                            TaskId = task.Id,
                            TagId = tag.Id
                        });
                    }

                    // Generate 1–3 comments
                    int commentCount = random.Next(1, 4);

                    for (int c = 0; c < commentCount; c++)
                    {
                        string commentText = genericComments.OrderBy(_ => Guid.NewGuid()).First();

                        foreach (var kvp in keywordComments)
                        {
                            if (task.Title.ToLower().Contains(kvp.Key))
                            {
                                var extra = kvp.Value.OrderBy(_ => Guid.NewGuid()).First();
                                commentText = $"{commentText} {extra}";
                                break;
                            }
                        }

                        context.Comments.Add(new Comment
                        {
                            TaskId = task.Id,
                            UserId = user.Id,
                            Content = commentText,
                            CreatedAt = createdAt.AddMinutes(random.Next(10, 5000))
                        });
                    }

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}

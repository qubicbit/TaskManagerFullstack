using Microsoft.AspNetCore.Identity;
using TaskManagerApi.Models;

namespace TaskManagerApi.Data
{
    public static class PublicTaskSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            var publicUser = await userManager.FindByEmailAsync("public@system.local");

            if (publicUser == null)
            {
                publicUser = new ApplicationUser
                {
                    UserName = "public@system.local",
                    Email = "public@system.local",
                    FullName = "Public User",
                    IsActive = true
                };

                await userManager.CreateAsync(publicUser, "Public123!");
            }


            // ---------------------------------------------------------
            // 1. Seed categories
            // ---------------------------------------------------------
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "IT" },
                    new Category { Name = "Home" },
                    new Category { Name = "Work" },
                    new Category { Name = "Study" },
                    new Category { Name = "Fitness" }
                );
                await context.SaveChangesAsync();
            }

            var categories = context.Categories.ToList();

            // ---------------------------------------------------------
            // 2. Seed tags
            // ---------------------------------------------------------
            if (!context.Tags.Any())
            {
                context.Tags.AddRange(
                    new Tag { Name = "Important" },
                    new Tag { Name = "Quick" },
                    new Tag { Name = "Home" },
                    new Tag { Name = "Work" },
                    new Tag { Name = "Study" },
                    new Tag { Name = "Fitness" },
                    new Tag { Name = "Computer" },
                    new Tag { Name = "Cleaning" },
                    new Tag { Name = "Urgent" },
                    new Tag { Name = "Daily" }
                );
                await context.SaveChangesAsync();
            }

            var tags = context.Tags.ToList();
            Tag T(string name) => tags.First(t => t.Name == name);

            // ---------------------------------------------------------
            // 3. Create public tasks (with deadlines + comments)
            // ---------------------------------------------------------
            var now = DateTime.UtcNow;

            var publicTasks = new List<(TaskItem task, List<Tag> tags, List<Comment> comments)>
            {
                (
                    new TaskItem {
                        Title = "Update graphics drivers",
                        Description = "Download and install the latest NVIDIA or AMD drivers.",
                        CategoryId = categories.First(c => c.Name == "IT").Id,
                        CreatedAt = now,
                        Deadline = now.AddDays(3),
                        UserId = null,
                        IsCompleted = false
                    },
                    new List<Tag> { T("Computer"), T("Important"), T("Quick") },
                    new List<Comment> {
                        new Comment { Content = "Remember to reboot after installation!", CreatedAt = now, UserId = publicUser.Id },
                        new Comment { Content = "This fixed my FPS drops.", CreatedAt = now, UserId = publicUser.Id }
                    }
                ),
                (
                    new TaskItem {
                        Title = "Run antivirus scan",
                        Description = "Perform a full system scan to detect malware.",
                        CategoryId = categories.First(c => c.Name == "IT").Id,
                        CreatedAt = now,
                        Deadline = now.AddDays(1),
                        UserId = null,
                        IsCompleted = true
                    },
                    new List<Tag> { T("Computer"), T("Urgent"), T("Daily") },
                    new List<Comment> {
                        new Comment { Content = "Full scan took about 30 minutes.", CreatedAt = now, UserId = publicUser.Id }
                    }
                ),
                (
                    new TaskItem {
                        Title = "Clean up disk space",
                        Description = "Remove temporary files and uninstall unused applications.",
                        CategoryId = categories.First(c => c.Name == "IT").Id,
                        CreatedAt = now,
                        Deadline = now.AddDays(5),
                        UserId = null,
                        IsCompleted = false
                    },
                    new List<Tag> { T("Computer"), T("Cleaning"), T("Important") },
                    new List<Comment> {
                        new Comment { Content = "Use WinDirStat to find large folders.", CreatedAt = now, UserId = publicUser.Id }
                    }
                ),
                (
                    new TaskItem {
                        Title = "Clean the kitchen",
                        Description = "Wash dishes, wipe counters, empty trash.",
                        CategoryId = categories.First(c => c.Name == "Home").Id,
                        CreatedAt = now,
                        Deadline = now.AddDays(2),
                        UserId = null,
                        IsCompleted = true
                    },
                    new List<Tag> { T("Home"), T("Cleaning"), T("Daily") },
                    new List<Comment> {
                        new Comment { Content = "Looks great after cleaning!", CreatedAt = now, UserId = publicUser.Id }
                    }
                ),
                (
                    new TaskItem {
                        Title = "Do the laundry",
                        Description = "Wash a load of dark clothes.",
                        CategoryId = categories.First(c => c.Name == "Home").Id,
                        CreatedAt = now,
                        Deadline = now.AddDays(1),
                        UserId = null,
                        IsCompleted = false
                    },
                    new List<Tag> { T("Home"), T("Daily"), T("Quick") },
                    new List<Comment> {
                        new Comment { Content = "Don't forget fabric softener.", CreatedAt = now, UserId = publicUser.Id }
                    }
                ),
                (
                    new TaskItem {
                        Title = "Plan the sprint",
                        Description = "Review backlog and prioritize tasks.",
                        CategoryId = categories.First(c => c.Name == "Work").Id,
                        CreatedAt = now,
                        Deadline = now.AddDays(7),
                        UserId = null,
                        IsCompleted = false
                    },
                    new List<Tag> { T("Work"), T("Important"), T("Urgent") },
                    new List<Comment> {
                        new Comment { Content = "Sprint planning meeting at 10:00.", CreatedAt = now, UserId = publicUser.Id }
                    }
                ),
                (
                    new TaskItem {
                        Title = "Reply to customer emails",
                        Description = "Respond to incoming support requests.",
                        CategoryId = categories.First(c => c.Name == "Work").Id,
                        CreatedAt = now,
                        Deadline = now.AddHours(6),
                        UserId = null,
                        IsCompleted = true
                    },
                    new List<Tag> { T("Work"), T("Urgent"), T("Daily") },
                    new List<Comment> {
                        new Comment { Content = "Handled all priority tickets.", CreatedAt = now, UserId = publicUser.Id }
                    }
                ),
                (
                    new TaskItem {
                        Title = "Read chapter 3",
                        Description = "Study the chapter on data structures.",
                        CategoryId = categories.First(c => c.Name == "Study").Id,
                        CreatedAt = now,
                        Deadline = now.AddDays(4),
                        UserId = null,
                        IsCompleted = false
                    },
                    new List<Tag> { T("Study"), T("Important"), T("Daily") },
                    new List<Comment> {
                        new Comment { Content = "This chapter is really useful.", CreatedAt = now, UserId = publicUser.Id }
                    }
                ),
                (
                    new TaskItem {
                        Title = "Solve math exercises",
                        Description = "Complete exercises 1–10 in algebra.",
                        CategoryId = categories.First(c => c.Name == "Study").Id,
                        CreatedAt = now,
                        Deadline = now.AddDays(2),
                        UserId = null,
                        IsCompleted = true
                    },
                    new List<Tag> { T("Study"), T("Important"), T("Quick") },
                    new List<Comment> {
                        new Comment { Content = "Exercise 7 was tricky!", CreatedAt = now, UserId = publicUser.Id }
                    }
                ),
                (
                    new TaskItem {
                        Title = "Upper body workout",
                        Description = "Bench press, rows, shoulder press, curls.",
                        CategoryId = categories.First(c => c.Name == "Fitness").Id,
                        CreatedAt = now,
                        Deadline = now.AddDays(1),
                        UserId = null,
                        IsCompleted = false
                    },
                    new List<Tag> { T("Fitness"), T("Daily"), T("Important") },
                    new List<Comment> {
                        new Comment { Content = "Great pump today!", CreatedAt = now, UserId = publicUser.Id }
                    }
                )
            };

            // ---------------------------------------------------------
            // 4. Save tasks
            // ---------------------------------------------------------
            foreach (var (task, _, _) in publicTasks)
                context.Tasks.Add(task);

            await context.SaveChangesAsync();

            // ---------------------------------------------------------
            // 5. Assign tags + comments
            // ---------------------------------------------------------
            foreach (var (task, tagList, commentList) in publicTasks)
            {
                foreach (var tag in tagList)
                {
                    context.TaskTags.Add(new TaskTag
                    {
                        TaskId = task.Id,
                        TagId = tag.Id
                    });
                }

                foreach (var comment in commentList)
                {
                    comment.TaskId = task.Id;
                    context.Comments.Add(comment);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}

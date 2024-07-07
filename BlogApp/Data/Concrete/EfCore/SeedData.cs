using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore;

public static class SeedData
{
    public static void ApplyTestData(IApplicationBuilder applicationBuilder)
    {
        var context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
        if (context != null)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Tags.Any())
            {
                context.Tags.AddRange(
                    new Entity.Tag { Text = "web prog", Url = "web-prog" },
                    new Entity.Tag { Text = "backend", Url = "backend" },
                    new Entity.Tag { Text = "frontend", Url = "frontend" },
                    new Entity.Tag { Text = "fullstack", Url = "fullstack" },
                    new Entity.Tag { Text = "php", Url = "php" }
                );
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Username = "turkerkiv", Image = "1.jpg", Name = "Türker", Email = "tkvlcm@gmail.com", Password = "turko123" },
                    new User { Username = "test1test2", Image = "2.jpg", Name = "Test", Email = "Test@gmail.com", Password = "test123" }
                );
                context.SaveChanges();
            }

            if (!context.Posts.Any())
            {
                context.Posts.AddRange(
                    new Post
                    {
                        Title = "Asp.net core",
                        Content = "Asp.net core dersleri",
                        IsActive = true,
                        Url = "aspnet-core",
                        PublishedOn = DateTime.UtcNow.AddDays(-1),
                        Tags = context.Tags.Take(3).ToList(),
                        Image = "1.jpg",
                        UserId = 1,
                        Comments = new List<Comment>
                        {
                            new Comment {Text= "lorem ipsum", PublishedOn = DateTime.UtcNow.AddDays(-10), UserId = 2},
                            new Comment {Text= "lorem ipsum", PublishedOn = DateTime.UtcNow.AddDays(-5), UserId = 1},
                            new Comment {Text= "lorem ipsum", PublishedOn = DateTime.UtcNow.AddDays(-3), UserId = 2},
                        }
                    },
                    new Post
                    {
                        Title = "Php ",
                        Content = "Php dersleri",
                        IsActive = true,
                        Url = "php",
                        PublishedOn = DateTime.UtcNow.AddDays(-20),
                        Tags = context.Tags.Take(2).ToList(),
                        Image = "2.jpg",
                        UserId = 1,
                        Comments = new List<Comment>
                        {
                            new Comment {Text= "lorem ipsum", PublishedOn = DateTime.UtcNow.AddDays(-10), UserId = 2},
                            new Comment {Text= "lorem ipsum", PublishedOn = DateTime.UtcNow.AddDays(-5), UserId = 1},
                        }
                    },
                    new Post
                    {
                        Title = "Django",
                        Content = "Django dersleri",
                        IsActive = true,
                        Url = "django",
                        PublishedOn = DateTime.UtcNow.AddDays(-5),
                        Tags = context.Tags.Take(1).ToList(),
                        Image = "3.jpg",
                        UserId = 2,
                        Comments = new List<Comment>
                        {
                            new Comment {Text= "lorem ipsum", PublishedOn = DateTime.UtcNow.AddDays(-10), UserId = 2},
                            new Comment {Text= "lorem ipsum", PublishedOn = DateTime.UtcNow.AddDays(-7), UserId = 2},
                            new Comment {Text= "lorem ipsum", PublishedOn = DateTime.UtcNow.AddDays(-5), UserId = 1},
                        }
                    },
                    new Post
                    {
                        Title = "React",
                        Content = "React dersleri",
                        IsActive = true,
                        Url = "react",
                        PublishedOn = DateTime.UtcNow.AddDays(-3),
                        Tags = context.Tags.Take(4).ToList(),
                        Image = "1.jpg",
                        UserId = 2,
                        Comments = new List<Comment>
                        {
                            new Comment {Text= "lorem ipsum", PublishedOn = DateTime.UtcNow.AddDays(-5), UserId = 1},
                        }
                    },
                    new Post
                    {
                        Title = "Angular",
                        Content = "Angular dersleri",
                        IsActive = true,
                        Url = "angular",
                        PublishedOn = DateTime.UtcNow.AddDays(-19),
                        Tags = context.Tags.Take(2).ToList(),
                        Image = "2.jpg",
                        UserId = 1,
                        Comments = new List<Comment>
                        {
                            new Comment {Text= "lorem ipsum", PublishedOn = DateTime.UtcNow.AddDays(-10), UserId = 1},
                            new Comment {Text= "lorem ipsum", PublishedOn = DateTime.UtcNow.AddDays(-5), UserId = 1},
                        }
                    }
                    );

                context.SaveChanges();
            }
        }
    }
}
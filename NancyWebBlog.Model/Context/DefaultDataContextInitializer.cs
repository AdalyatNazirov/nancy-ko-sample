using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace NancyWebBlog.Model
{
    public class DefaultDataContextInitializer : DropCreateDatabaseIfModelChanges<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            var newCategory = new Category()
            {
                Name = "C#"
            };
            context.Categories.Add(newCategory);
            newCategory = new Category()
            {
                Name = ".Net"
            };
            context.Categories.Add(newCategory);
            newCategory = new Category()
            {
                Name = "Nancy"
            };
            context.Categories.Add(newCategory);
            context.SaveChanges();

            Post newPost = new Post()
            {
                Author = "Adel",
                Title = "Post #1",
                Body = "This is my first post!",
                PreBody = "First post",
                PostedAt = DateTime.Now,
                LogoUrl = "",
                Comments = new List<Comment>() { 
                    new Comment(){ Author="Andrew", PostedAt = DateTime.Now, Text="nice job"},
                    new Comment(){ Author="Rob", PostedAt = DateTime.Now, Text="very cool"},
                },
                Categories = context.Categories.ToList()
            };
            context.Posts.Add(newPost);
            context.Comments.AddRange(newPost.Comments);

            newPost = new Post()
            {
                Author = "Adel",
                Title = "Post #2",
                Body = "This is my second post!",
                PreBody = "Second post",
                PostedAt = DateTime.Now,
                LogoUrl = "",
                Comments = new List<Comment>() { 
                    new Comment(){ Author = "Bob",
                                    PostedAt = DateTime.Now,
                                    Text = "good post"}
                },
                Categories = new List<Category>() { 
                    context.Categories.FirstOrDefault()
                }
            };
            context.Posts.Add(newPost);
            context.Comments.AddRange(newPost.Comments);

            context.SaveChanges();
        }
    }
}

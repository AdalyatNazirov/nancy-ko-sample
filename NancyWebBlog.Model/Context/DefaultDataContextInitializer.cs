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
            Post newPost = new Post()
            {
                Author = "Adel",
                Title = "Post #1",
                Body = "This is my first post!",
                PreBody = "First post",
                PostedAt = DateTime.Now,
                LogoUrl=""
            };
            context.Posts.Add(newPost);
            newPost = new Post()
            {
                Author = "Adel",
                Title = "Post #2",
                Body = "This is my second post!",
                PreBody = "Second post",
                PostedAt = DateTime.Now,
                LogoUrl = ""
            };
            context.Posts.Add(newPost);

            context.SaveChanges();
        }
    }
}

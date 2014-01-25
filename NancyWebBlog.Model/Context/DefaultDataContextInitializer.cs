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
                Title = "Congratulation!",
                Body = "This is my first post!",
                PreBody = "First post",
                PostedAt = DateTime.Now
            };
            context.Posts.Add(newPost);
            context.SaveChanges();
        }
    }
}

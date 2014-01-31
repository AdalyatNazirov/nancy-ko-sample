using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace NancyWebBlog.Model
{
    public class BlogContext : DbContext
    {
        static BlogContext()
        {
            Database.SetInitializer<BlogContext>(new DefaultDataContextInitializer());
        }

        public BlogContext()
            : base("Name=BlogContext")
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.Post)
                .WithMany(c => c.Comments)
                .WillCascadeOnDelete(true);
        }


    }
}

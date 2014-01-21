using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using NancyWebBlog.Model;

namespace NancyWebBlog.Repository
{
    public class UnitOfWork : IDisposable
    {
        private DbContext context = new BlogContext();

        private Repository<Post> postRepository;

        public Repository<Post> PostRepository
        {
            get
            {
                if (this.postRepository == null)
                {
                    this.postRepository = new Repository<Post>(context);
                }
                return postRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

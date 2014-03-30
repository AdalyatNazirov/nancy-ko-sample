using Nancy;
using Nancy.ModelBinding;
using NancyWebBlog.Models;
using NancyWebBlog.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace NancyWebBlog.Modules
{
    public class PostModule : NancyModule
    {
        public PostModule()
            : base("/post")
        {
            Get["/"] = SendPostPreviews;

            Post["/page/{page:int}/{itemsPerPage?10}"] = SendPostPreviews;

            Get["/{id}"] = SendPostById;

            Delete["/{id}"] = DeletePostById;
        }

        private Response SendPostPreviews(dynamic parameters)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                int page = parameters.page ?? 1;
                int itemsPerPage = parameters.itemsPerPage ?? 10;
                string[] categories = new string[0];
                var modelIDs = this.Bind<CategoryIDs>();
                if (modelIDs!=null && modelIDs.categories!=null)
                {
                    categories = modelIDs.categories.ToArray<string>();
                }

                var previews = unitOfWork.PostRepository
                    .Get( filter: item => categories.Count()==0 ||  item.Categories.Select(cat=>cat.Name).Intersect(categories).Count()!=0,
                          orderBy: model => model.OrderBy(p => p.PostedAt))
                    .Select(post =>
                        new PostPreviewModel(post));

                int count = unitOfWork.PostRepository.Count();

                return JsonConvert.SerializeObject(
                    new { previews = previews.Skip((page-1)*itemsPerPage).Take(itemsPerPage), 
                          count
                    });
            }
        }

        private Response SendPostById(dynamic parameters)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                int id = parameters.id;
                var post = unitOfWork.PostRepository
                    .GetByID(id);
                var nextPost = unitOfWork.PostRepository
                    .Get(filter: p => p.PostedAt > post.PostedAt,
                           orderBy: arr => arr.OrderBy(item => item.PostedAt))
                    .FirstOrDefault();
                var prevPost = unitOfWork.PostRepository
                    .Get(filter: p => p.PostedAt < post.PostedAt,
                           orderBy: arr => arr.OrderByDescending(item => item.PostedAt))
                    .FirstOrDefault();
                var related = unitOfWork.PostRepository
                    .Get(filter: item=> item.ID!=id)
                    .OrderByDescending(item=>item.Categories.Intersect(post.Categories).Count())
                    .Take(3)
                    .Select(item => new PostPreviewModel(item));
                return JsonConvert.SerializeObject(new
                    {
                        post,
                        related,
                        neighbors = new
                        {
                            prev = prevPost != null ? new PostPreviewModel(prevPost) : null,
                            next = nextPost != null ? new PostPreviewModel(nextPost) : null
                        }
                    });
            }
        }

        private Response DeletePostById(dynamic parameters)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                int id = parameters.id;
                unitOfWork.PostRepository.Delete(id);
                return "succeded";
            }
        }
    }
}
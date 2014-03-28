using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using NancyWebBlog.Repository;
using Newtonsoft.Json;
using NancyWebBlog.Models;

namespace NancyWebBlog.Modules
{
    public class PostModule : NancyModule
    {
        public PostModule()
            : base("/post")
        {
            Get["/"] = SendAllPostPreviews;

            Get["/all"] = SendAllPostPreviews;

            Get["/{id}"] = SendPostById;

            Get["/category/{categoryId}"] = SendPostPreviewsByCategories;

            Delete["/{id}"] = DeletePostById;
        }

        private Response SendAllPostPreviews(dynamic parameters)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var previews = unitOfWork.PostRepository
                    .Get(orderBy: model => model.OrderBy(p => p.PostedAt))
                    .Select(post =>
                        new PostPreviewModel(post));
                return JsonConvert.SerializeObject(previews);
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

        private dynamic SendPostPreviewsByCategories(dynamic parameters)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                int catId = parameters.categoryId;
                var previews = unitOfWork.PostRepository
                    .Get(filter: p => p.Categories
                                .Select(s => s.ID).Contains(catId),
                         orderBy: model => model.OrderBy(p => p.PostedAt))
                    .Select(post =>
                        new PostPreviewModel(post));
                return JsonConvert.SerializeObject(previews);
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
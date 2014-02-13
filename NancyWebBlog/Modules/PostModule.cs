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
            Get["/"] = _ =>
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                return JsonConvert.SerializeObject(unitOfWork.PostRepository.Get().First());
            };

            Get["/all"] = SendAllPostPreviews;

            Get["/{id}"] = SendPostById;

            Get["/category/{categoryId}"] = SendPostOnCategories;

            Delete["/{id}"] = DeletePostById;
        }

        private Response SendAllPostPreviews(dynamic parameters)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var previews = unitOfWork.PostRepository.Get()
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
                return JsonConvert.SerializeObject(post);
            }
        }

        private dynamic SendPostOnCategories(dynamic parameters)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                int catId = parameters.categoryId;
                var previews = unitOfWork.PostRepository
                    .Get(filter: p=> p.Categories
                                .Select(s=>s.ID).Contains(catId))
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
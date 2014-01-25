using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using NancyWebBlog.Repository;
using Newtonsoft.Json;

namespace NancyWebBlog.Modules
{
    public class PostModule : NancyModule
    {
        public PostModule():base("/post")
        {
            Get["/"] = _ =>
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                return JsonConvert.SerializeObject(unitOfWork.PostRepository.Get().First());
            };

            Get["/all"] = SendAllPosts;
        }

        private Response SendAllPosts(dynamic parameters)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return JsonConvert.SerializeObject(unitOfWork.PostRepository.Get());
        }
    }
}
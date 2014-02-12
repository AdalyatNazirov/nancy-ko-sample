using Nancy;
using NancyWebBlog.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyWebBlog.Modules
{
    public class CategoryModule:NancyModule
    {
        public CategoryModule():base("/category/")
        {
            Get["/all"] = param =>
                {
                    using (var unit = new UnitOfWork())
                    {
                        var categories = unit.CategoryRepository.Get();
                        return JsonConvert.SerializeObject(categories);
                    }
                };
        }
    }
}
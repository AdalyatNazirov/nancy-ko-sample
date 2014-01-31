using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyWebBlog.Modules
{
    public class AdminModule : NancyModule
    {
        public AdminModule()
            : base("/admin")
        {
            Get["/"] = parameters => View["Views/Admin/list"];
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace NancyWebBlog.Modules
{
    public class AboutModule:NancyModule
    {
        public AboutModule():base("/about")
        {
            Get["/"] = _ => View["Default/contact"];
        }
    }
}
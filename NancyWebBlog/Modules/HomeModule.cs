using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using NancyWebBlog.Repository;
using Newtonsoft.Json;

namespace NancyWebBlog.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
            : base("/")
        {
            Get["/"] = _ => View["Default/index"];

            Get["/about"] = _ => View["Default/about"];

            Get["/blog"] = _ => View["Default/blog"];
        }
    }
}
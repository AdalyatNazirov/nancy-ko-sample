using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NancyWebBlog.Model;

namespace NancyWebBlog.Models
{
    public class PostPreviewModel
    {
        public PostPreviewModel(Post post)
        {
            ID = post.ID;
            Author = post.Author;
            LogoUrl = post.LogoUrl;
            PreBody = post.PreBody;
            PostedAt = post.PostedAt;
            Title = post.Title;
        }

        public int ID { get; set; }

        public string Author { get; set; }

        public string LogoUrl { get; set; }

        public string PreBody { get; set; }

        public DateTime PostedAt { get; set; }

        public string Title { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NancyWebBlog.Model
{
    public class Post
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string PreBody { get; set; }
        
        public string Body { get; set; }
        
        public DateTime PostedAt { get; set; }
        
        public string Author { get; set; }
    }
}

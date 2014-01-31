using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NancyWebBlog.Model
{
    public class Comment
    {
        public int ID { get; set; }

        public int PostID { get; set; }

        public string Author { get; set; }

        public string Text { get; set; }

        public DateTime PostedAt { get; set; }

        //need to solve Self reference loop while serialing
        [JsonIgnore]
        public virtual Post Post { get; set; }
    }
}

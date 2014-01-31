using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace NancyWebBlog.Model
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Title { get; set; }

        public string PreBody { get; set; }
        
        public string Body { get; set; }
        
        public DateTime PostedAt { get; set; }
        
        public string Author { get; set; }

        public string LogoUrl { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}

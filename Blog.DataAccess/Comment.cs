using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DataAccess
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
    }
}

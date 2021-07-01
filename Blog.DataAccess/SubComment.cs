using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DataAccess
{
    public class SubComment : Comment
    {
        public int MainCommentId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DataAccess
{
    public class MainComment : Comment
    {
        public List<SubComment> SubComments { get; set; }
    }
}

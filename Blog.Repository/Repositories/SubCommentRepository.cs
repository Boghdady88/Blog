using Blog.DataAccess;
using Blog.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Repository.Repositories
{
    public interface ISubCommentRepository
    { }

    public class SubCommentRepository : Repository<SubComment>, ISubCommentRepository
    {
        public SubCommentRepository(AppDbContext ctx) : base(ctx)
        {
        }

    }
}

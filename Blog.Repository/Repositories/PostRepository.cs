using Blog.DataAccess;
using Blog.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Repository.Repositories
{
    public interface IPostRepository
    { }

    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext ctx) : base(ctx)
        {
        }

        public override IQueryable<Post> GetAll()
        {
            return base.GetAll();
        }


    }
}

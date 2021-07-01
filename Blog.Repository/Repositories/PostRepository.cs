using Blog.DataAccess;
using Blog.DataAccess.Data;
using Blog.DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
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

        public IndexViewModel GetAllPosts(
        int pageNumber,
        string category,
        string search)
        {
            Func<Post, bool> InCategory = (post) => { return post.Category.ToLower().Equals(category.ToLower()); };

            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);

            var query = GetAll();

            if (!String.IsNullOrEmpty(category))
                query = query.Where(x => InCategory(x));

            if (!String.IsNullOrEmpty(search))
                query = query.Where(x => EF.Functions.Like(x.Title, $"%{search}%")
                                    || EF.Functions.Like(x.Body, $"%{search}%")
                                    || EF.Functions.Like(x.Description, $"%{search}%"));
            int postsCount = 0;
            int pageCount = 0;
            IEnumerable<int> vPages = Enumerable.Empty<int>();
            var res = query.Skip(skipAmount).Take(pageSize).AsEnumerable();
            if (res != null && res.Count() != 0)
            {
                postsCount = query.Count();
                pageCount = (int)Math.Ceiling((double)postsCount / pageSize);
                vPages = PageHelper.PageNumbers(pageNumber, pageCount).ToList();
            }

            return new IndexViewModel
            {
                PageNumber = pageNumber,
                PageCount = pageCount,
                NextPage = postsCount > skipAmount + pageSize,
                Pages = vPages,
                Category = category,
                Search = search,
                Posts = res
            };
        }

    }
}

using Blog.DataAccess.Data;
using Blog.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _ctx;
        public UnitOfWork(AppDbContext ctx)
        {
            _ctx = ctx;
            _ctx.ChangeTracker.LazyLoadingEnabled = true;
        }
        public PostRepository _PostRepository => new PostRepository(_ctx);

        public SubCommentRepository _SubCommentRepository => new SubCommentRepository(_ctx);

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public int SaveChangesAsync()
        {
            return _ctx.SaveChanges();
        }
    }
}

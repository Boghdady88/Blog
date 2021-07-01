using Blog.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        PostRepository _PostRepository { get; }
        SubCommentRepository _SubCommentRepository { get; }
         int SaveChangesAsync();
    }
}

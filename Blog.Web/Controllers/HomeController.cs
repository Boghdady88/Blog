using Blog.Repository;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blog.DataAccess;
using Blog.Service.FileManager;

namespace Blog.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IFileManager _fileManager;

        public HomeController(
            IUnitOfWork unitOfWork,
            IFileManager fileManager
            )
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
        }

        public IActionResult Index(int pageNumber, string category, string search)
        {
            if (pageNumber < 1)
                return RedirectToAction("Index", new { pageNumber = 1, category });

            var vm = _unitOfWork._PostRepository.GetAllPosts(pageNumber, category, search);

            return View(vm);
        }

        public IActionResult Post(int id) =>
            View(_unitOfWork._PostRepository.GetById(id));

        [HttpGet("/Image/{image}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult Image(string image) =>
             new FileStreamResult(
                 _fileManager.ImageStream(image),
                 $"image/{image.Substring(image.LastIndexOf('.') + 1)}");

        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Post", new { id = vm.PostId });

            var post = _unitOfWork._PostRepository.GetById(vm.PostId);
            if (vm.MainCommentId == 0)
            {
                post.MainComments = post.MainComments ?? new List<MainComment>();

                post.MainComments.Add(new MainComment
                {
                    Message = vm.Message,
                    Created = DateTime.Now,
                });

                _unitOfWork._PostRepository.Update(post);
            }
            else
            {
                var comment = new SubComment
                {
                    MainCommentId = vm.MainCommentId,
                    Message = vm.Message,
                    Created = DateTime.Now,
                };
                _unitOfWork._SubCommentRepository.Add(comment);
            }

            _unitOfWork.SaveChangesAsync();

            return RedirectToAction("Post", new { id = vm.PostId });
        }



    }
}

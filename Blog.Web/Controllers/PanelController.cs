using Blog.DataAccess;
using Blog.Repository;
using Blog.Service.FileManager;
using Blog.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IFileManager _fileManager;

        public PanelController(
            IUnitOfWork unitOfWork,
            IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
        }
        public IActionResult Index()
        {
            var posts = new HashSet<Post>();
            posts = _unitOfWork._PostRepository.GetAll().ToHashSet();

            return View(posts);
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return View(new PostViewModel());
            }
            else
            {
                var post = _unitOfWork._PostRepository.GetById((int)Id);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    CurrentImage = post.Image,
                    Description = post.Description,
                    Category = post.Category,
                    Tags = post.Tags
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = new Post
            {
                Id = vm.Id,
                Title = vm.Title,
                Body = vm.Body,
                Description = vm.Description,
                Category = vm.Category,
                Tags = vm.Tags,
            };

            if (vm.Img == null)
                post.Image = vm.CurrentImage;
            else
            {
                if (!string.IsNullOrEmpty(vm.CurrentImage))
                    _fileManager.RemoveImage(vm.CurrentImage);

                post.Image = await _fileManager.SaveImage(vm.Img);
            }

            if (post.Id > 0)
                _unitOfWork._PostRepository.Update(post);
            else
                _unitOfWork._PostRepository.Add(post);

            var res = _unitOfWork.SaveChangesAsync();
            if (res > 0)
                return RedirectToAction("Index");
            else
                return View(vm);
        }

        [HttpGet]
        public IActionResult Remove(int Id)
        {
            _unitOfWork._PostRepository.Delete(Id);
            _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

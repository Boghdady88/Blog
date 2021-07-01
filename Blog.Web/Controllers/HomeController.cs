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

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var posts = new HashSet<Post>();
            posts = _unitOfWork._PostRepository.GetAll().ToHashSet();

            return View(posts);
        }

        public IActionResult Post(int Id)
        {

            var post = _unitOfWork._PostRepository.GetById((int)Id);
            var model = new PostViewModel
            {
                Id = post.Id,
                Body = post.Body,
                Title = post.Title,
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            var postview = new PostViewModel();
            if (Id == null)
            {
                return View(postview);
            }
            else
            {
                var post = _unitOfWork._PostRepository.GetById((int)Id);
                postview.Id = post.Id;
                postview.Body = post.Body;
                postview.Title = post.Title;
                return View(postview);

            }
        }
        [HttpPost]
        public IActionResult Edit(PostViewModel post)
        {
            var entity = new Post
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body
            };

            if (post.Id > 0)
            {
                _unitOfWork._PostRepository.Update(entity);
            }
            else
            {
                _unitOfWork._PostRepository.Add(entity);
            }

            _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            _unitOfWork._PostRepository.Delete(Id);
            _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

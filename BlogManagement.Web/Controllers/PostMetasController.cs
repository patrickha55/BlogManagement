using BlogManagement.Common.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.Web.Controllers
{
    [Authorize]
    public class PostMetasController : Controller
    {   
        // GET: PostMetasController
        [Route("Admins/PostMetas")]
        [Authorize(Roles = Roles.Administrator)]
        public ActionResult Index()
        {
            return View();
        }

        // GET: PostMetasController/Details/5
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Author}")]
        public ActionResult Details(long id)
        {
            return View();
        }

        // GET: PostMetasController/Create
        [Authorize(Roles = $"{Roles.Administrator}")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostMetasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Author}")]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostMetasController/Edit/5
        [Authorize(Roles = $"{Roles.Administrator}")]
        public ActionResult Edit(long id)
        {
            return View();
        }

        // POST: PostMetasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Author}")]
        public ActionResult Edit(long id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: PostMetasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Author}")]
        public ActionResult Delete(long id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

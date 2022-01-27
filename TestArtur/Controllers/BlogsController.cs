using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestArtur.Data;
using TestArtur.Models;
using TestArtur.Services.Blogs;

namespace TestArtur.Controllers
{
    public class BlogsController : Controller
    {
        //private readonly NovostiContext _context;

        private IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // GET: Blogs
        public async Task<IActionResult> Index(string searchString, int? teg)
        {
            var list = _blogService.List(searchString, teg);

            var listViewModel = new List<BlogViewModel>();

            foreach (var item in list)
            {
                var blogs = new BlogViewModel()
                {
                    Id = item.Id,
                    Zagolovok = item.Zagolovok,
                    Opisanie = item.Opisanie,
                    Kartinka = item.Kartinka,
                    Datadobavleniya = item.Datadobavleniya,
                    
                    Teg = item.Teg != null ? item.Teg.Nazvanie : "",
                    //TegId = item.TegId
                };
                listViewModel.Add(blogs);
            }
            ViewData["Teg"] = new SelectList(_blogService.TegList(), "Id", "Nazvanie");

            return View(await Task.Run(() => listViewModel));

            //return View(await _context.BlogViewModel.ToListAsync());
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = _blogService.GetById((int)id);

            //var blogViewModel = await _context.BlogViewModel
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            var blogViewModel = new BlogViewModel()
            {
                Id = blog.Id,
                Zagolovok = blog.Zagolovok,
                Opisanie = blog.Opisanie,
                Kartinka = blog.Kartinka,
                Datadobavleniya = blog.Datadobavleniya,

                Teg = blog.Teg != null ? blog.Teg.Nazvanie : "",
                TegId = blog.TegId
            };

            return View(await Task.Run(() => blogViewModel));

            //return View(blogViewModel);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            ViewData["Teg"] = new SelectList(_blogService.TegList(), "Id", "Nazvanie");
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogViewModel blogViewModel, IFormFile uploadFile)
        {
            if (ModelState.IsValid)
            {
                var blog = new Blog()
                {
                    Id = blogViewModel.Id,
                    Zagolovok = blogViewModel.Zagolovok,
                    Opisanie = blogViewModel.Opisanie,
                    Datadobavleniya = blogViewModel.Datadobavleniya,
                    TegId = blogViewModel.TegId
                };

                _blogService.Create(blog, uploadFile);
                return RedirectToAction(await Task.Run(() => nameof(Index)));

                //_context.Add(blogViewModel);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            ViewData["Teg"] = new SelectList(_blogService.TegList(), "Id", "Nazvanie", blogViewModel.TegId);
            return View(await Task.Run(() => blogViewModel));
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var blog = _blogService.GetById((int)id);

            //var blogViewModel = await _context.BlogViewModel.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            var blogViewModel = new BlogViewModel()
            {
                Id = blog.Id,
                Zagolovok = blog.Zagolovok,
                Opisanie = blog.Opisanie,
                Kartinka = blog.Kartinka,
                Datadobavleniya = blog.Datadobavleniya,
                TegId = blog.TegId
            };
            ViewData["Teg"] = new SelectList(_blogService.TegList(), "Id", "Nazvanie", blog.TegId);
            return View(await Task.Run(() => blogViewModel));
            //return View(blogViewModel);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogViewModel blogViewModel, IFormFile uploadFile)
        {
            if (id != blogViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogService.Update(id, blogViewModel, uploadFile);
                    //_context.Update(blogViewModel);
                    //await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    if (!BlogExists(blogViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(await Task.Run(() => nameof(Index)));
            }
            ViewData["Teg"] = new SelectList(_blogService.TegList(), "Id", "Nazvanie", blogViewModel.TegId);
            return View(await Task.Run(() => blogViewModel));
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = _blogService.GetById((int)id);
            //var blogViewModel = await _context.BlogViewModel
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            var blogViewModel = new BlogViewModel()
            {
                Id = blog.Id,
                Zagolovok = blog.Zagolovok,
                Opisanie = blog.Opisanie,
                Kartinka = blog.Kartinka,
                Datadobavleniya = blog.Datadobavleniya,
                Teg = blog.Teg != null ? blog.Teg.Nazvanie : ""
            };

            return View(await Task.Run(() => blogViewModel));
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var blogViewModel = await _context.BlogViewModel.FindAsync(id);
            //_context.BlogViewModel.Remove(blogViewModel);
            //await _context.SaveChangesAsync();
            _blogService.Delete(id);
            return RedirectToAction(await Task.Run(() => nameof(Index)));
        }

        private bool BlogExists(int id)
        {
            return _blogService.BlogExists(id);
        }
    }
}

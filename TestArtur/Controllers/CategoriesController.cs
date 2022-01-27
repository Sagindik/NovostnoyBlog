using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestArtur.Data;
using TestArtur.Models;
using TestArtur.Services.Categorys;

namespace TestArtur.Controllers
{
    public class CategoriesController : Controller
    {
        private ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var list = _categoryService.List();

            var listViewModel = new List<CategoryViewModel>();

            foreach (var item in list)
            {
                var tegs = new CategoryViewModel()
                {
                    Id = item.Id,
                    Naimenovanie = item.Naimenovanie,
                };
                listViewModel.Add(tegs);
            }

            return View(await Task.Run(() => listViewModel));
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetById((int)id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryViewModel = new CategoryViewModel()
            {
                Id = category.Id,
                Naimenovanie = category.Naimenovanie,
            };

            return View(await Task.Run(() => categoryViewModel));
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            ViewData["Tegs"] = new SelectList(_categoryService.TegList(), "Id", "Nazvanie");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category, List<Int32> list)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Create(category, list);
                return RedirectToAction(await Task.Run(() => nameof(Index)));
            }

            ViewData["Tegs"] = new SelectList(_categoryService.TegList(), "Id", "Nazvanie");
            return View(await Task.Run(() => category));
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetById((int)id);
            
            if (category == null)
            {
                return NotFound();
            }

            var categoryViewModel = new CategoryViewModel()
            {
                Id = category.Id,
                Naimenovanie = category.Naimenovanie,
            };

            return View(await Task.Run(() => categoryViewModel));
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category, List<Int32> list)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryService.Update(id, category, list);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
        
            ViewData["Tegs"] = new SelectList(_categoryService.TegList(), "Id", "Nazvanie");
            return View(await Task.Run(() => category));
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetById((int)id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryViewModel = new CategoryViewModel()
            {
                Id = category.Id,
                Naimenovanie = category.Naimenovanie,
            };

            return View(await Task.Run(() => categoryViewModel));
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _categoryService.Delete(id);
            return RedirectToAction(await Task.Run(() => nameof(Index)));
        }

        private bool CategoryExists(int id)
        {
            return _categoryService.CategoryExists(id);
        }
    }
}

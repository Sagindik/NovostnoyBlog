using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestArtur.Data;
using TestArtur.Models;
using TestArtur.Services.Tegs;

namespace TestArtur.Controllers
{
    public class TegsController : Controller
    {
        private ITegService _tegService;

        public TegsController(ITegService tegservice)
        {
            _tegService = tegservice;
        }

        // GET: Tegs
        public async Task<IActionResult> Index()
        {
            var list = _tegService.List();

            var listViewModel = new List<TegViewModel>();

            foreach (var item in list)
            {
                var tegs = new TegViewModel()
                {
                    Id = item.Id,
                    Nazvanie = item.Nazvanie,
                    
                    Category = item.Category != null ? item.Category.Naimenovanie : ""
                };
                listViewModel.Add(tegs);
            }

            ViewData["Category"] = new SelectList(_tegService.CategoryList(), "Id", "Naimenovanie");

            return View(await Task.Run(() => listViewModel));

            //var list = _tegService.List();
            //return View(await Task.Run(() => list));
        }
        /*var tegViewModel = new TegViewModel()
        {
            Id = teg.Id,
            Nazvanie = teg.Nazvanie
            Zagolovok = novost.Zagolovok,
            Vidimost = novost.Vidimost,
            Teg = novost.Teg != null ? novost.Teg.Nazvanie : ""
        };

            return View(await Task.Run(() => novostViewModel));*/


        // GET: Tegs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teg = _tegService.GetById((int)id);

            if (teg == null)
            {
                return NotFound();
            }

            var tegViewModel = new TegViewModel()
            {
                Id = teg.Id,
                Nazvanie = teg.Nazvanie,
                Category = teg.Category != null ? teg.Category.Naimenovanie : ""
            };

            return View(await Task.Run(() => tegViewModel));
        }

        // GET: Tegs/Create
        public IActionResult Create()
        {
            ViewData["Novosts"] = new SelectList(_tegService.NovostList(null), "Id", "Zagolovok");
            ViewData["Category"] = new SelectList(_tegService.CategoryList(), "Id", "Naimenovanie");
            return View();
        }

        // POST: Tegs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TegViewModel tegViewModel, List<Int32> list)
        {
            if (ModelState.IsValid)
            {
                var teg = new Teg()
                {
                    Id = tegViewModel.Id,
                    Nazvanie = tegViewModel.Nazvanie,
                    CategoryId = tegViewModel.CategoryId
                };

                _tegService.Create(teg, list);
                return RedirectToAction(await Task.Run(() => nameof(Index)));
            }
            ViewData["Category"] = new SelectList(_tegService.CategoryList(), "Id", "Naimenovanie", tegViewModel.CategoryId);
            return View(await Task.Run(() => tegViewModel));

            /*if (ModelState.IsValid)
            {
                _tegService.Create(teg, list);
                return RedirectToAction(await Task.Run(() => nameof(Index)));
            }
            ViewData["Category"] = new SelectList(_tegService.CategoryList(), "Id", "Naimenovanie", teg.CategoryId);
            ViewData["Novosts"] = new SelectList(_tegService.NovostList(null), "Id", "Zagolovok");
            return View(await Task.Run(() => teg));*/
        }

        // GET: Tegs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var teg = _tegService.GetById((int)id);
            if (teg == null)
            {
                return NotFound();
            }


            var tegViewModel = new TegViewModel()
            {
                Id = teg.Id,
                Nazvanie = teg.Nazvanie,
                CategoryId = teg.CategoryId
            };

            ViewData["Category"] = new SelectList(_tegService.CategoryList(), "Id", "Naimenovanie", teg.CategoryId);
            return View(await Task.Run(() => tegViewModel));




            //ViewData["Category"] = new SelectList(_tegService.CategoryList(), "Id", "Naimenovanie", teg.CategoryId);
            //ViewData["Novosts"] = new SelectList(_tegService.NovostList(null), "Id", "Zagolovok");
            //return View(await Task.Run(() => teg));
        }

        // POST: Tegs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Teg teg, List<Int32> list)
        {
            if (id != teg.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _tegService.Update(id, teg, list);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TegExists(teg.Id))
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
            ViewData["Categorys"] = new SelectList(_tegService.CategoryList(), "Id", "Naimenovanie", teg.CategoryId);

            return View(await Task.Run(() => teg));


            /*
            ViewData["Category"] = new SelectList(_tegService.CategoryList(), "Id", "Naimenovanie", teg.CategoryId);
            ViewData["Novosts"] = new SelectList(_tegService.NovostList(null), "Id", "Zagolovok");
            return View(await Task.Run(() => teg));
            */
        }

        // GET: Tegs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teg = _tegService.GetById((int)id);

            if (teg == null)
            {
                return NotFound();
            }

            var tegViewModel = new TegViewModel()
            {
                Id = teg.Id,
                Nazvanie = teg.Nazvanie,
                Category = teg.Category != null ? teg.Category.Naimenovanie : ""
            };

            return View(await Task.Run(() => tegViewModel));



        }

        // POST: Tegs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _tegService.Delete(id);
            return RedirectToAction(await Task.Run(() => nameof(Index)));
        }

        private bool TegExists(int id)
        {
            return _tegService.TegExists(id);

        }
    }
}

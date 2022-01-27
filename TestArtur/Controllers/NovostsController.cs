using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestArtur.Data;
using TestArtur.Models;
using TestArtur.Services.Novosts;

namespace TestArtur.Controllers
{
    public class NovostsController : Controller
    {
        private INovostService _novostService;
       
        public NovostsController(INovostService novostService)
        {
            _novostService = novostService;
        }

        // GET: Novosts
        public async Task<IActionResult> Index(string searchString, int? teg)
        {
            var list = _novostService.List(searchString, teg);

            var listViewModel = new List<NovostViewModel>();

            foreach (var item in list)
            {
                var news = new NovostViewModel()
                {
                    Id = item.Id,
                    Kartinka = item.Kartinka,
                    Datadobavleniya = item.Datadobavleniya,
                    Zagolovok = item.Zagolovok,
                    Vidimost = item.Vidimost,
                    Teg = item.Teg != null ? item.Teg.Nazvanie : "",
                    TegId = item.TegId 
                };
                listViewModel.Add(news);
            }

            //ViewData["Teg"] = new SelectList(_novostService.TegList(), "Id", "Nazvanie");

            return View(await Task.Run(() => listViewModel));

            //var list = _novostService.List(searchString, teg);
            //return View(await Task.Run(() => list));
        }


        // GET: Novosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novost = _novostService.GetById((int)id);

            if (novost == null)
            {
                return NotFound();
            }

            var novostViewModel = new NovostViewModel()
            {
                Id = novost.Id,
                Kartinka = novost.Kartinka,
                Datadobavleniya = novost.Datadobavleniya,
                Zagolovok = novost.Zagolovok,
                Vidimost = novost.Vidimost,
                Teg = novost.Teg != null ? novost.Teg.Nazvanie : "",
                TegId = novost.TegId
            };
            
            return View(await Task.Run(() => novostViewModel));
        }

        // GET: Novosts/Create
        public IActionResult Create()
        {
            ViewData["Teg"] = new SelectList(_novostService.TegList(), "Id", "Nazvanie");
            return View();
        }

        // POST: Novosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NovostViewModel novostViewModel, IFormFile uploadFile)
        {                                  
            if (ModelState.IsValid)
            {
                var novost = new Novost()
                {
                    Id = novostViewModel.Id,
                    Datadobavleniya = novostViewModel.Datadobavleniya,
                    Zagolovok = novostViewModel.Zagolovok,
                    Vidimost = novostViewModel.Vidimost,
                    TegId = novostViewModel.TegId
                };

                _novostService.Create(novost, uploadFile);
                return RedirectToAction(await Task.Run(() => nameof(Index)));
            }
            ViewData["Teg"] = new SelectList(_novostService.TegList(), "Id", "Nazvanie", novostViewModel.TegId);
            return View(await Task.Run(() => novostViewModel));
        }

        // GET: Novosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novost = _novostService.GetById ((int) id);
            if (novost == null)
            {
                return NotFound();
            }

            var novostViewModel = new NovostViewModel()
            {
                Id = novost.Id,
                Kartinka = novost.Kartinka,
                Datadobavleniya = novost.Datadobavleniya,
                Zagolovok = novost.Zagolovok,
                Vidimost = novost.Vidimost,
                TegId = novost.TegId
            };

            ViewData["Teg"] = new SelectList(_novostService.TegList(), "Id", "Nazvanie", novost.TegId);
            return View(await Task.Run(() => novostViewModel));


            //ViewData["Teg"] = new SelectList(_novostService.TegList(), "Id", "Nazvanie", novost.TegId);
            //return View(await Task.Run(() => novost));
        }

        // POST: Novosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NovostViewModel novostViewModel, IFormFile uploadFile)
        {
            if (id != novostViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _novostService.Update(id, novostViewModel, uploadFile);
                }
                catch(Exception ex)
                {
                    if (!NovostExists(novostViewModel.Id))
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

            ViewData["Teg"] = new SelectList(_novostService.TegList(), "Id", "Nazvanie", novostViewModel.TegId);
            return View(await Task.Run(() => novostViewModel));
        }

        // GET: Novosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novost = _novostService.GetById((int) id);
            
            if (novost == null)
            {
                return NotFound();
            }

            var novostViewModel = new NovostViewModel()
            {
                Id = novost.Id,
                Kartinka = novost.Kartinka,
                Datadobavleniya = novost.Datadobavleniya,
                Zagolovok = novost.Zagolovok,
                Vidimost = novost.Vidimost,
                Teg = novost.Teg != null ? novost.Teg.Nazvanie : ""
            };

            return View(await Task.Run(() => novostViewModel));

            //return View(await Task.Run(() => novost));
        }

        // POST: Novosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _novostService.Delete(id);
            return RedirectToAction(await Task.Run(() => nameof(Index)));
        }

        private bool NovostExists(int id)
        {
            return _novostService.NovostExists(id);
        }
    }
}

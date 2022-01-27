using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestArtur.Models;
using TestArtur.Services.Novosts;

namespace TestArtur.Controllers
{
    public class HomeController : Controller
    {
        private INovostService _novostService;
        
        public HomeController(INovostService novostService)
        {
            _novostService = novostService;
        }

        public async Task<IActionResult> Index(string searchString, int? teg)
        {
            var list = _novostService.List(searchString, teg);

            var listViewModel = new List<NovostViewModel>();

            foreach(var item in list.Where(_=>_.Vidimost))
            {
                var news = new NovostViewModel()
                {
                    Id = item.Id,
                    Zagolovok = item.Zagolovok,
                    Datadobavleniya = item.Datadobavleniya,
                    Vidimost = item.Vidimost,
                    Kartinka = item.Kartinka,
                    
                    Teg = item.Teg!=null? item.Teg.Nazvanie:""
                };
                listViewModel.Add(news);
            }

            ViewData["Teg"] = new SelectList(_novostService.TegList(), "Id", "Nazvanie");

            return View(await Task.Run(() => listViewModel));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Novosti()
        {
            return View();
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

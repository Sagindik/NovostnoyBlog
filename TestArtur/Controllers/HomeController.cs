using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestArtur.Models;
using TestArtur.Services.Novosts;
using TestArtur.Services.Blogs;

namespace TestArtur.Controllers
{
    public class HomeController : Controller
    {
        private INovostService _novostService;
        private IBlogService _blogService;
        
        public HomeController(INovostService novostService, IBlogService blogService)
        {
            _novostService = novostService;
            _blogService = blogService;
        }

        public async Task<IActionResult> Index(string searchString, int? teg)
        {
            var listnovost = _novostService.List(searchString, teg);
            var listblog = _blogService.List(searchString, teg);

            var listnovostblogViewModel = new NovostBlogViewModel();

            var listnovostViewModel = new List<NovostViewModel>();
            var listblogViewModel = new List<BlogViewModel>();

            foreach (var item in listnovost.Where(_=>_.Vidimost))
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
                listnovostViewModel.Add(news);
            }

            foreach (var item in listblog)
            {
                var blogs = new BlogViewModel()
                {
                    Id = item.Id,
                    Zagolovok = item.Zagolovok,
                    Opisanie = item.Opisanie,
                    Datadobavleniya = item.Datadobavleniya,
                    Kartinka = item.Kartinka,

                    Teg = item.Teg != null ? item.Teg.Nazvanie : ""
                };
                listblogViewModel.Add(blogs);
            }
            listnovostblogViewModel.novostViewModels = listnovostViewModel;
            listnovostblogViewModel.blogViewModels = listblogViewModel;

            ViewData["Teg"] = new SelectList(_novostService.TegList(), "Id", "Nazvanie");

            return View(await Task.Run(() => listnovostblogViewModel));
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

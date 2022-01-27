using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestArtur.Data;
using TestArtur.Models;

namespace TestArtur.Services.Novosts
{
    public class NovostService : INovostService
    {
        private NovostiContext _context;
        IWebHostEnvironment _appEnvironment;

        public NovostService(NovostiContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public List<Novost> List(string searchString, int? teg)
        {
            var novosts = _context.Novosts.Include(s => s.Teg).AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                novosts = novosts.Where(s => s.Zagolovok.Contains(searchString));
            }

            if (teg is not null)
            {
                novosts = novosts.Where(i => i.TegId == teg);
            }

            return novosts.ToList();
        }

        public List<Teg> TegList()
        {
            var tegList = _context.Tegs.ToList();
            return tegList;
        }

        public Novost GetById(int id)
        {
            var novost = _context.Novosts.Include(s => s.Teg).FirstOrDefault(m => m.Id == id);
            return novost;
        }

        public void Create(Novost novost, IFormFile uploadFile)
        {
            _context.Add(novost);
            if (uploadFile != null)
            {
                string path = "/Images/" + uploadFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadFile.CopyTo(fileStream);
                }
                novost.Kartinka = path;
            }
            _context.SaveChanges();
        }

        public void Update(int id, NovostViewModel novostViewModel, IFormFile uploadFile)
        {
            var novostContext = _context.Novosts.FirstOrDefault(i => i.Id == id);

            novostContext.Id = novostViewModel.Id;
            novostContext.Datadobavleniya = novostViewModel.Datadobavleniya;
            novostContext.Zagolovok = novostViewModel.Zagolovok;
            novostContext.Vidimost = novostViewModel.Vidimost;
            novostContext.TegId = novostViewModel.TegId;

            if (uploadFile != null)
            {
                string path = "/Images/" + uploadFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadFile.CopyTo(fileStream);
                }
                novostContext.Kartinka = path;
            }
            _context.Novosts.Update(novostContext);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var novost = GetById(id);
            _context.Remove(novost);
            _context.SaveChanges();
        }

        public bool NovostExists(int id)
        {
            return _context.Novosts.Any(m => m.Id == id);
        }
    }
}

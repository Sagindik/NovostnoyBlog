using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestArtur.Data;
using TestArtur.Models;

namespace TestArtur.Services.Blogs
{
    public class BlogService : IBlogService
    {
        private NovostiContext _context;
        IWebHostEnvironment _appEnvironment;

        public BlogService(NovostiContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public List<Blog> List(string searchString, int? teg)
        {
            var blogs = _context.Blogs.Include(s => s.Teg).AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                blogs = blogs.Where(s => s.Zagolovok.Contains(searchString));
            }

            if (teg is not null)
            {
                blogs = blogs.Where(i => i.TegId == teg);
            }

            return blogs.ToList();
        }

        public List<Teg> TegList()
        {
            var tegList = _context.Tegs.ToList();
            return tegList;
        }

        public Blog GetById(int id)
        {
            var blog = _context.Blogs.Include(s => s.Teg).FirstOrDefault(m => m.Id == id);
            return blog;
        }

        public void Create(Blog blog, IFormFile uploadFile)
        {
            _context.Add(blog);
            if (uploadFile != null)
            {
                string path = "/Images/" + uploadFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadFile.CopyTo(fileStream);
                }
                blog.Kartinka = path;
            }
            _context.SaveChanges();
        }

        public void Update(int id, BlogViewModel blogViewModel, IFormFile uploadFile)
        {
            var blogContext = _context.Blogs.FirstOrDefault(i => i.Id == id);

            blogContext.Id = blogViewModel.Id;
            blogContext.Zagolovok = blogViewModel.Zagolovok;
            blogContext.Opisanie = blogViewModel.Opisanie;
            blogContext.Datadobavleniya = blogViewModel.Datadobavleniya;
            blogContext.TegId = blogViewModel.TegId;

            if (uploadFile != null)
            {
                string path = "/Images/" + uploadFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadFile.CopyTo(fileStream);
                }
                blogContext.Kartinka = path;
            }
            _context.Blogs.Update(blogContext);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var blog = GetById(id);
            _context.Remove(blog);
            _context.SaveChanges();
        }

        public bool BlogExists(int id)
        {
            return _context.Blogs.Any(m => m.Id == id);
        }

    }
}
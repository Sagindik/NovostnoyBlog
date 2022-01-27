using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestArtur.Data;
using TestArtur.Models;

namespace TestArtur.Services.Blogs
{
    public interface IBlogService
    {
        List<Blog> List(string SearchString, int? teg);
        void Create(Blog blog, IFormFile uploadFile);
        Blog GetById(int id);
        void Update(int id, BlogViewModel blogViewModel, IFormFile uploadFile);
        void Delete(int id);
        bool BlogExists(int id);
        List<Teg> TegList();
    }
}

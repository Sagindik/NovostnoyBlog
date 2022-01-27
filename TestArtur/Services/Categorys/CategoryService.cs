using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TestArtur.Data;

namespace TestArtur.Services.Categorys
{
    public class CategoryService : ICategoryService
    {

        private NovostiContext _context;

        public CategoryService(NovostiContext context)
        {
            _context = context;
        }

        public List<Category> List()
        {
            var list = _context.Categorys.Include(c => c.Tegs).ToList();
            return list;
        }

        public List<Teg> TegList()
        {
            var tegList = _context.Tegs.ToList();
            return tegList;
        }

        public void Create([Bind("Id,Naimenovanie")] Category category, List<Int32> list)
        {
            _context.Add(category);

            var tegChoice = new List<Teg>();

            foreach (var i in list)
            {
                tegChoice.Add(_context.Tegs.FirstOrDefault(m => m.Id == i));
            }
            category.Tegs = tegChoice;
            
            _context.SaveChanges();
        }

        public Category GetById(int id)
        {
            var category = _context.Categorys
                .Include(c => c.Tegs)
                .FirstOrDefault(m => m.Id == id);
            return category;
        }

        public void Update(int id, [Bind("Id,Naimenovanie")] Category category, List<Int32> list)
        {
            var category1 = _context.Categorys
                .Include(m => m.Tegs).FirstOrDefault(m => m.Id == category.Id);
            _context.Entry(category1).CurrentValues.SetValues(category);

            var tegChoice = new List<Teg>();

            foreach (var i in list)
            {
                tegChoice.Add(_context.Tegs.FirstOrDefault(m => m.Id == i));
            }
            category1.Tegs = tegChoice;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Categorys.Remove(GetById(id));
            _context.SaveChanges();
        }

        public bool CategoryExists(int id)
        {
            return _context.Categorys.Any(e => e.Id == id);
        }

    }
}

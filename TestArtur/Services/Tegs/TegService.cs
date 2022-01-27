using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TestArtur.Data;

namespace TestArtur.Services.Tegs
{
    public class TegService : ITegService
    {
        private NovostiContext _context;

        public TegService(NovostiContext context)
        {
            _context = context;
        }

        public List<Teg> List()
        {
            var list = _context.Tegs.Include(c => c.Novosts).Include(c => c.Category).ToList();
            return list;
        }

        public List<Novost> NovostList(int? id)
        {
            var novostContext = _context.Novosts.Include(s => s.Teg).ToList();
            var novostList = new List<Novost>();

            foreach (var i in novostContext)
            {
                if(i.Teg is null || i.Teg is not null && i.TegId == id)
                {
                    novostList.Add(i);
                }
            }

            return novostList;
        }

        public List<Category> CategoryList()
        {
            var categoryList = _context.Categorys.ToList();
            return categoryList;
        }


        public void Create(Teg teg, List<Int32> list)
        {
            _context.Tegs.Add(teg);
            var novostChoice = new List<Novost>();
            foreach(var i in list)
            {
                novostChoice.Add(_context.Novosts.FirstOrDefault(m => m.Id == i));
            }
          
            teg.Novosts = novostChoice;

            _context.SaveChanges();
        }

        public Teg GetById(int id)
        {
            var teg = _context.Tegs.Include(c => c.Novosts).Include(c => c.Category)
                .FirstOrDefault(m => m.Id == id);
            return teg;
        }

        public void Update(int id, [Bind("Id, Naimenovanie")] Teg teg, List<Int32> list)
        {
            var teg1 = _context.Tegs.Include(m => m.Novosts).FirstOrDefault(m => m.Id == teg.Id);
            _context.Entry(teg1).CurrentValues.SetValues(teg);

            var novostChoice = new List<Novost>();
            foreach (var i in list)
            {
                novostChoice.Add(_context.Novosts.FirstOrDefault(m => m.Id == i));
            }
            teg1.Novosts = novostChoice;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Remove(GetById(id));
            _context.SaveChanges();
        }

        public bool TegExists(int id)
        {
            return _context.Tegs.Any(m => m.Id == id);
        }
    }
}

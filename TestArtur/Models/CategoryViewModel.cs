using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestArtur.Data;

namespace TestArtur.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Naimenovanie { get; set; }

        public ICollection<Teg> Tegs { get; set; }
    }
}

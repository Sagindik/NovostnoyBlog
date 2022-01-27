using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TestArtur.Data
{
    public class Teg
    {
        public int Id { get; set; }
        public string Nazvanie { get; set; }
        public ICollection<Novost> Novosts { get; set; }
        public ICollection<Blog> Blogs { get; set; }

        public Category Category { get; set; }
        public int? CategoryId { get; set; }

    }
}

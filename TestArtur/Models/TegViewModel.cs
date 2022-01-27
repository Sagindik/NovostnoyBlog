using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestArtur.Data;

namespace TestArtur.Models
{
    public class TegViewModel
    {

        public int Id { get; set; }
        public string Nazvanie { get; set; }

        public ICollection <Novost> Novosts { get; set; }
        public ICollection<Blog> Blogs { get; set; }

        public string Category { get; set; }
        public int? CategoryId { get; set; }
    }
}

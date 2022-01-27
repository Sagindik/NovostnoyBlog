using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TestArtur.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Naimenovanie { get; set; }
        public ICollection<Teg> Tegs { get; set; }


        
        
        

    }
}

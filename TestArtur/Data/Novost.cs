using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestArtur.Data
{
    public class Novost
    {
        public int Id { get; set; }
        public string Zagolovok { get; set; }
        public DateTime Datadobavleniya {get; set; }
        public bool Vidimost { get; set; }
        public string Kartinka { get; set; }
        
        public Teg Teg { get; set; }
        public int? TegId { get; set; }

    }
}

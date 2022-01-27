using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestArtur.Models;

namespace TestArtur.Data
{
    public class DbInitializer
    {
        public static void Initialize(NovostiContext context) 
        {

            context.Database.EnsureCreated();

            if (context.Novosts.Any())
            {
                return;
            }


            var categorys = new Category[]
            {
                new Category{Naimenovanie="Спорт"},
                new Category{Naimenovanie="Экономика"},
                new Category{Naimenovanie="Культура"}
            };

            foreach (Category c in categorys)
            {
                context.Categorys.Add(c);
            }

            var tegs = new Teg[]
            {
                new Teg{Nazvanie="Футбол"},
                new Teg{Nazvanie="Кино"},
                new Teg{Nazvanie="Банки"}
            };

            tegs[0].Category = categorys[0];
            tegs[1].Category = categorys[0];
            tegs[2].Category = categorys[0];

            foreach (Teg t in tegs)
            {
                context.Tegs.Add(t);
            }

            var novosts = new Novost[]
            {
                new Novost{Zagolovok="Месси получил золотой мяч", Datadobavleniya=DateTime.Parse("2001-06-15"), Vidimost=true, Kartinka=""},
                new Novost{Zagolovok="Банки по всему миру закрылись", Datadobavleniya=DateTime.Parse("2001-06-16"), Vidimost=true, Kartinka=""},
                new Novost{Zagolovok="Кинотеатры стали самым посещаемым местом за август", Datadobavleniya=DateTime.Parse("2001-06-17"), Vidimost=true, Kartinka=""}
            };

            novosts[0].Teg = tegs[0];
            novosts[1].Teg = tegs[0];
            novosts[2].Teg = tegs[0];


            foreach (Novost n in novosts)
            {
                context.Novosts.Add(n);
            }


            var blogs = new Blog[]
            {
                new Blog{Zagolovok="Месси получил золотой мяч", Opisanie="Это описание для новости Месси получил золотой мяч", Kartinka="", Datadobavleniya=DateTime.Parse("2000-06-15")},
                new Blog{Zagolovok="Банки по всему миру закрылись", Opisanie="Это описание для новости Банки по всему миру закрылись", Kartinka="", Datadobavleniya=DateTime.Parse("2000-06-16")},
                new Blog{Zagolovok="Кинотеатры стали самым посещаемым местом за август", Opisanie="Это описание для новости Кинотеатры стали самым посещаемым местом за август", Kartinka="", Datadobavleniya=DateTime.Parse("2000-06-17")},
                new Blog{Zagolovok="Ученные были в шоке когда это узна...", Opisanie="Футболист - самая высоко оплачиваемая профессия", Kartinka="", Datadobavleniya=DateTime.Parse("2000-06-18")}
            };

            novosts[0].Teg = tegs[0];
            novosts[1].Teg = tegs[0];
            novosts[2].Teg = tegs[0];
            novosts[3].Teg = tegs[0];


            foreach (Novost n in novosts)
            {
                context.Novosts.Add(n);
            }



            context.SaveChanges();

        }
    }
}

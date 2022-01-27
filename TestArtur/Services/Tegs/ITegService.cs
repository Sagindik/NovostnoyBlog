using System;
using System.Collections.Generic;
using TestArtur.Data;

namespace TestArtur.Services.Tegs
{
    public interface ITegService
    {
        List<Teg> List();
        void Create(Teg teg, List<Int32> list);
        Teg GetById(int id);
        void Update(int id, Teg teg, List<Int32> list);
        void Delete(int id);
        bool TegExists(int id);
        List<Category> CategoryList();
        List<Novost> NovostList(int? id);
    }
}

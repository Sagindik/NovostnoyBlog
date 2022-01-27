using System;
using System.Collections.Generic;
using TestArtur.Data;

namespace TestArtur.Services.Categorys
{
    public interface ICategoryService
    {
        List<Category> List();
        void Create(Category category, List<Int32> list);
        Category GetById(int id);
        void Update(int id, Category category, List<Int32> list);
        void Delete(int id);
        bool CategoryExists(int id);
        List<Teg> TegList();
    }
}

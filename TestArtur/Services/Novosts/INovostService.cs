using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using TestArtur.Data;
using TestArtur.Models;

namespace TestArtur.Services.Novosts
{
    public interface INovostService
    {
        List<Novost> List(string SearchString, int? teg);
        void Create(Novost novost, IFormFile uploadFile);
        Novost GetById(int id);
        void Update(int id, NovostViewModel novostViewModel, IFormFile uploadFile);
        void Delete(int id);
        bool NovostExists(int id);
        List<Teg> TegList();
    }
}

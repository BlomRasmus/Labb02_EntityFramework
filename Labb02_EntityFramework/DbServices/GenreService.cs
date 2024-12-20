using Labb02_EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_EntityFramework.DbServices
{
    internal class GenreService
    {
        public List<Genre> GetGenres()
        {
            using EveryloopContext db = new();
            return db.Genres.ToList();
        }
    }
}

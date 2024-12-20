using Labb02_EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_EntityFramework.DbServices
{
    internal class MediaTypeService
    {
        public List<MediaType> GetMediaTypes()
        {
            using EveryloopContext db = new();
            return db.MediaTypes.ToList();
        }
    }
}

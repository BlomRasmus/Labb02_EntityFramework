﻿using Labb02_EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_EntityFramework.DbServices
{
    internal class AlbumService
    {
        public List<Album> GetAlbums()
        {
            using EveryloopContext db = new();
            return db.Albums.ToList();
        }
    }
}
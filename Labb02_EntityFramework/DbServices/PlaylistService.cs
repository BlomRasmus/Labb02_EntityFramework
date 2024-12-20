using Labb02_EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_EntityFramework.DbServices
{
    internal class PlaylistService
    {
        public List<Playlist> GetPlaylists()
        {
            using EveryloopContext db = new EveryloopContext();
            return db.Playlists.ToList();
        }


    }
}

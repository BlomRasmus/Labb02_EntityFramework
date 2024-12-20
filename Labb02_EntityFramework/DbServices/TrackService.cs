using Labb02_EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_EntityFramework.DbServices
{
    internal class TrackService
    {
        public List<Track> GetTracks()
        {
            using EveryloopContext db = new EveryloopContext();
            return db.Tracks.ToList();
        }
        public List<Track> GetActiveTracks(int playlistId)
        {
            using EveryloopContext db = new EveryloopContext();
            List<int> TrackIds = db.PlaylistTracks
                .Where(p => p.PlaylistId == playlistId)
                .Select(t => t.TrackId)
                .ToList();

            return db.Tracks
            .Where(t => TrackIds.Contains(t.TrackId))
            .ToList();
        }
    }
}

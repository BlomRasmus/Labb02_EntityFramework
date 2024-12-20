using System;
using System.Collections.Generic;

namespace Labb02_EntityFramework.Model;

public partial class Playlist
{
    public int PlaylistId { get; set; }

    public string? Name { get; set; }

    public override string ToString()
    {
        return $"{Name}";
    }
}

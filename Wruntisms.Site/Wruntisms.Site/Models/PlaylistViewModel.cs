namespace Wruntisms.Site.Models
{
    using System.Collections.Generic;

    public class PlaylistViewModel
    {
        public string PlaylistName { get; set; }
        public string PlaylistKey { get; set; }
        public List<SongViewModel> Songs { get; set; }
        public bool Validated { get; set; }
    }
}
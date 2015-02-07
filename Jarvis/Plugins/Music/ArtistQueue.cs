using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torshify;

namespace Jarvis.Plugins.Music
{
    public class ArtistQueue : IPlayerQueue
    {
        private readonly List<ITrack> _tracks; 

        public ArtistQueue(IArtist artist)
        {
            _tracks = new List<ITrack>();

            var browse = artist.Browse();
            browse.WaitForCompletion();

            foreach (var track in browse.Tracks)
            {
                track.Load();
                track.WaitUntilLoaded();
                if (track.Availability == TrackAvailablity.Available)
                    _tracks.Add(track);
            }
            _tracks = _tracks.OrderByDescending((t) => t.Popularity).ToList();
        }

        public ITrack this[int i]
        {
            get { return _tracks[i]; }
        }

        public int Count
        {
            get { return _tracks.Count; }
        }
    }
}

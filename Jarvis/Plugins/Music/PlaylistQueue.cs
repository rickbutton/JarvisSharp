using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torshify;

namespace Jarvis.Plugins.Music
{
    public class PlaylistQueue : IPlayerQueue
    {
        private readonly List<ITrack> _tracks; 

        public PlaylistQueue(IPlaylist playlist)
        {
            _tracks = new List<ITrack>();
            foreach (var track in playlist.Tracks)
            {
                track.Load();
                track.WaitUntilLoaded();
                if (track.Availability == TrackAvailablity.Available)
                    _tracks.Add(track);
            }
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

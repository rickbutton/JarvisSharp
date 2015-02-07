using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torshify;

namespace Jarvis.Plugins.Music
{
    public class SearchQueue : IPlayerQueue
    {
        private ISession _session;
        private ISearch _search;
        private int _count;

        private List<ITrack> _tracks; 

        public SearchQueue(ISession session, ISearch search)
        {
            _session = session;
            _search = search;
            _count = _search.TotalTracks;

            _tracks = _search.Tracks.Select((t) =>
            {
                t.Load();
                t.WaitUntilLoaded();
                return t;
            }).ToList();
        }

        public SearchQueue(ISession session, ISearch search, int count)
        {
            _session = session;
            _search = search;
            _count = count;

            _tracks = _search.Tracks.Select((t) =>
            {
                t.Load();
                t.WaitUntilLoaded();
                return t;
            }).ToList();
        }

        public ITrack this[int i]
        {
            get
            {
                if (i > _count - 1)
                {
                    throw new IndexOutOfRangeException();
                }
                while  (i > _tracks.Count - 1)
                {
                    LoadNextSection();
                }
                return _tracks[i];
            }
        }

        private void LoadNextSection()
        {
            var newSearch = _session.Search(_search.Query,
                _tracks.Count, 25, 0, 0, 0, 0, 0, 0, SearchType.Standard);
            _search = newSearch;

            foreach (var track in _search.Tracks)
            {
                track.Load();
                track.WaitUntilLoaded();
                _tracks.Add(track);
            }
        }

        public int Count
        {
            get { return _count; }
        }
    }
}

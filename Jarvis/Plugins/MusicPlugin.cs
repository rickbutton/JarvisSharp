using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Plugins.Music;
using Jarvis.Wit;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using Torshify;

namespace Jarvis.Plugins
{
    public class MusicPlugin : IIntentHandler
    {
        private ISession _session;

        private readonly Player _player;

        public MusicPlugin()
        {
            var t = Login();
            t.Wait();
            _player = new Player(_session);
        }

        public JarvisResponse Handle(JarvisIntent intent)
        {
            var o = intent.Intent.BestOutcome;

            if (o.Intent == "action")
            {
                return HandleAction(o);
            }
            else if (o.Intent == "music")
            {
                return HandleMusic(o);
            }
            else
            {
                return JarvisResponse.Unknown;
            }
        }

        public ICollection<string> IntentsToListen { get { return new List<string>() { "music" , "action" };} }

        private JarvisResponse HandleAction(WitOutcome o)
        {
            var action = o.GetFirstString("action").Value;

            switch (action)
            {
                case "next":
                    _player.Next();
                    break;
                case "prev":
                    _player.Previous();
                    break;
                case "play":
                    _player.Play();
                    break;
                case "pause":
                    _player.Pause();
                    break;
            }
            return JarvisResponse.None;
        }

        private JarvisResponse HandleMusic(WitOutcome o)
        {
            var query = o.GetFirstString("music_query").Value;

            // check playlists
            var playlists = SearchPlaylists(query);

            if (GetBestDistance(query) < 5) // matches playlist most probably
            {
                PlayPlaylist(playlists.First());
            } else if (query.IndexOf(" by ") != -1) // matches song/artist combo probably
            {
                var sections = query.Split(new string[] {" by "}, StringSplitOptions.RemoveEmptyEntries);

                if (sections.Length == 2)
                {
                    var song = sections[0];
                    var artist = sections[1];

                    if (song == "song" || song == "songs" || song == "music") // special case
                    {
                        return HandleArtist(artist);
                    }

                    return HandleSong(song, artist);
                }
            }
            else
            {
                var search = Search(query);
                if (search.Tracks.Count > 0)
                {
                    PlaySearch(search);
                    return JarvisResponse.None;
                }
            }
            

            return JarvisResponse.Unknown;
        }

        private JarvisResponse HandleSong(string song, string artist)
        {
            var searchQuery = "track:\"" + song + "\" artist:\"" + artist + "\"";
            var search = Search(searchQuery);

            if (search.Tracks.Count > 0)
            {
                PlaySong(search);
                return JarvisResponse.None;
            }
            return JarvisResponse.Unknown;
        }

        private JarvisResponse HandleArtist(string artist)
        {
            var searchQuery = "artist:\"" + artist + "\"";
            var search = Search(searchQuery);

            if (search.Artists.Count > 0)
            {
                PlayArtist(search.Artists.First());
                return JarvisResponse.None;
            }
            return JarvisResponse.Unknown;
        }

        private void PlaySearch(ISearch search)
        {
            _player.Reset();
            _player.LoadSearch(search);
            _player.Play();
        }

        private void PlaySong(ISearch search)
        {
            _player.Reset();
            _player.LoadSong(search);
            _player.Play();
        }

        private void PlayPlaylist(IPlaylist playlist)
        {
            playlist.WaitUntilLoaded();
            Console.WriteLine("will play " + playlist.Name);

            _player.Reset();
            _player.LoadPlaylist(playlist);
            _player.Play();
        }

        private void PlayArtist(IArtist artist)
        {
            _player.Reset();
            _player.LoadArtist(artist);
            _player.Play();
        }

        private Task<bool> Login()
        {
            _session = SessionFactory.CreateSession(
                Constants.ApplicationKey,
                Path.Combine(Directory.GetCurrentDirectory(), "SpotifyCache").ToString(),
                Path.Combine(Directory.GetCurrentDirectory(), "SpotifySettings").ToString(),
                "jarvis");

            return Task.Run(() =>
            {
                var t = new TaskCompletionSource<bool>();

                _session.LoginComplete += (sender, e) =>
                {
                    t.TrySetResult(true);
                    LoginComplete(sender, e);
                };
                _session.ConnectionError += (sender, e) =>
                {
                    t.TrySetResult(false);
                    ConnectionError(sender, e);
                };

                _session.Login("ricky715", "twhpirg123", true);
                return t.Task;
            });
        }

        private IEnumerable<IPlaylist> SearchPlaylists(string query)
        {
            _session.PlaylistContainer.WaitUntilLoaded();
            var playlists = _session.PlaylistContainer.Playlists;
            var distances = new List<int>();
            foreach (var p in playlists)
            {
                p.WaitUntilLoaded();
                distances.Add(ComputeDistance(query, p.Name));
            }

            IContainerPlaylist temp;
            int distTemp;
            var array = playlists.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (distances[i] > distances[j])
                    {
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;

                        distTemp = distances[i];
                        distances[i] = distances[j];
                        distances[j] = distTemp;
                    }
                }
            }
            return array.ToList();
        }

        private int GetBestDistance(string query)
        {
            _session.PlaylistContainer.WaitUntilLoaded();
            var playlists = _session.PlaylistContainer.Playlists;
            var distances = new List<int>();
            foreach (var p in playlists)
            {
                p.WaitUntilLoaded();
                distances.Add(ComputeDistance(query, p.Name));
            }

            int temp;
            var array = distances.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                    {
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
            return array[0];
        }

        private ISearch Search(string query)
        {
            return Search(query, 0);
        }

        private ISearch Search(string query, int offset)
        {
            var search = _session.Search(query, offset, 25, offset, 25, offset, 25, offset, 25, SearchType.Standard);
            search.WaitForCompletion();
            return search;
        }

        private void LoginComplete(object sender, SessionEventArgs e)
        {
        }

        private void ConnectionError(object sender, SessionEventArgs e)
        {
            
        }

        // Levenshtein distance
        private int ComputeDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
    }
}

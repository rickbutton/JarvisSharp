using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using Torshify;

namespace Jarvis.Plugins.Music
{
    public class Player
    {
        private WaveFormat _format;
        private BufferedWaveProvider _provider;
        private WaveOut _out;

        private bool _playing;

        private IPlayerQueue _queue;
        private ISession _session;

        private int _trackIndex = 0;

        public Player(ISession session)
        {
            _session = session;

            _session.MusicDeliver += HandleAudio;
            _session.EndOfTrack += HandleEndOfTrack;
        }

        private void LoadTrack(int index)
        {
            if (_playing)
            {
                _session.PlayerUnload();
            }
            var track = _queue[index];
            _session.PlayerLoad(track);
            _trackIndex = index;
        }

        private void HandleEndOfTrack(object sender, SessionEventArgs e)
        {
            if (_trackIndex < _queue.Count - 1) // there are more tracks
            {
                LoadTrack(_trackIndex + 1); // load next track
                Play(true);
            }
            else
            {
                Stop(); // out of tracks
                _trackIndex = 0;
            }
        }

        public void LoadSong(ISearch search)
        {
            _queue = new SearchQueue(_session, search, 1);
            LoadTrack(0);
        }

        public void LoadSearch(ISearch search)
        {
            _queue = new SearchQueue(_session, search);
            LoadTrack(0);
        }

        public void LoadPlaylist(IPlaylist playlist)
        {
            _queue = new PlaylistQueue(playlist);
            LoadTrack(0);
        }

        public void LoadArtist(IArtist artist)
        {
            _queue = new ArtistQueue(artist);
            LoadTrack(0);
        }

        public void Reset()
        {
            Stop();
            ClearQueue();
        }

        public void ClearQueue()
        {
            _queue = null;
        }

        public void Stop()
        {
            Pause();
            if (_provider != null)
                _provider.ClearBuffer();
        }

        public void Next()
        {
            _provider.ClearBuffer();
            if (_trackIndex < _queue.Count - 1)
            {
                LoadTrack(_trackIndex + 1);
            }
            else
            {
                LoadTrack(0);
            }
            Play(true);
        }

        public void Previous()
        {
            _provider.ClearBuffer();
            if (_trackIndex > 0)
            {
                LoadTrack(_trackIndex - 1);
            }
            else
            {
                LoadTrack(_queue.Count - 1);
            }
            Play(true);
        }

        public void HandleAudio(object sender, MusicDeliveryEventArgs e)
        {
            if (_format == null)
            {
                _format = new WaveFormat(e.Rate, e.Channels);
            }

            if (_provider == null)
            {
                _provider = new BufferedWaveProvider(_format);
            }

            var consumed = 0;
            if (_playing && (_provider.BufferLength - _provider.BufferedBytes) > e.Samples.Length)
            {
                _provider.AddSamples(e.Samples, 0, e.Samples.Length);
                consumed = e.Frames;
            }
            e.ConsumedFrames = consumed;

            if (_out == null)
            {
                _out = new WaveOut();
                _out.Init(_provider);
                if (_playing)
                    _out.Play();
                else
                    _out.Pause();
            }
        }

        public void Play(bool forcePlay = false)
        {
            if (!_playing || forcePlay)
            {
                _session.PlayerPlay();
                if (_out != null)
                    _out.Play();
            }
            _playing = true;
        }

        public void Pause()
        {
            if (_playing)
            {
                _session.PlayerPause();
                if (_out != null)
                    _out.Pause();
            }
            _playing = false;
        }
    }
}

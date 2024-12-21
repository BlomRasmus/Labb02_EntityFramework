using Labb02_EntityFramework.Command;
using Labb02_EntityFramework.DbServices;
using Labb02_EntityFramework.DialogServices;
using Labb02_EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Labb02_EntityFramework.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Playlist> Playlists { get; set; }
        public ObservableCollection<Track> Tracks { get; set; }
        public ObservableCollection<Track> ActiveTracks { get; set; }
        public ObservableCollection<Artist> Artists { get; set; }
        public ObservableCollection<Album> Albums { get; set; }




        public PlaylistService PlaylistService { get; }
        public TrackService TrackService { get; set; }
        public DialogService DialogService { get; set; }
        public ArtistService ArtistService { get; set; }
        public AlbumService AlbumService { get; set; }



        private Playlist _activePlaylist;

        public Playlist ActivePlaylist
        {
            get { return _activePlaylist; }
            set 
            {
                _activePlaylist = value;

                ActiveTracks = new ObservableCollection<Track>(TrackService.GetActiveTracks(_activePlaylist.PlaylistId));

                RaisePropertyChanged();
                RaisePropertyChanged("ActiveTracks");
            }
        }
        private Playlist _newPlaylist;

        public Playlist NewPlaylist
        {
            get { return _newPlaylist; }
            set { _newPlaylist = value; RaisePropertyChanged(); }
        }

        private Track _selectedTrack;

        public Track SelectedTrack
        {
            get { return _selectedTrack; }
            set { _selectedTrack = value; RaisePropertyChanged(); }
        }

        private Track _selectedPlaylistTrack;

        public Track SelectedPlaylistTrack
        {
            get { return _selectedPlaylistTrack; }
            set { _selectedPlaylistTrack = value; RaisePropertyChanged(); }
        }

        private bool _isTracksVisible;

        public bool IsTracksVisible
        {
            get { return _isTracksVisible; }
            set { _isTracksVisible = value; RaisePropertyChanged(); }
        }
        private bool _isArtistsVisible;

        public bool IsArtistsVisible
        {
            get { return _isArtistsVisible; }
            set { _isArtistsVisible = value; RaisePropertyChanged(); }
        }
        private bool _isAlbumsVisible;

        public bool IsAlbumsVisible
        {
            get { return _isAlbumsVisible; }
            set { _isAlbumsVisible = value; RaisePropertyChanged(); }
        }
        private Artist _selectedArtist;

        public Artist SelectedArtist
        {
            get { return _selectedArtist; }
            set { _selectedArtist = value; RaisePropertyChanged(); }
        }
        private Album _selectedAlbum;

        public Album SelectedAlbum
        {
            get { return _selectedAlbum; }
            set { _selectedAlbum = value; RaisePropertyChanged(); }
        }







        public DelegateCommand AddPlaylistCommand { get; }
        public DelegateCommand RemovePlaylistCommand { get; }
        public DelegateCommand CreatePlaylistCommand { get; }
        public DelegateCommand AddToPlaylistCommand { get; }
        public DelegateCommand RemoveFromPlaylistCommand { get; }
        public DelegateCommand CreateTrackCommand { get; }
        public DelegateCommand RemoveTrackCommand { get; }
        public DelegateCommand CreateArtistCommand { get; }
        public DelegateCommand CreateAlbumCommand { get; }
        public DelegateCommand RemoveArtistCommand { get; }
        public DelegateCommand RemoveAlbumCommand { get; }
        public DelegateCommand SetTrackVisibilityCommand { get; }
        public DelegateCommand SetAlbumVisibilityCommand { get; }
        public DelegateCommand SetArtistVisibilityCommand { get; }
        public DelegateCommand EditTrackCommand { get; }
        public DelegateCommand EditAlbumCommand { get; }
        public DelegateCommand EditArtistCommand { get; }
        public DelegateCommand RefreshCommand { get; }







        public CreateStuffViewModel CreateStuffViewModel { get; }
        public MainWindowViewModel()
        {
            CreateStuffViewModel = new CreateStuffViewModel(this);

            PlaylistService = new PlaylistService();
            TrackService = new TrackService();
            DialogService = new DialogService();
            ArtistService = new ArtistService();
            AlbumService = new AlbumService();
            SelectedTrack = new Track();
            SelectedArtist = new Artist();
            SelectedAlbum = new Album();
            SelectedPlaylistTrack = new Track();

            AddPlaylistCommand = new DelegateCommand(AddPlaylist);
            RemovePlaylistCommand = new DelegateCommand(RemovePlayList);
            CreatePlaylistCommand = new DelegateCommand(CreatePlaylist);
            AddToPlaylistCommand = new DelegateCommand(AddToPlaylist);
            RemoveFromPlaylistCommand = new DelegateCommand(RemoveFromPlaylist);
            CreateTrackCommand = new DelegateCommand(CreateTrack);
            RemoveTrackCommand = new DelegateCommand(RemoveTrack);
            CreateArtistCommand = new DelegateCommand(CreateArtist);
            CreateAlbumCommand = new DelegateCommand(CreateAlbum);
            SetTrackVisibilityCommand = new DelegateCommand(SetTrackVisibility);
            SetArtistVisibilityCommand = new DelegateCommand(SetArtistVisibility);
            SetAlbumVisibilityCommand = new DelegateCommand(SetAlbumVisibility);
            RemoveAlbumCommand = new DelegateCommand(RemoveAlbum);
            RemoveArtistCommand = new DelegateCommand(RemoveArtist);
            EditTrackCommand = new DelegateCommand(EditTrack);
            EditAlbumCommand = new DelegateCommand(EditAlbum);
            EditArtistCommand = new DelegateCommand(EditArtist);
            RefreshCommand = new DelegateCommand(Refresh);


            Playlists = new ObservableCollection<Playlist>(PlaylistService.GetPlaylists());
            ActivePlaylist = Playlists.FirstOrDefault();
            Artists = new ObservableCollection<Artist>(ArtistService.GetArtists());
            Tracks = new ObservableCollection<Track>(TrackService.GetTracks());
            Albums = new ObservableCollection<Album>(AlbumService.GetAlbums());

            IsTracksVisible = true;
            IsArtistsVisible = false;
            IsAlbumsVisible = false;
        }


        public void CreatePlaylist(object obj)
        {
            NewPlaylist = new Playlist();
            DialogService.ShowCreatePlaylistDialog();
        }
        public void AddPlaylist(object obj)
        {
            var addedPlaylist = new Playlist() { Name = NewPlaylist.Name };

            using EveryloopContext db = new();

            db.Playlists.Add(addedPlaylist);
            Playlists.Add(addedPlaylist);

            db.SaveChanges();

            ActivePlaylist = addedPlaylist;
        }
        public void AddToPlaylist(object obj)
        {
            using EveryloopContext db = new();

            if(!ActiveTracks.Any(t => t.TrackId == SelectedTrack.TrackId))
            {
                PlaylistTrack newTrackToPlaylist = new PlaylistTrack() { PlaylistId = ActivePlaylist.PlaylistId, TrackId = SelectedTrack.TrackId };
                db.PlaylistTracks.Add(newTrackToPlaylist);
                ActiveTracks.Add(SelectedTrack);
            }

            db.SaveChanges();
        }
        public void RemoveFromPlaylist(object obj)
        {
            using EveryloopContext db = new();
            PlaylistTrack trackToDelete = new() { PlaylistId = ActivePlaylist.PlaylistId, TrackId = SelectedPlaylistTrack.TrackId };
            db.PlaylistTracks.Remove(trackToDelete);
            ActiveTracks.Remove(SelectedPlaylistTrack);

            SelectedPlaylistTrack = ActiveTracks.FirstOrDefault();
            db.SaveChanges();
        }

        public void RemovePlayList(object obj)
        {
            using EveryloopContext db = new();

            Playlist currentPlaylist = ActivePlaylist;
            db.Playlists.Remove(currentPlaylist);

            ActivePlaylist = Playlists.FirstOrDefault();
            Playlists.Remove(currentPlaylist);

            db.SaveChanges();
        }

        public void CreateTrack(object obj)
        {
            DialogService.ShowCreateTrackDialog();
        }
        public void RemoveTrack(object obj)
        {
            using EveryloopContext db = new();

            db.Tracks.Remove(SelectedTrack);
            Tracks.Remove(SelectedTrack);

            if(ActiveTracks.Any(p => p == SelectedTrack))
            {
                ActiveTracks.Remove(SelectedTrack);
            }

            db.SaveChanges();
        }

        public void CreateArtist(object obj)
        {
            DialogService.ShowCreateArtistDialog();
        }
        public void RemoveArtist(object obj)
        {
            using EveryloopContext db = new();

            db.Artists.Remove(SelectedArtist);
            Artists.Remove(SelectedArtist);
            RaisePropertyChanged(nameof(Artists));

            db.SaveChanges();
        }
        public void CreateAlbum(object obj)
        {
            DialogService.ShowCreateAlbumDialog();
        }
        public void RemoveAlbum(object obj)
        {
            using EveryloopContext db = new();

            db.Albums.Remove(SelectedAlbum);

            var tracksToRemove = Tracks.Where(t => t.AlbumId == SelectedAlbum.AlbumId).ToList();
            foreach (var track in tracksToRemove)
            {
                Tracks.Remove(track);
            }
            Albums.Remove(SelectedAlbum);

            RaisePropertyChanged(nameof(Tracks));
            RaisePropertyChanged(nameof(Albums));

            db.SaveChanges();
        }


        public void SetTrackVisibility(object obj)
        {
            IsTracksVisible = true;
            IsArtistsVisible = false;
            IsAlbumsVisible = false;
        }
        public void SetArtistVisibility(object obj)
        {
            IsTracksVisible = false;
            IsArtistsVisible = true;
            IsAlbumsVisible = false;

            SelectedArtist = Artists.FirstOrDefault();
        }
        public void SetAlbumVisibility(object obj)
        {
            IsTracksVisible = false;
            IsArtistsVisible = false;
            IsAlbumsVisible = true;

            SelectedAlbum = Albums.FirstOrDefault();
        }
        public void EditTrack(object obj)
        {
            DialogService.ShowEditTrackDialog();
        }
        public void EditAlbum(object obj)
        {
            DialogService.ShowEditAlbumDialog();
        }
        public void EditArtist(object obj)
        {
            DialogService.ShowEditArtistDialog();
        }

        public void Refresh(object obj)
        {
            if (IsTracksVisible == true)
            {
                Tracks = new ObservableCollection<Track>(TrackService.GetTracks());
                RaisePropertyChanged(nameof(Tracks));
            }
            else if(IsAlbumsVisible == true)
            {
                Albums = new ObservableCollection<Album>(AlbumService.GetAlbums());
                RaisePropertyChanged(nameof(Albums));
            }
            else if(IsArtistsVisible == true)
            {
                Artists = new ObservableCollection<Artist>(ArtistService.GetArtists());
                RaisePropertyChanged(nameof(Artists));
            }
        }
    }
}

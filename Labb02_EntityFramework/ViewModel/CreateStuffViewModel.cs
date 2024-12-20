using Labb02_EntityFramework.Command;
using Labb02_EntityFramework.DbServices;
using Labb02_EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using Track = Labb02_EntityFramework.Model.Track;

namespace Labb02_EntityFramework.ViewModel
{
    class CreateStuffViewModel : ViewModelBase
    {
        public ObservableCollection<Album> Albums { get; }
        public ObservableCollection<Genre> Genres { get; }
        public ObservableCollection<MediaType> MediaTypes { get; }
        public ObservableCollection<Artist> Artists { get; set; }

        private Track _createdTrack;

        public Track CreatedTrack
        {
            get { return _createdTrack; }
            set { _createdTrack = value; RaisePropertyChanged(); }
        }


        private string _trackName;
        public string TrackName
        {
            get { return _trackName; }
            set { _trackName = value; RaisePropertyChanged(); }
        }

        private Album _trackAlbum;
        public Album TrackAlbum
        {
            get { return _trackAlbum; }
            set { _trackAlbum = value; RaisePropertyChanged(); }
        }

        private MediaType _trackMediaType;
        public MediaType TrackMediaType
        {
            get { return _trackMediaType; }
            set { _trackMediaType = value; RaisePropertyChanged(); }
        }

        private Genre _trackGenre;
        public Genre TrackGenre
        {
            get { return _trackGenre; }
            set { _trackGenre = value; RaisePropertyChanged(); }
        }

        private string _composer;
        public string TrackComposer
        {
            get { return _composer; }
            set { _composer = value; RaisePropertyChanged(); }
        }

        private int _trackLength;
        public int TrackLength
        {
            get { return _trackLength; }
            set { _trackLength = value; RaisePropertyChanged(); }
        }

        private int _trackSize;
        public int TrackSize
        {
            get { return _trackSize; }
            set { _trackSize = value; RaisePropertyChanged(); }
        }

        private double _trackPrice;
        public double TrackPrice
        {
            get { return _trackPrice; }
            set { _trackPrice = value; RaisePropertyChanged(); }
        }

        
        private string _artistName;

        public string ArtistName
        {
            get { return _artistName; }
            set { _artistName = value; }
        }

        private string _albumTitle;

        public string AlbumTitle
        {
            get { return _albumTitle; }
            set { _albumTitle = value; }
        }
        private Artist _albumArtist;

        public Artist AlbumArtist
        {
            get { return _albumArtist; }
            set { _albumArtist = value; }
        }




        public DelegateCommand CreateTrackCommand { get; }
        public DelegateCommand CreateArtistCommand { get; }
        public DelegateCommand CreateAlbumCommand { get; }
        public DelegateCommand UpdateTrackCommand { get; }
        public DelegateCommand UpdateAlbumCommand { get; }


        public AlbumService AlbumService { get; set; }
        public GenreService GenreService { get; set; }
        public MediaTypeService MediaTypeService { get; set; }
        public ArtistService ArtistService { get; set; }

        private readonly MainWindowViewModel? mainWindowViewModel;
        public CreateStuffViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            AlbumService = new AlbumService();
            GenreService = new GenreService();
            MediaTypeService = new MediaTypeService();
            ArtistService = new ArtistService();


            Albums = new ObservableCollection<Album>(AlbumService.GetAlbums());
            Genres = new ObservableCollection<Genre>(GenreService.GetGenres());
            MediaTypes = new ObservableCollection<MediaType>(MediaTypeService.GetMediaTypes());
            Artists = new ObservableCollection<Artist>(ArtistService.GetArtists());

            CreateTrackCommand = new DelegateCommand(CreateTrack);
            CreateArtistCommand = new DelegateCommand(CreateArtist);
            CreateAlbumCommand = new DelegateCommand(CreateAlbum);
            UpdateTrackCommand = new DelegateCommand(UpdateTrack);
            UpdateAlbumCommand = new DelegateCommand(UpdateAlbum);
        }


        public void CreateTrack(object obj)
        {
            using EveryloopContext db = new EveryloopContext();

            CreatedTrack = new Track()
            {
                Name = TrackName,
                AlbumId = TrackAlbum.AlbumId,
                GenreId = TrackGenre.GenreId,
                MediaTypeId = TrackMediaType.MediaTypeId,
                Composer = TrackComposer,
                Bytes = TrackSize * 1000000,
                UnitPrice = TrackPrice,
                Milliseconds = TrackLength * 1000
            };

            db.Tracks.Add(CreatedTrack);

            mainWindowViewModel.Tracks.Add(CreatedTrack);

            db.SaveChanges();
        }

        public void CreateArtist(object obj)
        {
            using EveryloopContext db = new();

            var newArtist = new Artist() { Name = ArtistName };

            db.Artists.Add(newArtist);
            Artists.Add(newArtist);

            mainWindowViewModel.Artists.Add(newArtist);

            db.SaveChanges();
        }

        public void CreateAlbum(object obj)
        {
            using EveryloopContext db = new();

            var newAlbum = new Album()
            {
                Title = AlbumTitle,
                ArtistId = AlbumArtist.ArtistId
            };

            db.Albums.Add(newAlbum);
            Albums.Add(newAlbum);
            mainWindowViewModel.Albums.Add(newAlbum);

            db.SaveChanges();
        }
        public void UpdateTrack(object obj)
        {
            using EveryloopContext db = new();

            var trackToUpdate = db.Tracks.FirstOrDefault(track => track.TrackId == mainWindowViewModel.SelectedTrack.TrackId);

            if(trackToUpdate != null)
            {
                trackToUpdate.Milliseconds = TrackLength * 1000;
                mainWindowViewModel.SelectedTrack.Milliseconds = TrackLength * 1000;

                db.SaveChanges();
            }
        }
        public void UpdateAlbum(object obj)
        {
            using EveryloopContext db = new();

            var albumToUpdate = db.Albums.FirstOrDefault(album => album.AlbumId == mainWindowViewModel.SelectedAlbum.AlbumId);

            if (albumToUpdate != null)
            {
                albumToUpdate.Title = AlbumTitle;
                albumToUpdate.ArtistId = AlbumArtist.ArtistId;

                mainWindowViewModel.SelectedAlbum.Title = AlbumTitle;
                mainWindowViewModel.SelectedAlbum.ArtistId = AlbumArtist.ArtistId;
                RaisePropertyChanged(nameof(mainWindowViewModel.SelectedAlbum));

                db.SaveChanges();
            }
        }
    }
}

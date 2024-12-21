using Labb02_EntityFramework.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_EntityFramework.DialogServices
{
    internal class DialogService
    {
        public void ShowCreatePlaylistDialog()
        {
            var createPlaylistDialog = new AddPlaylistDialog();
            createPlaylistDialog.Show();
        }
        public void ShowCreateTrackDialog()
        {
            var createTrackDialog = new CreateNewTrackDialog();
            createTrackDialog.Show();
        }
        public void ShowCreateArtistDialog()
        {
            var createArtistDialog = new CreateArtistDialog();
            createArtistDialog.Show();
        }
        public void ShowCreateAlbumDialog()
        {
            var createAlbumDialog = new CreateAlbumDialog();
            createAlbumDialog.Show();
        }
        public void ShowEditTrackDialog()
        {
            var editTrackDialog = new EditTrackDialog();
            editTrackDialog.Show();
        }
        public void ShowEditAlbumDialog()
        {
            var editAlbumDialog = new EditAlbumDialog();
            editAlbumDialog.Show();
        }
        public void ShowEditArtistDialog()
        {
            var editArtistDialog = new EditArtistDialog();
            editArtistDialog.Show();
        }
    }
}

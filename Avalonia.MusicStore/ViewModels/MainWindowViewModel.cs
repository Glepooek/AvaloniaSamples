using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.MusicStore.Helpers;
using Avalonia.MusicStore.Views;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Avalonia.MusicStore.Models;

namespace Avalonia.MusicStore.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IRecipient<AlbumViewModel>
    {
        #region Properties

        public ObservableCollection<AlbumViewModel> Albums { get; } = new();

        #endregion

        #region Commands

        public RelayCommand BuyMusicCommand { get; private set; }
        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand UnloadedCommand { get; private set; }
        public RelayCommand ShowWebCommand { get; private set; }


        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            BuyMusicCommand = new RelayCommand(() =>
            {
                MusicStoreWindow dialog = new MusicStoreWindow();
                dialog.ShowDialog(AvaloniaHelper.GetMainWindow());
            });

            LoadedCommand = new RelayCommand(()=>
            {
                LoadAlbums();
            });

            UnloadedCommand = new RelayCommand(() => 
            {
                WeakReferenceMessenger.Default.UnregisterAll(this);
            });

            ShowWebCommand = new RelayCommand(() =>
            {
                WebViewWindow dialog = new WebViewWindow();
                dialog.ShowDialog(AvaloniaHelper.GetMainWindow());
            });

            WeakReferenceMessenger.Default.Register<AlbumViewModel>(this);
        }

        public async void Receive(AlbumViewModel message)
        {
            if (!Albums.Contains(message))
            {
                Albums.Add(message);
                await message.SaveToDiskAsync();
            }
        }

        public async void LoadAlbums()
        {
            var albums = (await Album.LoadCachedAsync()).Select(x => new AlbumViewModel(x));

            foreach (var album in albums)
            {
                Albums.Add(album);
            }

            foreach (var album in Albums.ToList())
            {
                await album.LoadCover();
            }
        }

        #endregion
    }
}

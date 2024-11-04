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
using Avalonia.MusicStore.Messages;

namespace Avalonia.MusicStore.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IRecipient<MessageParam>
    {
        #region Properties

        public ObservableCollection<AlbumViewModel> Albums { get; } = new();

        #endregion

        #region Commands

        public RelayCommand BuyMusicCommand { get; private set; }
        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand UnloadedCommand { get; private set; }
        public RelayCommand ShowWebCommand { get; private set; }
        public RelayCommand CallJSMethodCommand { get; private set; }

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            BuyMusicCommand = new RelayCommand(() =>
            {
                MusicStoreWindow dialog = new MusicStoreWindow();
                dialog.ShowDialog(AvaloniaHelper.GetMainWindow());
            });

            LoadedCommand = new RelayCommand(() =>
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
                //dialog.ShowDialog(AvaloniaHelper.GetMainWindow());
                dialog.Show(AvaloniaHelper.GetMainWindow());
            });

            CallJSMethodCommand = new RelayCommand(() =>
            {
                WeakReferenceMessenger.Default.Send<MessageParam>(new MessageParam { Reult = true });
            });

            WeakReferenceMessenger.Default.Register<MessageParam>(this);
        }

        public async void Receive(MessageParam message)
        {
            if (message == null)
            {
                return;
            }

            if (message.Data is AlbumViewModel albumVM
                && !Albums.Contains(albumVM))
            {
                Albums.Add(albumVM);
                await albumVM.SaveToDiskAsync();
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

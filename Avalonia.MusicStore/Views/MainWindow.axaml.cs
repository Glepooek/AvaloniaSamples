using Avalonia.Controls;
using Avalonia.MusicStore.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Threading.Tasks;

namespace Avalonia.MusicStore.Views
{
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Unloaded += MainWindow_Unloaded;
        }

        private void MainWindow_Unloaded(object? sender, Interactivity.RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.UnregisterAll(viewModel);
        }

        private void MainWindow_Loaded(object? sender, Interactivity.RoutedEventArgs e)
        {
            viewModel = this.DataContext as MainWindowViewModel;
            viewModel?.LoadAlbums();
        }
    }
}
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using Xilium.CefGlue.Avalonia;

namespace Avalonia.MusicStore.Views;

public partial class WebViewWindow : Window
{
    AvaloniaCefBrowser cefBrowser;

    public WebViewWindow()
    {
        InitializeComponent();
        this.Loaded += WebViewWindow_Loaded;
        this.Unloaded += WebViewWindow_Unloaded;
    }

    private void WebViewWindow_Unloaded(object? sender, Interactivity.RoutedEventArgs e)
    {
        cefBrowser?.Dispose();
    }

    private void WebViewWindow_Loaded(object? sender, Interactivity.RoutedEventArgs e)
    {
        cefBrowser = new AvaloniaCefBrowser();
        //cefBrowser.Address = "https://www.cnblogs.com";
        cefBrowser.Address = $"{AppDomain.CurrentDomain.BaseDirectory}TestWeb\\index.html";
        //this.Content = cefBrowser;
        panel.Children.Add(cefBrowser);
        cefBrowser.RegisterJavascriptObject(new JSCallback(), "AvaWebView");
    }

    public class JSCallback
    {
        // 方法名小写开头
        public void closeWindow()
        { 
        
        }
    }
}
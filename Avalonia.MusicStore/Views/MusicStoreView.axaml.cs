using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace Avalonia.MusicStore.Views;

public partial class MusicStoreView : UserControl
{
    public MusicStoreView()
    {
        InitializeComponent();
    }

    // 样式化属性、附加属性
    public static readonly StyledProperty<int> RepeatCountProperty =
        AvaloniaProperty.Register<MusicStoreView, int>(nameof(RepeatCount), defaultValue: 1);

    public int RepeatCount
    {
        get => GetValue(RepeatCountProperty);
        set => SetValue(RepeatCountProperty, value);
    }

    // 路由事件及引发
    public static readonly RoutedEvent<RoutedEventArgs> ValueChangedEvent =
        RoutedEvent.Register<MusicStoreView, RoutedEventArgs>(nameof(ValueChanged), RoutingStrategies.Bubble);

    public event EventHandler<RoutedEventArgs> ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    private void RaiseEvent()
    {
        RoutedEventArgs args = new RoutedEventArgs(ValueChangedEvent, this);
        RaiseEvent(args);
    }
}
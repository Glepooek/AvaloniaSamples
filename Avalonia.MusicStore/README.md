### Avalonia与WPF对比，没有的功能：
- 依赖项属性没有属性更改回调
- 没有触发器（属性触发器、数据触发器等）
- 没有Popup，类似控件Flyout

### Avalonia与WPF对比，特有的功能：
- 支持编译绑定
- Debug模式下可用F12启动DevTools，类似snoop
- 

### Avalonia与WPF对比，功能上不同：
- 样式资源放在顶级元素的Styles中
- 数据模版放在顶级元素的DataTemplates中；数据模版选择器实现IDataTemplate
- Style类似于CSS中的样式实现，ControlTheme类似于WPF中的Style
- 依赖项属性、附加属性定义用AvaloniaProperty.Register，类型StyledProperty
- 路由事件的定义用RoutedEvent.Register，类型RoutedEvent<RoutedEventArgs>
- Image的Source不能加载http\https\file://资源

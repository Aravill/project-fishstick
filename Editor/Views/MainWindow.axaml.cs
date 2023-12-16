using AvaloniaEditor.ViewModels;

namespace AvaloniaEditor.Views;
using Avalonia.ReactiveUI;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
  public MainWindow()
  {
    InitializeComponent();
  }
}
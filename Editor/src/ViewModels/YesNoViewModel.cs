using System.Reactive.Linq;
using System.Reactive;
using ReactiveUI;

namespace AvaloniaEditor.ViewModels;

public class YesNoViewModel : ReactiveObject
{
  private string _message = string.Empty;
  public ReactiveCommand<Unit, bool> YesCommand { get; }
  public ReactiveCommand<Unit, bool> NoCommand { get; }

  public Interaction<bool, Unit> CloseDialog { get; } = new Interaction<bool, Unit>();
  public YesNoViewModel()
  {
    YesCommand = ReactiveCommand.CreateFromTask(async () =>
    {
      await CloseDialog.Handle(true);
      return true;
    });
    NoCommand = ReactiveCommand.CreateFromTask(async () =>
    {
      await CloseDialog.Handle(false);
      return false;
    });
  }

  public string Message
  {
    get => _message;
    set => this.RaiseAndSetIfChanged(ref _message, value);
  }
}

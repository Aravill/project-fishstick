using System.Collections.Generic;
using System.Collections.ObjectModel;
using AvaloniaEditor.Models;

namespace AvaloniaEditor.ViewModels
{
  public class SceneViewModel : ViewModelBase
  {

    public SceneViewModel(IEnumerable<Scene> scenes)
    {
      Scenes = new ObservableCollection<Scene>(scenes);
    }

    public ObservableCollection<Scene> Scenes { get; }

  }
}
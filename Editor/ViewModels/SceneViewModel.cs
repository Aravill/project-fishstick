using System.Collections.Generic;
using System.Collections.ObjectModel;
using AvaloniaEditor.Models;

namespace AvaloniaEditor.ViewModels
{
  public class SceneViewModel : ViewModelBase
  {

    public SceneViewModel(IEnumerable<SceneModel> scenes)
    {
      Scenes = new ObservableCollection<SceneModel>(scenes);
    }

    public ObservableCollection<SceneModel> Scenes { get; }

  }
}
using System.Collections.Generic;
using AvaloniaEditor.Models;

namespace AvaloniaEditor.Services
{
  public class SceneService
  {
    public IEnumerable<Scene> GetItems() => new[]
    {
            new Scene { SceneDescription = "Room 1" },
            new Scene { SceneDescription = "Room 2" },
            new Scene { SceneDescription = "Room 3" },
        };
  }
}
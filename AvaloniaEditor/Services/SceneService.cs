using System.Collections.Generic;
using AvaloniaEditor.Models;

namespace AvaloniaEditor.Services
{
  public class SceneService
  {
    public IEnumerable<Scene> GetItems() => new[]
    {
            new Scene { Description = "Room 1" },
            new Scene { Description = "Room 2" },
            new Scene { Description = "Room 3" },
        };
  }
}
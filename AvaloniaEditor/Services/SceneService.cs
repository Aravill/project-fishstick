using System.Collections.Generic;
using AvaloniaEditor.Models;

namespace AvaloniaEditor.Services
{
  public class SceneService
  {
    public IEnumerable<Scene> GetItems() => new[]
    {
            new Scene { Id = "1", Description = "Room 1", Position = new Avalonia.Point(100, 100) },
            new Scene { Id = "2", Description = "Room 2", Position = new Avalonia.Point(200, 200) },
            new Scene { Id = "3", Description = "Room 3", Position = new Avalonia.Point(300, 300) },
        };
  }
}
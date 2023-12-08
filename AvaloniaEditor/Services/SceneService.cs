using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using AvaloniaEditor.Models;

namespace AvaloniaEditor.Services
{
  public class SceneService
  {

    private IEnumerable<Scene> _scenes;

    public SceneService()
    {
      _scenes = Array.Empty<Scene>();
      LoadScenes();
    }
    public IEnumerable<Scene> GetItems()
    {
      return _scenes;
    }
    public void SaveScenes()
    {
      MemoryStream stream = new MemoryStream();
      var ser = new DataContractJsonSerializer(typeof(IEnumerable<Scene>));
      ser.WriteObject(stream, _scenes);
      byte[] file = stream.ToArray();
      stream.Close();
      File.WriteAllBytes("Scenes.json", file);
    }

    private bool SceneFileExists()
    {
      return File.Exists("Scenes.json");
    }

    private void LoadScenes()
    {
      if (SceneFileExists())
      {
        IEnumerable<Scene>? deserializedList = Array.Empty<Scene>();
        MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText("Scenes.json")));
        var ser = new DataContractJsonSerializer(deserializedList.GetType());
        deserializedList = ser.ReadObject(stream) as IEnumerable<Scene>;
        stream.Close();
        if (deserializedList != null)
        {
          _scenes = deserializedList;
        }
      }
      else
      {
        _scenes = new[]
            {
            new Scene { Id = "1", Description = "Room 1", Position = new Avalonia.Point(100, 100) },
            new Scene { Id = "2", Description = "Room 2", Position = new Avalonia.Point(200, 200) },
            new Scene { Id = "3", Description = "Room 3", Position = new Avalonia.Point(300, 300) },
        };
      }
    }
  }


}
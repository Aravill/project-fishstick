using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using AvaloniaEditor.Models;
using FishStick.Scene;

namespace AvaloniaEditor.Services
{
  public class SceneService
  {

    private List<SceneModel> _scenes;

    private static readonly SceneService _instance = new();
    private SceneService()
    {
      _scenes = new();
      LoadScenes();
    }

    public static SceneService Instance
    {
      get
      {
        return _instance;
      }
    }

    public IEnumerable<SceneModel> GetItems()
    {
      return _scenes;
    }

    public void AddItem(SceneModel scene)
    {
      _scenes.Add(scene);
    }
    public void SaveScenes()
    {
      MemoryStream stream = new MemoryStream();
      var ser = new DataContractJsonSerializer(typeof(IEnumerable<SceneModel>));
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
        List<SceneModel>? deserializedList = new();
        MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText("Scenes.json")));
        var ser = new DataContractJsonSerializer(deserializedList.GetType());
        deserializedList = ser.ReadObject(stream) as List<SceneModel>;
        stream.Close();
        if (deserializedList != null)
        {
          _scenes = deserializedList;
        }
      }
      else
      {
        string guid1 = System.Guid.NewGuid().ToString();
        string guid2 = System.Guid.NewGuid().ToString();
        string guid3 = System.Guid.NewGuid().ToString();
        _scenes = new()
            {
            new SceneModel { Id = guid1, Name = "Starting Room", Description = "A cold room.", Position = new Avalonia.Point(100, 100), Transitions = new List<ITransition> { new BaseTransition("Transition 1", "Something", guid2)  }},
            new SceneModel { Id = guid2, Name = "Warm Room", Description = "A warm room.", Position = new Avalonia.Point(200, 200) },
            new SceneModel { Id = guid3, Name = "Long Room", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Position = new Avalonia.Point(300, 300) },
        };
      }
    }
  }


}
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using AvaloniaEditor.Models;

namespace AvaloniaEditor.Services
{
  public class SceneService
  {

    private List<SceneModel> _scenes;

    public SceneService()
    {
      _scenes = new();
      LoadScenes();
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
        _scenes = new()
            {
            new SceneModel { Id = "1", Description = "Room 1", Position = new Avalonia.Point(100, 100) },
            new SceneModel { Id = "2", Description = "Room 2", Position = new Avalonia.Point(200, 200) },
            new SceneModel { Id = "3", Description = "Room 3", Position = new Avalonia.Point(300, 300) },
        };
      }
    }
  }


}
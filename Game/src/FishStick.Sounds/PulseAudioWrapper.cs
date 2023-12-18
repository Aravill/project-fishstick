
using SDL2;
namespace FishStick.Sounds
{
  public class SDL2Wrapper : ISoundPlayerWrapper
  {
    private string _soundLocation = string.Empty;

    public string SoundLocation
    {
      set
      {
        _soundLocation = value;
      }
    }

    public void Play(bool once)
    {

    }

    public void Load()
    {

    }

    public void Stop()
    {
    }

    public SDL2Wrapper()
    {
      SDL.SDL_Init(SDL.SDL_INIT_AUDIO);
    }
  }
}
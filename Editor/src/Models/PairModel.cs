namespace AvaloniaEditor.Models
{
  public class PairModel<T, U>
  {

    public PairModel(T first, U second)
    {
      First = first;
      Second = second;
    }

    public T First { get; set; }
    public U Second { get; set; }
  }
}
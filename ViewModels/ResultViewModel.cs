namespace Blog.ViewModels
{
  public class ResultViewModel<T>
  {
    public List<T> Data { get; set; } = new List<T>();
    public List<string> Errors { get; set; } = new List<string>();

    public ResultViewModel(T data)
    {
      Data.Add(data);
    }
    public ResultViewModel(List<T> dataList)
    {
      Data.AddRange(dataList);
    }
    public ResultViewModel(string error)
    {
      Errors.Add(error);
    }
    public ResultViewModel(List<string> errorList)
    {
      Errors.AddRange(errorList);
    }
  }
}

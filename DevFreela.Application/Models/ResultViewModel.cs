namespace DevFreela.Application.Models;

public class ResultViewModel
{
    public ResultViewModel(bool isSuccessful = true, string message = "")
    {
        IsSuccessful = isSuccessful;
        Message = message;
    }
    public bool IsSuccessful { get; private set; }
    public string Message { get; private set; }
}

public class ResultViewModel<T> : ResultViewModel
{
    public ResultViewModel(T? data,bool isSuccessful = true, string message = ""): base(isSuccessful, message)
    {
        Data = data;
    }
    public T? Data { get; set; }
}
using MediatR;

namespace DevFreela.Application.Models;

public class ResultViewModel : IRequest
{
    public ResultViewModel(bool isSuccessful = true, string message = "")
    {
        IsSuccessful = isSuccessful;
        Message = message;
    }
    public bool IsSuccessful { get; private set; }
    public string Message { get; private set; }
    
    public static ResultViewModel Success()
        => new();
    
    public static ResultViewModel Failed(string message)
        => new ( false, message);
}

public class ResultViewModel<T> : ResultViewModel
{
    public ResultViewModel(T? data,bool isSuccessful = true, string message = ""): base(isSuccessful, message)
    {
        Data = data;
    }
    public T? Data { get; set; }

    public static ResultViewModel<T> Success(T data)
        => new(data);
    
    public static ResultViewModel<T> Failed(string message)
        => new (default, false, message);
}
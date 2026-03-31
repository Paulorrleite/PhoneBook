namespace PhoneBook.Application.Commom;

public class ResultT<T> : Result
{
    public T Value { get; }

    protected ResultT(T value, bool isSuccess, string error) : base(isSuccess, error)
    {
        Value = value;   
    }

    public static ResultT<T> Success(T value)
    {
        return new ResultT<T>(value, true, string.Empty);
    }

    public static new ResultT<T> Failure(string error)
    {
        return new ResultT<T>(default!, false, error);
    }
}

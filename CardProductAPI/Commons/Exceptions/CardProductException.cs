namespace CardProductAPI.Commons.Exceptions;

public class CardProductException : Exception
{
    public int Code { get; }

    public CardProductException() { }

    public CardProductException(string message)
        : base(message) { }

    public CardProductException(string message, Exception inner)
        : base(message, inner) { }

    public CardProductException(string message, int code)
        : this(message)
    {
        Code = code;
    }
}
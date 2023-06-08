namespace CardProductAPI.Commons.Exceptions;

public class NotFoundException : Exception
{
    public int Code { get; }
    public NotFoundException(string message)
        : base(message) { }
}
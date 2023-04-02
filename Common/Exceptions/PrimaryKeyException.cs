namespace Common.Exceptions;

public class PrimaryKeyException : Exception
{
    public PrimaryKeyException()
    {
    }

    public PrimaryKeyException(string message) : base(message)
    {
    }
}
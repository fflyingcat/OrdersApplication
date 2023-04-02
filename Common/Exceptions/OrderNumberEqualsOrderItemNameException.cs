namespace Common.Exceptions;

public class OrderNumberEqualsOrderItemNameException : Exception
{
    public OrderNumberEqualsOrderItemNameException()
    {
    }

    public OrderNumberEqualsOrderItemNameException(string message) : base(message)
    {
    }
}
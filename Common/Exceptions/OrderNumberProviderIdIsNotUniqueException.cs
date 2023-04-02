namespace Common.Exceptions
{
    public class OrderNumberProviderIdIsNotUniqueException : Exception
    {
        public OrderNumberProviderIdIsNotUniqueException()
        {
        }

        public OrderNumberProviderIdIsNotUniqueException(string message) : base(message)
        {
        }
    }
}

namespace Common.Exceptions
{
    public class ForeignKeyException : Exception
    {
        public ForeignKeyException()
        {
        }

        public ForeignKeyException(string message) : base(message)
        {
        }
    }
}
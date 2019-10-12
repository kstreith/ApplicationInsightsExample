namespace CustomerApi.Business.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public NotFoundException()
        {
        }
    }
}

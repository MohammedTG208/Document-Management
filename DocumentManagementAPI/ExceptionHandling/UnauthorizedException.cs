namespace DocumentManagementAPI.ExceptionHandling
{
    public class UnauthorizedException:Exception
    {
        public UnauthorizedException(string message):base(message) { }
    }
}

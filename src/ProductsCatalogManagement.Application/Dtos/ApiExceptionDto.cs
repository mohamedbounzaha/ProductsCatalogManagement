namespace ProductsCatalogManagement.Application.Dtos
{
    public class ApiExceptionDto
    {
        public int HttpStatusCode { get; set; }
        public ExceptionDto Exception { get; set; }
    }

    public class ExceptionDto
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
    }
}
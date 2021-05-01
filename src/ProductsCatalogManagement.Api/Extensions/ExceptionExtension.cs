using ProductsCatalogManagement.Application.Dtos;
using System;

namespace ProductsCatalogManagement.Api.Extensions
{
    public static class ExceptionExtension
    {
        public static ApiExceptionDto CreateApiErrorException(this Exception exception, int httpStatusCode)
        {
            var exceptionDto =
                new ExceptionDto
                {
                    Message = exception.Message,
                    Source = exception.Source,
                    StackTrace = exception.StackTrace
                };
            var apiErrorException =
                new ApiExceptionDto
                {
                    HttpStatusCode = httpStatusCode,
                    Exception = exceptionDto
                };
            return apiErrorException;
        }

        public static ApiExceptionDto CreateApiErrorException(int httpStatusCode, string message)
        {
            var exceptionDto = new ExceptionDto { Message = message };
            var apiErrorException =
                new ApiExceptionDto
                {
                    HttpStatusCode = httpStatusCode,
                    Exception = exceptionDto
                };
            return apiErrorException;
        }
    }
}
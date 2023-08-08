using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Fuel9.ExceptionFilter
{
    public class BusinessExceptionFilter:ExceptionFilterAttribute
    {
        private readonly Type _exceptionType;
        private readonly HttpStatusCode _httpStatusCode;

        public BusinessExceptionFilter(Type exceptionType, HttpStatusCode httpStatusCode)
        {
            _exceptionType = exceptionType;
            _httpStatusCode = httpStatusCode;
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception.GetType() == _exceptionType)
            {
                context.Result = new ObjectResult(exception.Message)
                {
                    StatusCode = (int)_httpStatusCode
                };

                return;
            }

            base.OnException(context);
        }
    }
}

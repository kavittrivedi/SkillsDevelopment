using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;
using EmployeeSkillsDevelopment.Api.DTOs;

namespace EmployeeSkillsDevelopment.Api.Filters
{

    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {

            Log.Error(context.Exception, "An unhandled exception occurred.");
            var errorResponse = new ErrorResponseDto
            {
                Message = "An unexpected error occurred on internal server."
            };

            // Set the response to use the custom error response format
            context.Result = new JsonResult(errorResponse)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            context.ExceptionHandled = true;


        }
    }

}

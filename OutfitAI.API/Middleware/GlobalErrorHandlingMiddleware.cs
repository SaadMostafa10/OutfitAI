using Domain.Exceptions;
using Domain.Exceptions.BadRequest.Outfit;
using Domain.Exceptions.NotFound;
using Shared.Dtos.AuthDtos;

namespace OutfitAI.API.Middleware
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

                // TO DO 
                // NotFound EndPoint and refactoring 

               

            }
            catch (Exception ex)
            {
                // Log Exception
                _logger.LogError(ex, ex.Message);

                // 1. Set Status Code For Response
                // 2. Set Content Type For Response
                // 3. Response Object [Body]
                // 4. Return Response

                context.Response.ContentType = "application/json";
                var response = new ErrorDetails()
                {
                    ErrorMessage = ex.Message
                };
                response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    UnAuthorizedException => StatusCodes.Status401Unauthorized,
                    ValidationException => HandleValidationExceptionAsync((ValidationException)ex, response),

                    EmailAlreadyExistsException => StatusCodes.Status400BadRequest,
                    MustAgreeToTermsException => StatusCodes.Status400BadRequest,

                    OutfitImagesRequiredException => StatusCodes.Status400BadRequest,
                    InvalidImageFileException => StatusCodes.Status400BadRequest,
                   
                     
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = response.StatusCode;

                await context.Response.WriteAsJsonAsync(response);
            }
        }
        private static int HandleValidationExceptionAsync(ValidationException ex, ErrorDetails response)
        {
            response.Errors = ex.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}

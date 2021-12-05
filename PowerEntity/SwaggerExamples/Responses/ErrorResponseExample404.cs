using PowerEntity.Model;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger.Demo.SwaggerExamples.Responses
{
    public class ErrorResponseExample404 : IExamplesProvider<ErrorResponse404>
    {
        public ErrorResponse404 GetExamples()
        {
            return new ErrorResponse404 { errorCode = "10001", errorMessage = "IdEntity inválido." };
        }
    }
}

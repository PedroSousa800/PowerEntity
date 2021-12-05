using PowerEntity.Model;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger.Demo.SwaggerExamples.Responses
{
    public class ErrorResponseExample400 : IExamplesProvider<ErrorResponse400>
    {
        public ErrorResponse400 GetExamples()
        {
            return new ErrorResponse400 { errorCode = "10002", errorMessage = "Código do país inválido." };
        }
    }
}

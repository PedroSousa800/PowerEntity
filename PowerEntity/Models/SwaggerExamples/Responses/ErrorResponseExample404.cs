using PowerEntity.Models.SwaggerExamples.ErrorModels;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.Models.SwaggerExamples.Responses
{
    public class ErrorResponseExample404 : IExamplesProvider<ErrorResponse404>
    {
        public ErrorResponse404 GetExamples()
        {
            return new ErrorResponse404 { ErrorCode = "10001", ErrorMessage = "IdEntity inválido." };
        }
    }
}

﻿using PowerEntity.Models.SwaggerExamples.ErrorModels;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.Models.SwaggerExamples.Responses
{
    public class ErrorResponseExample400 : IExamplesProvider<ErrorResponse400>
    {
        public ErrorResponse400 GetExamples()
        {
            return new ErrorResponse400 { ErrorCode = "10002", ErrorMessage = "Código do país inválido." };
        }
    }
}

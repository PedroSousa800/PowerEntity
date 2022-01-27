using System;

namespace PowerEntity.Models.SwaggerExamples.ErrorModels
{
    public class ErrorResponse404
    {
        public String ErrorCode { get; set; }
        public String ErrorMessage { get; set; }
        public ErrorResponse404(String errorCode, String errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;

        }
        public ErrorResponse404()
        {

        }
    }
}

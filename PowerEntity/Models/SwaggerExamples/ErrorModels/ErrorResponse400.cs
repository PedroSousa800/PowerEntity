using System;

namespace PowerEntity.Models.SwaggerExamples.ErrorModels
{
    public class ErrorResponse400
    {
        public String ErrorCode { get; set; }
        public String ErrorMessage { get; set; }
        public ErrorResponse400(String errorCode, String errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;

        }
        public ErrorResponse400()
        {

        }
    }
}

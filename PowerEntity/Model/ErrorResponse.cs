using System;

namespace PowerEntity.Model
{
    public class ErrorResponse
    {
        public String errorCode { get; set; }
        public String errorMessage { get; set; }
        public ErrorResponse(String errorCode, String errorMessage)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;

        }
        public ErrorResponse()
        {

        }
    }
}

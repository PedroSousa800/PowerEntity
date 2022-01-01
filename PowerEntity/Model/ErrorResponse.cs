using System;

namespace PowerEntity.Model
{
    public class ErrorResponse
    {
        public int errorCode { get; set; }
        public String errorMessage { get; set; }
        public ErrorResponse(int errorCode, String errorMessage)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;

        }
        public ErrorResponse()
        {

        }
    }
}

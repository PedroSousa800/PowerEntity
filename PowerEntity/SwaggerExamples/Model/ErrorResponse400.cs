using System;

namespace PowerEntity.Model
{
    public class ErrorResponse400
    {
        public String errorCode { get; set; }
        public String errorMessage { get; set; }
        public ErrorResponse400(String errorCode, String errorMessage)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;

        }
        public ErrorResponse400()
        {

        }
    }
}

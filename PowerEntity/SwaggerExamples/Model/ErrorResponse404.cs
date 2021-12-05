using System;

namespace PowerEntity.Model
{
    public class ErrorResponse404
    {
        public String errorCode { get; set; }
        public String errorMessage { get; set; }
        public ErrorResponse404(String errorCode, String errorMessage)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;

        }
        public ErrorResponse404()
        {

        }
    }
}

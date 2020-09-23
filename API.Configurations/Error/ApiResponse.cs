using System;

namespace API.Configurations.Error {
    public class ApiResponse {
        public ApiResponse (int statusCode, string message = null) {
            this.StatusCode = statusCode;
            this.Message = message ?? GetDefaultMessageForStatusCode (statusCode);
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessageForStatusCode (int statusCode) {
            return statusCode
            switch {
                400 => "A bad request, you have made",
                    401 => "Authorized, you are not",
                    404 => "Resource found, it was not",
                    500 => "Errors are the path to the dard side. Errors lead to anger. Anger leads to hate.Hate leads to carrer changer",
                    _ => null
            };
        }
    }
}
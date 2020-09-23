using System.Collections.Generic;

namespace API.Configurations.Error {
    public class ApiValidationErrorResponse : ApiResponse {
        public ApiValidationErrorResponse () : base (400) { }
        public IEnumerable<string> Errors { get; set; }
    }
}
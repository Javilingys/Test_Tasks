namespace SmartWash.API.DTOs
{
    public class ApiResponse<T>
    {
        private ApiResponse()
        {
            
        }

        public T Data { get; private set; }
        public bool IsSuccess { get; private set; }
        public bool IsNotFound { get; set; }
        public string FailureMessage { get; private set; }

        public static ApiResponse<T> CreateSuccess(T data)
        {
            return new ApiResponse<T>()
            {
                IsSuccess = true,
                Data = data
            };
        }

        public static ApiResponse<T> CreateFailure(string failureMessage)
        {
            return new ApiResponse<T>()
            {
                IsSuccess = false,
                FailureMessage = failureMessage
            };
        }

        public static ApiResponse<T> CreateFailureNotFound(string notFoundMessage)
        {
            return new ApiResponse<T>()
            {
                IsSuccess = false,
                IsNotFound = true,
                FailureMessage = notFoundMessage
            };
        }
    }
}

namespace SecretsShare.HelperObject
{
    /// <summary>
    /// a class for sending a response about a successful request
    /// </summary>
    public class SuccessResponse
    {
        /// <summary>
        /// a field indicating the success of the request
        /// </summary>
        public bool Success;

        /// <summary>
        /// initializes the successful response object
        /// </summary>
        /// <param name="success">was the data received correctly</param>
        public SuccessResponse(bool success)
        {
            Success = success;
        }
    }
}
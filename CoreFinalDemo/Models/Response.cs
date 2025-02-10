namespace CoreFinalDemo.Models
{
    /// <summary>
    /// Represents the response returned by the API.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// data returned in the response.
        /// </summary>
        public dynamic? Data { get; set; }

        /// <summary>
        /// value indicating whether the response is an error.
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// message associated with the response.
        /// </summary>
        public string? Message { get; set; }
    }
}

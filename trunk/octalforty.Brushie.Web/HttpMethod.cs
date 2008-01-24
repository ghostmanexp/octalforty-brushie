namespace octalforty.Brushie.Web
{
    /// <summary>
    /// Defines a list of all possible HTTP Methods.
    /// </summary>
    public enum HttpMethod
    {
        /// <summary>
        /// Requests a representation of the specified resource.
        /// </summary>
        /// <remarks>
        /// By far the most common method used on the Web today. Should not be used for operations that cause 
        /// side-effects (using it for actions in web applications is a common misuse).
        /// </remarks>
        Get = 1,

        /// <summary>
        /// Submits data to be processed (e.g. from an HTML form) to the identified resource. 
        /// </summary>
        /// <remarks>
        /// The data is included in the body of the request. This may result in the creation of a new resource 
        /// or the updates of existing resources or both.
        /// </remarks>
        Post = 2,

        /// <summary>
        /// Uploads a representation of the specified resource.
        /// </summary>
        Put = 3,

        /// <summary>
        /// Deletes the specified resource.
        /// </summary>
        Delete = 4
    }
}

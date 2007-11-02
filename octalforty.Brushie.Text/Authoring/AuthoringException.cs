using System;

namespace octalforty.Brushie.Text.Authoring
{
    /// <summary>
    /// Raised when an error occurs during authoring process.
    /// </summary>
    public class AuthoringException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AuthoringException"/> class.
        /// </summary>
        public AuthoringException()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AuthoringException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public AuthoringException(string message) : 
            base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AuthoringException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AuthoringException(string message, Exception innerException) : 
            base(message, innerException)
        {
        }
    }
}

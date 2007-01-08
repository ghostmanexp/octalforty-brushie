using System;

namespace octalforty.Brushie.Instrumentation.Core.Binders
{
    /// <summary>
    /// Binds messages according to their type.
    /// </summary>
    public class MessageTypeBinder : BinderBase
    {
        #region Private Member Variables
        private string[] messageTypeNames;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a reference to the array of strings with names of types of messages.
        /// </summary>
        public string[] MessageTypeNames
        {
            get { return messageTypeNames; }
            set { messageTypeNames = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="MessageTypeBinder"/> class
        /// with a given array of message type names.
        /// </summary>
        /// <param name="messageTypeNames"></param>
        public MessageTypeBinder(string[] messageTypeNames)
        {
            this.messageTypeNames = messageTypeNames;
        }

        #region BinderBase Members
        /// <summary>
        /// Internal method, invoked from implementation of <see cref="BinderBase.CanBind"/>.<para />
        /// Determines whether the <paramref name="message"/> can be bound to the
        /// persister in question or not.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected override bool InternalCanBind(IMessage message)
        {
            //
            // Move on to the next binder on no message types.
            if(MessageTypeNames == null || MessageTypeNames.GetLength(0) == 0)
                return true;
            
            //
            // Check each type in turn.
            Type targetType = message.GetType();
            foreach(string messageTypeName in MessageTypeNames)
            {
                //
                // Checking for an asterisk
                if(messageTypeName == "*")
                    return true;
                
                if(Type.GetType(messageTypeName) == targetType)
                    return true;
            } // foreach
            
            return false;
        }
        #endregion
    }
}

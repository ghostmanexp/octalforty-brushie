using System;

namespace octalforty.Brushie.Instrumentation.Core.Binders
{
    /// <summary>
    /// Binds messages according to their type.
    /// </summary>
    public class MessageTypeBinder : BinderBase
    {
        #region Private Member Variables
        private Type[] messageTypes;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a reference to the array of <see cref="Type"/> objects
        /// with types of messages.
        /// </summary>
        public Type[] MessageTypes
        {
            get { return messageTypes; }
            set { messageTypes = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="MessageTypeBinder"/> class
        /// with a given array of message types.
        /// </summary>
        /// <param name="messageTypes"></param>
        public MessageTypeBinder(Type[] messageTypes)
        {
            this.messageTypes = messageTypes;
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
            if(MessageTypes == null || MessageTypes.GetLength(0) == 0)
                return true;
            
            //
            // Check each type in turn.
            Type targetType = message.GetType();
            foreach(Type messageType in MessageTypes)
            {
                if(messageType == targetType)
                    return true;
            } // foreach
            
            return false;
        }
        #endregion
    }
}

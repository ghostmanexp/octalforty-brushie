using System;

namespace octalforty.Brushie.Web
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class QueryStringFieldAttribute : Attribute
    {
        #region Private Member Variables
        private string name;
        private string dateTimeFormatString;
        private Type elementType;
        #endregion

        #region Public Properties
        public string Name
        {
            get { return name; }
        }

        public string DateTimeFormatString
        {
            get { return dateTimeFormatString; }
            set { dateTimeFormatString = value; }
        }

        public Type ElementType
        {
            get { return elementType; }
            set { elementType = value; }
        }
        #endregion

        public QueryStringFieldAttribute(string name)
        {
            this.name = name;
        }
    }
}

using System;

namespace octalforty.Brushie.Web
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class QueryStringFieldAttribute : Attribute
    {
        #region Private Member Variables
        private string name;
        private string dateTimeFormatString;
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
        #endregion

        public QueryStringFieldAttribute(string name)
        {
            this.name = name;
        }
    }
}

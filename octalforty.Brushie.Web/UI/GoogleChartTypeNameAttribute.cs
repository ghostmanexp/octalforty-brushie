using System;

namespace octalforty.Brushie.Web.UI
{
    /// <summary>
    /// Used to specify the name of the Google Chart Type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    internal sealed class GoogleChartTypeNameAttribute : Attribute
    {
        #region Private Member Variables
        private string name;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a string which contains the name of the chart type.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="GoogleChartTypeNameAttribute"/> class.
        /// </summary>
        /// <param name="name"></param>
        public GoogleChartTypeNameAttribute(string name)
        {
            this.name = name;
        }
    }
}

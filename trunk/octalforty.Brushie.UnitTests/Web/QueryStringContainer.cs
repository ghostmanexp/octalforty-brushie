using System;

using octalforty.Brushie.Web;

namespace octalforty.Brushie.UnitTests.Web
{
    public class QueryStringContainer
    {
        #region Private Member Variables
        private string stringField;
        private int intField;
        private int[] intArrayField;
        private DateTime? dateField;
        private DateTime? dateTimeField;
        #endregion

        #region Public Properties
        [QueryStringField("s")]
        public string StringField
        {
            get { return stringField; }
            set { stringField = value; }
        }

        [QueryStringField("intField")]
        public int IntField
        {
            get { return intField; }
            set { intField = value; }
        }

        [QueryStringField("iaf")]
        public int[] IntArrayField
        {
            get { return intArrayField; }
            set { intArrayField = value; }
        }

        [QueryStringField("df", DateTimeFormatString = "yyyyMMdd")]
        public DateTime? DateField
        {
            get { return dateField; }
            set { dateField = value; }
        }

        [QueryStringField("dtf", DateTimeFormatString = "yyyyMMddHHmmss")]
        public DateTime? DateTimeField
        {
            get { return dateTimeField; }
            set { dateTimeField = value; }
        }
        #endregion

        public QueryStringContainer()
        {
        }

        public QueryStringContainer(string stringField, int intField, int[] intArrayField)
        {
            this.stringField = stringField;
            this.intField = intField;
            this.intArrayField = intArrayField;
        }
    }
}

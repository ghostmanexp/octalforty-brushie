using System;
using System.Collections.Generic;

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
        private List<string> strings = new List<string>();
        private List<int> ints = new List<int>();
        private bool boolField;
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

        [QueryStringField("sts", ElementType = typeof(string))]
        public List<string> Strings
        {
            get { return strings; }
            set { strings = value; }
        }

        [QueryStringField("integers", ElementType = typeof(int))]
        public List<int> Ints
        {
            get { return ints; }
            set { ints = value; }
        }

        [QueryStringField("b")]
        public bool BoolField
        {
            get { return boolField; }
            set { boolField = value; }
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

﻿using octalforty.Brushie.Web.XmlRpc;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc
{
    [XmlRpcStructure()]
    public class Range
    {
        private int lowerBound;
        private string upperBound;
        private string ignored;

        [XmlRpcMember("lower-bound")]
        public int LowerBound
        {
            get { return lowerBound; }
            set { lowerBound = value; }
        }

        [XmlRpcMember()]
        public string UpperBound
        {
            get { return upperBound; }
            set { upperBound = value; }
        }

        public string Ignored
        {
            get { return ignored; }
            set { ignored = value; }
        }

        public Range(int lowerBound, string upperBound)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        public Range()
        {
        }
    }
}

using octalforty.Brushie.Web.XmlRpc;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc
{
    [XmlRpcStructure()]
    public class DoubleRange
    {
        private Range from = new Range();
        private Range to = new Range();

        [XmlRpcMember()]
        public Range From
        {
            get { return from; }
            set { from = value; }
        }

        [XmlRpcMember()]
        public Range To
        {
            get { return to; }
            set { to = value; }
        }

        public DoubleRange(Range from, Range to)
        {
            this.from = from;
            this.to = to;
        }

        public DoubleRange()
        {
        }
    }
}

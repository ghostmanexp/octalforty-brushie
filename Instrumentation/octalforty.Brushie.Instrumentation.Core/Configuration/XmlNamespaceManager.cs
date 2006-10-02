using System.Xml;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// octalforty Brushie Instrumentation framework XML namespace manager.
    /// </summary>
    public class XmlNamespaceManager : System.Xml.XmlNamespaceManager
    {
        /// <summary>
        /// Initializes a new instance of <see cref="XmlNamespaceManager"/> class.
        /// </summary>
        public XmlNamespaceManager() : 
            base(new NameTable())
        {
            AddNamespace("instrumentation", 
                "http://schemas.octalfortystudios.com/brushie/1.0/instrumentation");
        }
    }
}

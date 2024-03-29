using System.Collections.Specialized;

using octalforty.Brushie.Instrumentation.Core.Exceptions;
using octalforty.Brushie.Instrumentation.Core.Resources;

namespace octalforty.Brushie.Instrumentation.Core.Persisters
{
    /// <summary>
    /// Persister which requires <c>formatString</c> property in order to
    /// format the output string.
    /// </summary>
    /// <remarks>
    /// Properties required by this persister:
    /// <list type="table">
    ///     <listheader><term>Property</term><description>Description</description></listheader>
    ///     <item>
    ///         <term>formatString</term>
    ///         <description>
    ///             Format string used to format the output string. For example:
    ///             <code>{Time:yyyy-MM-dd hh:mm:ss.fff} - {Severity:-20} - {Source:-30} - {Message}</code>
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public abstract class FormattingPersister : PersisterBase
    {
        #region Private Constants
        private readonly string FormatStringPropertyName = "formatString";
        #endregion

        #region Private Member Variables
        private string formatString = string.Empty;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the format string used by this persister.
        /// </summary>
        public string FormatString
        {
            get { return formatString; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="FormattingPersister"/> class.
        /// </summary>
        protected FormattingPersister()
        {
        }

        #region PersisterBase Members
        /// <summary>
        /// Configures persister with information from <paramref name="properties"/> dictionary.
        /// </summary>
        /// <param name="properties">Properties of the persister.</param>
        public override void Configure(StringDictionary properties)
        {
            base.Configure(properties);

            if(!properties.ContainsKey(FormatStringPropertyName))
                throw new InstrumentationException(
                    string.Format(Strings.FormattingPersister_Configure_RequiredPropertyMissing,
                        FormatStringPropertyName));

            formatString = properties[FormatStringPropertyName];
        }
        #endregion
    }
}
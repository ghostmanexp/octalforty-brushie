namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Provides access to the configuration information of octalforty Brushie Instrumentation
    /// framework.
    /// </summary>
    public static class ConfigurationManager
    {
        #region Private Constants
        private const string ConfigurationSectionName = "octalforty.brushie.instrumentation";
        private const string ConsolePersisterName = "consolePersister";
        private const string FormatStringPropertyName = "formatString";
        private const string FormatStringPropertyValue = 
            "{Time:yyyy-MM-dd hh:mm:ss.fff} - {Severity:-20} - {Source:-30} - {Message}";
        #endregion

        #region Public Static Properties
        /// <summary>
        /// Gets a reference to the <see cref="InstrumentationSection"/>.
        /// </summary>
        public static InstrumentationSection ConfigurationSection
        {
            get
            {
                return 
                    System.Configuration.ConfigurationManager.GetSection(
                        ConfigurationSectionName)
                    as InstrumentationSection;
            }
        }
        #endregion
    }
}

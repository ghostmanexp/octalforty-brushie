using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

using octalforty.Brushie.Instrumentation.Core;

namespace octalforty.Brushie.Instrumentation.Core.Util
{
    /// <summary>
    /// Formats <see cref="IMessage"/> instances.
    /// </summary>
    public static class MessageFormatter
    {
        public static string FormatMessage(IMessage message, string formatString)
        {
            string result = formatString;
            Regex regex = new Regex(@"(?<Group>\{(?<PropertyName>\w*)(:(?<FormatString>.*?))?\})",
                RegexOptions.Compiled | RegexOptions.Singleline);
            
            Match match = regex.Match(result);

            Dictionary<string, PropertyInfo> properties = GetObjectProperties(message);

            while(match.Success)
            {
                object propertyValue =
                    properties[match.Groups["PropertyName"].Value].GetValue(message, null);
                string valueFormatString = match.Groups["FormatString"].Value;

                result = result.Replace(match.Groups["Group"].Value,
                    FormattingManager.Format(propertyValue, valueFormatString));

                match = match.NextMatch();
            } // while

            return result;
        }

        private static Dictionary<string, PropertyInfo> GetObjectProperties(IMessage message)
        {
            PropertyInfo[] propertyInfos = message.GetType().GetProperties();
            Dictionary<string, PropertyInfo> properties = new Dictionary<string, PropertyInfo>();

            foreach(PropertyInfo propertyInfo in propertyInfos)
                properties.Add(propertyInfo.Name, propertyInfo);

            return properties;
        }
    }
}

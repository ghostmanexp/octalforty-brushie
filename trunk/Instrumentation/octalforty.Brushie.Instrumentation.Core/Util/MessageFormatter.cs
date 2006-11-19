using System;
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
        #region Private Static Member Variables
        private static Dictionary<Type, Dictionary<string, PropertyInfo>> messagePropertiesCache =
            new Dictionary<Type, Dictionary<string, PropertyInfo>>();
        private static object syncRoot = new object();
        #endregion

        /// <summary>
        /// Formats <paramref name="message"/> according to <paramref name="formatString"/>.
        /// </summary>
        /// <param name="message">Message to be formatted.</param>
        /// <param name="formatString">Format string used when formatting message.</param>
        /// <returns></returns>
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
            lock(syncRoot)
            {
                Type messageType = message.GetType();

                if(!messagePropertiesCache.ContainsKey(messageType))
                {
                    PropertyInfo[] propertyInfos = messageType.GetProperties();

                    Dictionary<string, PropertyInfo> properties = 
                        new Dictionary<string, PropertyInfo>();

                    foreach(PropertyInfo propertyInfo in propertyInfos)
                        properties.Add(propertyInfo.Name, propertyInfo);
                    
                    messagePropertiesCache.Add(messageType, properties);
                } // if
                
                return messagePropertiesCache[messageType];
            } // lock
        }
    }
}
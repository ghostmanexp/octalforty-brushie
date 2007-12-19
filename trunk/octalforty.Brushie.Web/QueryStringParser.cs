using System;
using System.Collections;
#if FW2
using System.Collections.Generic;
#endif
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;

namespace octalforty.Brushie.Web
{
    /// <summary>
    /// Parses a <see cref="NameValueCollection"/> into a container object.
    /// </summary>
    public class QueryStringParser
    {
        /// <summary>
        /// Parses <paramref name="queryStringFields"/> into <paramref name="container"/>.
        /// </summary>
        /// <param name="queryStringFields"></param>
        /// <param name="container"></param>
        public void Parse(NameValueCollection queryStringFields, object container)
        {
#if FW2
            IDictionary<string, PropertyInfo> propertiesCache = new Dictionary<string, PropertyInfo>();
#else
			Hashtable propertiesCache = new Hashtable();
#endif
            PropertyInfo[] properties = 
                container.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach(PropertyInfo property in properties)
            {
                if(Attribute.IsDefined(property, typeof(QueryStringFieldAttribute)))
                {
                    QueryStringFieldAttribute queryStringField =
                        (QueryStringFieldAttribute)Attribute.GetCustomAttribute(property, typeof(QueryStringFieldAttribute));
                    propertiesCache.Add(queryStringField.Name, property);
                } // if
            } // foreach

            ParseQueryStringFields(queryStringFields, container, propertiesCache);
        }

        protected virtual void ParseQueryStringFields(NameValueCollection queryStringFields, object container, 
#if FW2
			IDictionary<string, PropertyInfo> propertiesCache
#else
			Hashtable propertiesCache
#endif
            
												  )
        {
            foreach(string fieldName in queryStringFields.AllKeys)
            {
                PropertyInfo property = (PropertyInfo)propertiesCache[fieldName];
                ParseProperty(container, property, fieldName, queryStringFields);
            } // foreach
        }

        protected virtual void ParseProperty(object container, PropertyInfo property, string fieldName,
            NameValueCollection queryStringFields)
        {
            if(property.PropertyType.IsArray)
            {
                ParseArrayProperty(container, property, fieldName, queryStringFields);
            } // if
            else
            {
                Type propertyType = property.PropertyType;
#if FW2
                if(Nullable.GetUnderlyingType(property.PropertyType) != null)
                    propertyType = Nullable.GetUnderlyingType(property.PropertyType);
#endif
                ParseTypedProperty(container, property, propertyType, fieldName, queryStringFields);
            } // else
        }

        protected virtual void ParseTypedProperty(object container, PropertyInfo property, Type propertyType,
            string fieldName, NameValueCollection queryStringFields)
        {
            QueryStringFieldAttribute queryStringField =
                (QueryStringFieldAttribute)Attribute.GetCustomAttribute(property, typeof(QueryStringFieldAttribute));

            if(propertyType.IsAssignableFrom(typeof(DateTime)))
            {
                property.SetValue(container,
                    DateTime.ParseExact(queryStringFields[fieldName], queryStringField.DateTimeFormatString,
                        CultureInfo.InvariantCulture), null);
            } // if
            else if(typeof(IList).IsAssignableFrom(propertyType))
            {
                IList list = (IList)property.GetValue(container, null);
                
                string[] values = queryStringFields[fieldName].Split(',');
                foreach(string value in values)
                    list.Add(Convert.ChangeType(value, queryStringField.ElementType));
            } // else if
            else if(typeof(bool) == propertyType)
            {
                property.SetValue(container,
                    queryStringFields[fieldName] == "1" ? true : false, null);
            } // else if
            else
                property.SetValue(container,
                    Convert.ChangeType(queryStringFields[fieldName], propertyType), null);
        }

        protected virtual void ParseArrayProperty(object container, PropertyInfo property, string fieldName, 
            NameValueCollection queryStringFields)
        {
            string[] values = queryStringFields[fieldName].Split(',');
            ArrayList array = new ArrayList();

            foreach(string value in values)
                array.Add(Convert.ChangeType(value, property.PropertyType.GetElementType()));

            property.SetValue(container, array.ToArray(property.PropertyType.GetElementType()), null);
        }
    }
}

using System;
using System.Collections;
#if FW2
using System.Collections.Generic;
#endif
using System.Reflection;

namespace octalforty.Brushie.Web
{
    /// <summary>
    /// Compiles a query string from an object with properties marked with <see cref="QueryStringFieldAttribute"/>.
    /// </summary>
    public class QueryStringCompiler
    {
        /// <summary>
        /// Initializes a new instance of <see cref="QueryStringCompiler"/> class.
        /// </summary>
        public QueryStringCompiler()
        {
        }

        /// <summary>
        /// Compiles a query string from the <paramref name="container"/>.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public string Compile(object container)
        {
#if FW2
			List<string> fields = new List<string>();
#else
			ArrayList fields = new ArrayList();
#endif
            
            PropertyInfo[] properties = 
                container.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            CompileProperties(container, properties, fields);

#if FW2
            return string.Join("&", fields.ToArray());
#else
            return string.Join("&", (string[])fields.ToArray(typeof(string)));
#endif
        }

        private void CompileProperties(object container, PropertyInfo[] properties, 
#if FW2
			List<string> fields
#else
			ArrayList fields
#endif
							 )
        {
            foreach(PropertyInfo property in properties)
            {
                if(Attribute.IsDefined(property, typeof(QueryStringFieldAttribute)))
                {
                    CompileProperty(container, property, fields);
                } // if
            } // foreach
        }

        protected  virtual void CompileProperty(object container, PropertyInfo property, 
#if FW2
			List<string> fields
#else
			ArrayList fields
#endif
			)
        {
            QueryStringFieldAttribute queryStringField =
                (QueryStringFieldAttribute)Attribute.GetCustomAttribute(property, typeof(QueryStringFieldAttribute));
            
            string propertyValue = CompilePropertyValue(container, property, queryStringField);
            if(propertyValue != null && propertyValue != string.Empty)
                fields.Add(string.Format("{0}={1}", queryStringField.Name, propertyValue));
        }

        protected virtual string CompilePropertyValue(object container, PropertyInfo property,
            QueryStringFieldAttribute queryStringField)
        {
            if(property.PropertyType.IsArray)
                return CompileArrayPropertyValue(container, property);

            if(property.PropertyType.IsAssignableFrom(typeof(DateTime)))
                return CompileDateTimePropertyValue(container, property, queryStringField);

            if(typeof(IList).IsAssignableFrom(property.PropertyType))
                return CompileListPropertyValue(container, property, queryStringField);

            if(typeof(bool) == property.PropertyType)
                return CompileBooleanPropertyValue(container, property, queryStringField);

            object value = property.GetValue(container, null);
            return value == null ? null : value.ToString();
        }

        private string CompileBooleanPropertyValue(object container, PropertyInfo property, 
            QueryStringFieldAttribute queryStringField)
        {
            object value = property.GetValue(container, null);

            if(value == null)
                return null;

            return (bool)value ? "1" : "0";
        }

        private static string CompileListPropertyValue(object container, PropertyInfo property, 
            QueryStringFieldAttribute queryStringField)
        {
            IList list = (IList)property.GetValue(container, null);
            if(list == null || list.Count == 0)
                return null;

#if FW2
			List<string> values = new List<string>();
#else
			ArrayList values = new ArrayList();
#endif
            foreach(object value in list)
                values.Add(value.ToString());

#if FW2
            return string.Join(",", values.ToArray());
#else
            return string.Join(",", (string[])values.ToArray(typeof(string)));
#endif
        }

        protected string CompileDateTimePropertyValue(object container, 
            PropertyInfo property, QueryStringFieldAttribute queryStringField)
        {
            object value = property.GetValue(container, null);

            if(value == null)
                return null;

            DateTime dateTime = (DateTime)value;
            return dateTime.ToString(queryStringField.DateTimeFormatString);
        }

        private static string CompileArrayPropertyValue(object container, PropertyInfo property)
        {
            IEnumerable array = (IEnumerable)property.GetValue(container, null);

#if FW2
			List<string> values = new List<string>();
#else
			ArrayList values = new ArrayList();
#endif
            
            foreach(object value in array)
                values.Add(value.ToString());

#if FW2
            return string.Join(",", values.ToArray());
#else
            return string.Join(",", (string[])values.ToArray(typeof(string)));
#endif
        }
    }
}

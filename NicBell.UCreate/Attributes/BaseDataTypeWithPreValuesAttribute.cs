using System;
using System.Collections.Generic;
using Umbraco.Core.Models;

namespace NicBell.UCreate.Attributes
{    
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public abstract class BaseDataTypeWithPreValuesAttribute : Attribute
    {
        /// <summary>
        /// The name of the DataType
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Guid that is used to identify the DataType
        /// </summary>
        public string Key { get; set; }

        public abstract string EditorAlias { get; }
        public abstract DataTypeDatabaseType DBType { get; }
        public abstract IDictionary<string, PreValue> PreValues { get; }

        protected IDictionary<string, PreValue> CreatePreValues(string[] values)
        {
            var preValues = new Dictionary<string, PreValue>();
            for (int i = 0; i < values.Length; i++)
            {
                preValues.Add((i + 1).ToString(), new PreValue(values[i]));
            }
            return preValues;
        }
    }
}

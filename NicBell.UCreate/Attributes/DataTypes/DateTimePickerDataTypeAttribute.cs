using System.Collections.Generic;
using Umbraco.Core.Models;

namespace NicBell.UCreate.Attributes.DataTypes
{
    public class DateTimePickerDataTypeAttribute : BaseDataTypeWithPreValuesAttribute
    {
        /// <summary>
        /// The format to display the date (time) in. 
        /// Default for DateOnly: YYYY-MM-DD
        /// Default for DateTime: YYYY-MM-DD HH:mm:ss
        /// See http://momentjs.com for supported formats
        /// </summary>
        public string Format { get; set; }

        public bool DateOnly { get; set; }

        public override DataTypeDatabaseType DBType
        {
            get { return DataTypeDatabaseType.Date; }
        }

        public override string EditorAlias
        {
            get { return DateOnly ? Umbraco.Core.Constants.PropertyEditors.DateAlias : Umbraco.Core.Constants.PropertyEditors.DateTimeAlias; }
        }

        public override IDictionary<string, PreValue> PreValues
        {
            get
            {
                return new Dictionary<string, PreValue> {
                    {"format", new PreValue(Format)}
                };
            }
        }
    }
}

using System.Collections.Generic;
using Umbraco.Core.Models;

namespace NicBell.UCreate.Attributes.DataTypes
{
    public class ColorPickerDataTypeAttribute : BaseDataTypeWithPreValuesAttribute
    {
        /// <summary>
        /// In HEX notation without the # (example Red = #ff0000 use 'ff0000')
        /// </summary>
        public string[] Colors { get; set; }

        public override DataTypeDatabaseType DBType
        {
            get { return DataTypeDatabaseType.Nvarchar; }
        }

        public override string EditorAlias
        {
            get { return Umbraco.Core.Constants.PropertyEditors.ColorPickerAlias; }
        }

        public override IDictionary<string, PreValue> PreValues
        {
            get { return CreatePreValues(Colors); }
        }
    }
}

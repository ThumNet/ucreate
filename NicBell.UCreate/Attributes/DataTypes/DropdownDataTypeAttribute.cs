using System.Collections.Generic;
using Umbraco.Core.Models;

namespace NicBell.UCreate.Attributes.DataTypes
{
    public class DropdownDataTypeAttribute : BaseDataTypeWithPreValuesAttribute
    {
        public string[] Items { get; set; }
        public bool Multiselect { get; set; }

        public override DataTypeDatabaseType DBType
        {
            get { return DataTypeDatabaseType.Nvarchar; }
        }

        public override string EditorAlias
        {
            get { return Multiselect ? Umbraco.Core.Constants.PropertyEditors.DropDownListMultipleAlias : Umbraco.Core.Constants.PropertyEditors.DropDownListAlias; }
        }

        public override IDictionary<string, PreValue> PreValues
        {
            get { return CreatePreValues(Items); }
        }
    }
}

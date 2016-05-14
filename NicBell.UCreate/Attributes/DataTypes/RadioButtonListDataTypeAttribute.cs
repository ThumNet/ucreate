using System.Collections.Generic;
using Umbraco.Core.Models;

namespace NicBell.UCreate.Attributes.DataTypes
{
    public class RadioButtonListDataTypeAttribute : BaseDataTypeWithPreValuesAttribute
    {
        public string[] Items { get; set; }

        public override DataTypeDatabaseType DBType
        {
            get { return DataTypeDatabaseType.Nvarchar; }
        }

        public override string EditorAlias
        {
            get { return Umbraco.Core.Constants.PropertyEditors.RadioButtonListAlias; }
        }

        public override IDictionary<string, PreValue> PreValues
        {
            get { return CreatePreValues(Items); }
        }
    }
}

using System.Collections.Generic;
using Umbraco.Core.Models;

namespace NicBell.UCreate.Attributes.DataTypes
{
    public class CheckBoxListDataTypeAttribute : BaseDataTypeWithPreValuesAttribute
    {
        public string[] Values { get; set; }

        public override DataTypeDatabaseType DBType
        {
            get { return DataTypeDatabaseType.Nvarchar; }
        }

        public override string EditorAlias
        {
            get { return Umbraco.Core.Constants.PropertyEditors.CheckBoxListAlias; }
        }

        public override IDictionary<string, PreValue> PreValues
        {
            get { return CreatePreValues(Values); }
        }
    }
}

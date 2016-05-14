using System.Collections.Generic;
using Umbraco.Core.Models;

namespace NicBell.UCreate.Attributes.DataTypes
{
    public class TrueFalseDataTypeAttribute : BaseDataTypeWithPreValuesAttribute
    {
        public bool DefaultValue { get; set; }

        public override DataTypeDatabaseType DBType
        {
            get { return DataTypeDatabaseType.Integer; }
        }

        public override string EditorAlias
        {
            get { return Umbraco.Core.Constants.PropertyEditors.TrueFalseAlias; }
        }

        public override IDictionary<string, PreValue> PreValues
        {
            get {
                return new Dictionary<string, PreValue> {
                    {"default", new PreValue(DefaultValue ? "1" : "0")}
                };
            }
        }
    }
}

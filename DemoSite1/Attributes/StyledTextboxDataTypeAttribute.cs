using NicBell.UCreate.Attributes;
using System.Collections.Generic;
using Umbraco.Core.Models;

namespace DemoSite1.Attributes
{
    public class StyledTextboxDataTypeAttribute : BaseDataTypeWithPreValuesAttribute
    {
        public StyledTextboxDataTypeAttribute()
        {
            Style = "font-size: 36px; line-height: 45px; font-weight: bold";
        }

        public string Style { get; set; }
        public bool Multiline { get; set; }
        public string Placeholder { get; set; }
        public int CharCount { get; set; }
        public bool EnforceLimit { get; set; }
        public bool HideLabel { get; set; }

        public override string EditorAlias
        {
            get { return "Styled.TextBox"; }
        }

        public override DataTypeDatabaseType DBType
        {
            get { return DataTypeDatabaseType.Ntext; }
        }

        public override IDictionary<string, PreValue> PreValues
        {
            get
            {
                return new Dictionary<string, PreValue> {
                    {"style", new PreValue(Style)},
                    {"multiLine", new PreValue(Multiline ? "1" : "0")},
                    {"placeholder", new PreValue(Placeholder)},
                    {"charCount", new PreValue(CharCount.ToString())},
                    {"enforceLimit", new PreValue(EnforceLimit ? "1" : "0")},
                    {"hideLabel", new PreValue(HideLabel ? "1" : "0")},
                };
            }
        }
    }
}
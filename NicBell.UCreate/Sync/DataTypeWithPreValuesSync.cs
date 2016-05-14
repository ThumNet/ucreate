using NicBell.UCreate.Attributes;
using NicBell.UCreate.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace NicBell.UCreate.Sync
{
    public class DataTypeWithPreValuesSync : BaseTypeSync<BaseDataTypeWithPreValuesAttribute>
    {
        public IDataTypeService Service
        {
            get
            {
                return ApplicationContext.Current.Services.DataTypeService;
            }
        }

        protected override IEnumerable<Type> TypesToSync
        {
            get
            {
                //return _typesToSync ?? (_typesToSync = ReflectionHelper.GetTypesOfSubClass<BaseDataTypeWithPreValuesAttribute>());
                return _typesToSync ?? (_typesToSync = TypeFinder.FindClassesOfType<BaseDataTypeWithPreValuesAttribute>());
            }
        }

        protected override void Save(Type itemType)
        {
            // itemType is the Custom attribute that implements BaseDataTypeWithPreValuesAttribute
            // so get all classes that have the Custom attribute
            var dataTypes = ReflectionHelper.GetTypesWithAttribute(itemType);

            foreach (var dataType in dataTypes)
            {
                SaveDataType(dataType, itemType);
            }
        }

        private void SaveDataType(Type dataType, Type attributeType)
        {
            var attr = (BaseDataTypeWithPreValuesAttribute)dataType.GetCustomAttribute(attributeType);

            var dt = Service.GetDataTypeDefinitionById(new Guid(attr.Key)) ?? new DataTypeDefinition(attr.EditorAlias) { Key = new Guid(attr.Key) };
            dt.Name = attr.Name;
            dt.DatabaseType = attr.DBType;
            dt.PropertyEditorAlias = attr.EditorAlias;

            Service.SaveDataTypeAndPreValues(dt, MergePreValues(dt, (attr.PreValues)));
        }

        private IDictionary<string, PreValue> MergePreValues(IDataTypeDefinition dt, IDictionary<string, PreValue> newPrevalues)
        {
            var mergedPrevalues = new Dictionary<string, PreValue>();

            if (!dt.HasIdentity)
            {
                return newPrevalues;
            }

            var oldPrevalues = Service.GetPreValuesCollectionByDataTypeId(dt.Id).PreValuesAsDictionary;

            foreach (var preValue in newPrevalues)
            {
                var id = oldPrevalues.ContainsKey(preValue.Key)
                    ? oldPrevalues[preValue.Key].Id
                    : preValue.Value.Id;

                mergedPrevalues.Add(preValue.Key, new PreValue(id, preValue.Value.Value, preValue.Value.SortOrder));
            }

            return mergedPrevalues;
        }
    }
}

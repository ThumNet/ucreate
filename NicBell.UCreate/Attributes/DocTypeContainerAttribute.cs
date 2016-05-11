using System;

namespace NicBell.UCreate.Attributes
{
    public class DocTypeContainerAttribute : BaseContainerAttribute
    {
        internal override Guid ContainerObjectType
        {
            get
            {
                return Umbraco.Core.Constants.ObjectTypes.DocumentTypeContainerGuid;
            }
        }

        internal override Guid ObjectType
        {
            get
            {
                return Umbraco.Core.Constants.ObjectTypes.DocumentTypeGuid;
            }
        }
    }
}

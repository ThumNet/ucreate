using System;

namespace NicBell.UCreate.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public abstract class BaseContainerAttribute : Attribute
    {
        public string Name { get; set; }

        public Type Parent { get; set; }

        internal abstract Guid ContainerObjectType { get; }
        internal abstract Guid ObjectType { get; }
    }
}

using NicBell.UCreate.Helpers;
using System;
using System.Collections.Generic;

namespace NicBell.UCreate.Sync
{
    public abstract class BaseTypeSync<T> where T : Attribute
    {
        protected IEnumerable<Type> _typesToSync;

        /// <summary>
        /// Types to sync
        /// </summary>
        protected virtual IEnumerable<Type> TypesToSync
        {
            get
            {
                if (_typesToSync == null)
                {
                    _typesToSync = ReflectionHelper.GetTypesWithAttribute<T>();

                }

                return _typesToSync;
            }
        }


        /// <summary>
        /// Sync all
        /// </summary>
        public virtual void SyncAll()
        {
            foreach (var typeToSync in TypesToSync)
            {
                Save(typeToSync);
            }
        }


        /// <summary>
        /// Save an item
        /// </summary>
        /// <param name="itemType"></param>
        protected abstract void Save(Type itemType);
    }
}

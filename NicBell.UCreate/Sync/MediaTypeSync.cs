using NicBell.UCreate.Attributes;
using System;
using System.Linq;
using System.Reflection;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace NicBell.UCreate.Sync
{
    public class MediaTypeSync : BaseTreeContentTypeSync<MediaTypeAttribute>
    {
        internal IContentTypeService Service
        {
            get
            {
                return ApplicationContext.Current.Services.ContentTypeService;
            }
        }


        protected override IContentTypeComposition GetByAlias(string alias)
        {
            return Service.GetMediaType(alias);
        }


        protected override void Save(IContentTypeBase ct)
        {
            Service.Save(ct as IMediaType);
        }

        protected override void Save(Type itemType)
        {
            var mediaTypes = Service.GetAllMediaTypes();
            var attr = itemType.GetCustomAttribute<MediaTypeAttribute>();
            var mt = mediaTypes.FirstOrDefault(x => x.Alias == itemType.Name) ?? new MediaType(-1);

            mt.Name = attr.Name;
            mt.Alias = itemType.Name;
            mt.Icon = attr.Icon;
            mt.AllowedAsRoot = attr.AllowedAsRoot;
            mt.IsContainer = attr.IsContainer;

            SetParent(mt, itemType);
            MapProperties(mt, itemType);

            Save(mt);
        }
    }
}

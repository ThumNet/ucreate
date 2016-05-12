using NicBell.UCreate.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.UnitOfWork;
using Umbraco.Core.Services;

namespace NicBell.UCreate.Sync
{
    public class BaseContainerSync<T> : BaseTypeSync<T> where T : BaseContainerAttribute
    {
        private const int RootId = -1;

        public IContentTypeService Service
        {
            get
            {
                return ApplicationContext.Current.Services.ContentTypeService;
            }
        }

        public override void Save(Type itemType)
        {
            var attr = itemType.GetCustomAttribute<T>(false);

            Create(attr);
        }

        private int Create(T attr)
        {
            var containers = GetContainers(attr);

            var existingContainer = containers.SingleOrDefault(c => c.Name == attr.Name);
            if (existingContainer != null)
            {
                return existingContainer.Id;
            }

            if (attr.Parent == null)
            {
                var attempt1 = Service.CreateContentTypeContainer(RootId, attr.Name);
                return attempt1.Result.Entity.Id;
            }

            var parentAttr = attr.Parent.GetCustomAttribute<T>(false);
            var parentId = Create(parentAttr);
            var attempt2 = Service.CreateContentTypeContainer(parentId, attr.Name);
            return attempt2.Result.Entity.Id;
        }

        public int CreateContainers(string[] parents)
        {
            var existing = GetContainers(Umbraco.Core.Constants.ObjectTypes.DocumentTypeContainerGuid, Umbraco.Core.Constants.ObjectTypes.DocumentTypeGuid);
            var parentId = -1;
            for (int i=0; i<parents.Length; i++)
            {
                var container = existing.FirstOrDefault(e => e.ParentId == parentId && e.Name == parents[i] && e.Level == i+1);
                if (container == null)
                {
                    var attempt = Service.CreateContentTypeContainer(parentId, parents[i]);
                    parentId = attempt.Result.Entity.Id;                    
                }
                else
                {
                    parentId = container.Id;
                }
            }
            return parentId;
        }

        private IEnumerable<EntityContainer> GetContainers(Guid containerObjectType, Guid objectType)
        {
            // current service doesnt yet support getting all ContentTypeContainers thus created a workaround
            // _service.GetContentTypeContainers(new[] { RootId });
            var uowProvider = new PetaPocoUnitOfWorkProvider(LoggerResolver.Current.Logger);
            var uow = uowProvider.GetUnitOfWork();
            var sql = new Sql("SELECT * FROM umbracoNode WHERE nodeObjectType = @0", containerObjectType);

            foreach (var dto in uow.Database.Fetch<EntityDto>(sql))
            {
                yield return MapEntityDto(objectType, dto);
            }
        }

        private IEnumerable<EntityContainer> GetContainers(T attr)
        {
            return GetContainers(attr.ContainerObjectType, attr.ObjectType);
        }

        internal EntityContainer GetContainer(T attr)
        {
            // current service doesnt yet support getting all ContentTypeContainers thus created a workaround
            // _service.GetContentTypeContainers(new[] { RootId });           

            var uowProvider = new PetaPocoUnitOfWorkProvider(LoggerResolver.Current.Logger);
            var uow = uowProvider.GetUnitOfWork();
            var sql = new Sql("SELECT * FROM umbracoNode WHERE nodeObjectType = @0 AND text = @1", attr.ContainerObjectType, attr.Name);
            var dto = uow.Database.FirstOrDefault<EntityDto>(sql);
            return MapEntityDto(attr.ObjectType, dto);
        }


        private EntityContainer MapEntityDto(Guid containedObjectType, EntityDto dto)
        {
            var container = new EntityContainer(containedObjectType);
            container.Id = dto.Id;
            container.ParentId = dto.ParentId;
            container.Level = dto.Level;
            container.Path = dto.Path;
            container.SortOrder = dto.SortOrder;
            container.Key = dto.UniqueId;
            container.Name = dto.Text;
            container.CreateDate = dto.CreateDate;
            return container;
        }

        private class EntityDto
        {
            public int Id { get; set; }
            public bool Trashed { get; set; }
            public int ParentId { get; set; }
            public int Level { get; set; }
            public string Path { get; set; }
            public int SortOrder { get; set; }
            public Guid UniqueId { get; set; }
            public string Text { get; set; }
            public DateTime CreateDate { get; set; }
        }
    }
}

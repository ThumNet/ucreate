using System;
using NicBell.UCreate.Sync;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Core.Models;
using NicBell.UCreate.Constants;
using System.Linq;

namespace DemoSite1
{
    public class AppEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //DoStuff(applicationContext.Services);
            //DoStuff2();
            //DoStuff3();

            var data = applicationContext.Services.DataTypeService.GetPreValuesCollectionByDataTypeId(1076);
        }

        private void DoStuff3()
        {
            var syncer = new DataTypeWithPreValuesSync();
            syncer.SyncAll();
        }

        private void DoStuff2()
        {
            var path = "Document Base2/Nested Content2";
            var parents = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var parentId = new ContainerSync().CreateContainers(parents);
        }

        private void DoStuff(ServiceContext services)
        {
            var ctService = services.ContentTypeService;
            var dtService = services.DataTypeService;

            var testCt = ctService.GetContentType("testTuje");
            var testDt = dtService.GetAllDataTypeDefinitions().First(d => d.Name == "TestTuje - TujeColor - Color Picker");

            var alias = "SeoTest";

            var seoKey = new Guid("a16cd152-33e6-4094-84a5-3d32f8082fe3");

            var ct = ctService.GetContentType(seoKey) ?? new ContentType(-1) { Key = seoKey };
            ct.Name = "_SeoTest";
            ct.Alias = alias;
            ct.AllowedAsRoot = false;
            ct.Icon = "icon-folder";
            ct.IsContainer = false;
            ct.ParentId = -1;

            AddProperties(ct, dtService);

            ctService.Save(ct);
        }

        private void AddProperties(IContentType ct, IDataTypeService dtService)
        {
            var tabName = "Seo";
            // create the tab
            ct.AddPropertyGroup(tabName);

            var editorAlias = "Styled.TextBox";
            var dataType = dtService.GetDataTypeDefinitionByName(PropertyTypes.Textstring);            
            var prop = new PropertyType(dataType);
            //var prop = new PropertyType(editorAlias, DataTypeDatabaseType.Ntext);
            prop.Alias = "seoMetaTitle";
            prop.Name = "Meta Title";
            prop.Description = "";
            prop.Mandatory = false;            
                                   
            ct.AddPropertyType(prop, tabName);
        }

        private void DoStuff()
        {
            //var docContainerSyncer = new DocTypeContainerSync();
            //var docTypeSyncer = new DocTypeSync();

            //docContainerSyncer.SyncAll();
            //docTypeSyncer.SyncAll();
        }
    }    
}
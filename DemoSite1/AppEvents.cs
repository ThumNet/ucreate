using NicBell.UCreate.Sync;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace DemoSite1
{
    public class AppEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            DoStuff();
        }

        private void DoStuff()
        {
            var docContainerSyncer = new DocTypeContainerSync();
            var docTypeSyncer = new DocTypeSync();

            docContainerSyncer.SyncAll();
            docTypeSyncer.SyncAll();
        }
    }    
}
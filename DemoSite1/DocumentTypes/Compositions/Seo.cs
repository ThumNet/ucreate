using NicBell.UCreate.Attributes;

namespace DemoSite1.DocumentTypes.Compositions
{
    [DocType(Name ="_Seo", Description = "", Folder = typeof(CompositionsContainer))]
    public class Seo
    {
        //[Property(name: "Meta titel", Alias = "metatitle", Description = "De titel mag maximaal 65 karakters zijn.",
        //    TabName = "Seo", TypeName = "CharLimitEditor")]
        //public string MetaTitle { get; set; }
    }
}
using NicBell.UCreate.Attributes;

namespace DemoSite1.DocumentTypes
{

    [DocTypeContainer(Name = "Compositions")]
    public class CompositionsContainer
    { }

    [DocTypeContainer(Name = "Document Base")]
    public class DocumentBaseContainer
    { }

    [DocTypeContainer(Name = "Nested Content", Parent = typeof(DocumentBaseContainer))]
    public class NestedContentContainer
    { }
}
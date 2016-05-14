namespace NicBell.UCreate.Attributes
{
    public class DocTypeAttribute : BaseTreeContentTypeAttribute
    {
        //TODO: tuje
        ///// <summary>
        ///// The alias for the DocType
        ///// </summary>
        //public string Alias { get; set; }

        /// <summary>
        /// List of allowed templates
        /// </summary>
        public string[] AllowedTemplates { get; set; }

        /// <summary>
        /// Default template
        /// </summary>
        public string DefaultTemplate { get; set; }

        /// <summary>
        /// The path for the document type (visual in the backoffice), DocumentTypeContainers are created. 
        /// For nesting use '/' for example: Sites/Pages
        /// </summary>
        public string Path { get; set; }
    }
}

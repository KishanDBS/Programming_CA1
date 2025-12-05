namespace FileExtensionInfoSystem
{
    /// <summary>
    /// Represents a single file extension and its description.
    /// This is a simple data model class.
    /// </summary>
    public class ExtensionInfo
    {
        // Property to store the extension (e.g., ".mp4")
        public string Extension { get; set; }

        // Property to store the description of the extension
        public string Description { get; set; }

        // Constructor to initialize the ExtensionInfo object
        public ExtensionInfo(string extension, string description)
        {
            Extension = extension;
            Description = description;
        }
    }
}
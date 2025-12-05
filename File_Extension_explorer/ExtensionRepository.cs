using System.Collections.Generic;

namespace FileExtensionInfoSystem
{
    /// <summary>
    /// Repository class that stores all supported file extensions.
    /// Uses a Dictionary for fast lookups.
    /// </summary>
    public class ExtensionRepository
    {
        // Dictionary to store extensions as keys and ExtensionInfo objects as values
        private Dictionary<string, ExtensionInfo> extensions;

        // Constructor initializes the dictionary with at least 20 extensions
        public ExtensionRepository()
        {
            // Case-insensitive lookup (so ".MP4" and ".mp4" both work)
            extensions = new Dictionary<string, ExtensionInfo>(System.StringComparer.OrdinalIgnoreCase)
            {
                {".mp4", new ExtensionInfo(".mp4", "MPEG-4 Video File - widely used for video streaming and storage.")},
                {".mov", new ExtensionInfo(".mov", "Apple QuickTime Movie File - commonly used for video playback.")},
                {".avi", new ExtensionInfo(".avi", "Audio Video Interleave - Microsoft video format with good quality.")},
                {".mkv", new ExtensionInfo(".mkv", "Matroska Video File - supports multiple audio and subtitle tracks.")},
                {".webm", new ExtensionInfo(".webm", "WebM Video File - open-source format optimized for web streaming.")},
                {".mp3", new ExtensionInfo(".mp3", "MPEG Audio Layer III - compressed audio format widely used for music.")},
                {".wav", new ExtensionInfo(".wav", "Waveform Audio File - uncompressed audio format with high quality.")},
                {".flac", new ExtensionInfo(".flac", "Free Lossless Audio Codec - compressed audio without quality loss.")},
                {".jpg", new ExtensionInfo(".jpg", "JPEG Image File - commonly used compressed image format.")},
                {".png", new ExtensionInfo(".png", "Portable Network Graphics - lossless image format with transparency support.")},
                {".gif", new ExtensionInfo(".gif", "Graphics Interchange Format - supports animation and transparency.")},
                {".bmp", new ExtensionInfo(".bmp", "Bitmap Image File - uncompressed raster image format.")},
                {".pdf", new ExtensionInfo(".pdf", "Portable Document Format - used for documents and e-books.")},
                {".docx", new ExtensionInfo(".docx", "Microsoft Word Document - modern Word file format.")},
                {".xlsx", new ExtensionInfo(".xlsx", "Microsoft Excel Spreadsheet - modern Excel file format.")},
                {".pptx", new ExtensionInfo(".pptx", "Microsoft PowerPoint Presentation - modern PowerPoint file format.")},
                {".txt", new ExtensionInfo(".txt", "Plain Text File - contains unformatted text.")},
                {".html", new ExtensionInfo(".html", "HyperText Markup Language File - used for web pages.")},
                {".css", new ExtensionInfo(".css", "Cascading Style Sheets File - used for styling web pages.")},
                {".js", new ExtensionInfo(".js", "JavaScript File - used for client-side scripting in web development.")}
            };
        }

        /// <summary>
        /// Returns information about a given extension if it exists.
        /// </summary>
        public ExtensionInfo GetExtensionInfo(string extension)
        {
            if (extensions.ContainsKey(extension))
                return extensions[extension];
            return null; // Return null if extension not found
        }

        /// <summary>
        /// Returns all supported extensions (keys of the dictionary).
        /// </summary>
        public IEnumerable<string> GetAllExtensions()
        {
            return extensions.Keys;
        }

        /// <summary>
        /// Returns all ExtensionInfo objects (extension + description).
        /// This is used when we want to show both extension and description.
        /// </summary>
        public IEnumerable<ExtensionInfo> GetAllExtensionInfos()
        {
            return extensions.Values;
        }
    }
}
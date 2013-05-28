using System;

namespace ExtraMvcExtension.Bootstrap
{
    /// <summary>
    /// Represents a visited page. Used for navigation history purposes.
    /// </summary>
    public class VisitedPage : IEquatable<VisitedPage>, IEquatable<string>
    {


        /// <summary>
        /// Title of the page (from the Viewbag.Title).
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Page Uri
        /// </summary>
        public Uri Uri { get; set; }

        public bool Equals(VisitedPage other)
        {
            return Uri.AbsolutePath.Equals(other.Uri.AbsolutePath) || Uri.AbsoluteUri.Equals(other.Uri.AbsoluteUri);
        }

        public bool Equals(string other)
        {
            return Uri.AbsolutePath.Equals(other) || Uri.AbsoluteUri.Equals(other);
        }
    }
}

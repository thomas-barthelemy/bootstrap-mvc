using System;
using System.Web.Mvc;
using ExtraMVCExtension.Bootstrap.Enums;
using ExtraMVCExtension.Bootstrap.Utils;

namespace ExtraMVCExtension.Bootstrap
{
    /// <summary>
    /// Represents a Boostrap list element in an MVC view.
    /// </summary>
    public class BootstrapMvcList : IDisposable
    {
        private readonly ViewContext _context;
        private bool _isStopped;
        private TagBuilder _listTags;

        /// <summary>
        /// Create a new Bootstrap list for an MVC view.
        /// </summary>
        internal BootstrapMvcList(ViewContext context)
        {
            _context = context;
        }

        internal void BeginList(ListType listType)
        {
            _listTags = GetRootTagBuilder(listType);
            _context.Writer.WriteLine(MvcHtmlString.Create(_listTags.ToString(TagRenderMode.StartTag)));
        }

        internal void BeginDescriptionList(bool isHorizontal)
        {
            _listTags = new TagBuilder("dl");
            if (isHorizontal)
                _listTags.AddCssClass("dl-horizontal");
            _context.Writer.WriteLine(MvcHtmlString.Create(_listTags.ToString(TagRenderMode.StartTag)));
        }

        internal void StopList()
        {
            if (_isStopped) return;

            _context.Writer.WriteLine(MvcHtmlString.Create(_listTags.ToString(TagRenderMode.EndTag)));
            _isStopped = true;
        }

        internal static TagBuilderExt GetRootTagBuilder(ListType listType)
        {
            TagBuilder root;
            switch (listType)
            {
                case ListType.Unordered:
                    root = new TagBuilder("ul");
                    break;
                case ListType.Ordered:
                    root = new TagBuilder("ol");
                    break;
                case ListType.Unstyled:
                    root = new TagBuilder("ul");
                    root.AddCssClass("unstyled");
                    break;
                case ListType.Inline:
                    root = new TagBuilder("ul");
                    root.AddCssClass("inline");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("listType");
            }
            return root;
        }

        private void Dispose(bool cleanManaged)
        {
            StopList();

            if (!cleanManaged) return;

            // Clean up managed resources
            _listTags = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
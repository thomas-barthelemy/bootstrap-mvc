using System;
using System.Web.Mvc;

namespace ExtraMvcExtension.Bootstrap.BootstrapModels
{
    public class BootstrapBreadcrumb : IDisposable
    {
               private readonly ViewContext _context;
        private bool _isStopped;
        private TagBuilder _breadcrumbsTag;

        /// <summary>
        /// Create a new Bootstrap Breadcrumbs container for an MVC view.
        /// </summary>
        internal BootstrapBreadcrumb(ViewContext context)
        {
            _context = context;
        }

        internal void BeginBreadcrumb()
        {
            _breadcrumbsTag = new TagBuilderExt("ul");
            _breadcrumbsTag.AddCssClass("breadcrumb");
            _context.Writer.WriteLine(MvcHtmlString.Create(_breadcrumbsTag.ToString(TagRenderMode.StartTag)));
        }

        internal void StopList()
        {
            if (_isStopped) return;

            _context.Writer.WriteLine(MvcHtmlString.Create(_breadcrumbsTag.ToString(TagRenderMode.EndTag)));
            _isStopped = true;
        }


        private void Dispose(bool cleanManaged)
        {
            StopList();

            if (!cleanManaged) return;

            // Clean up managed resources
            _breadcrumbsTag = null;
        }

        /// <summary>
        /// Dispose the BootstrapBreadcrumb and call for garbage collection.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

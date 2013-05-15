using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ExtraMvcExtension.Bootstrap.Enums;

namespace ExtraMvcExtension.Bootstrap
{
    public class BootstrapMvcMenuItemsContainer : IDisposable
    {
        private readonly ViewContext _context;
        private bool _isStopped;
        private TagBuilderExt _listTag;

        internal BootstrapMvcMenuItemsContainer(ViewContext context)
        {
            _context = context;
        }

        internal void BeginMenuItems()
        {
            _listTag = new TagBuilderExt("ul");
            _listTag.AddCssClass("nav");

            _context.Writer.WriteLine(_listTag.ToString(TagRenderMode.StartTag));
        }

        internal void StopMenu()
        {
            if (_isStopped) return;

            _context.Writer.WriteLine(MvcHtmlString.Create(_listTag.ToString(TagRenderMode.EndTag)));
            _isStopped = true;
        }

        private void Dispose(bool cleanManaged)
        {
            StopMenu();

            if (!cleanManaged) return;

            // Clean up managed resources
            _listTag = null;
        }

        /// <summary>
        /// Dispose the BootstrapMvcList and call for garbage collection.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

using System.Web.Mvc;

namespace ExtraMVCExtension.Bootstrap
{
    /// <summary>
    /// Represents an MVC web page with an included Bootstrap HTML helper.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class BootstrapMvcPage<TModel> : WebViewPage<TModel>
    {
        private BootstrapHelper _bootstrap;

        public override void Execute()
        {
        }

        /// <summary>
        /// Provides a set of methods to generate HTML code for Bootstrap.
        /// </summary>
        public BootstrapHelper Bootstrap
        {
            get { return _bootstrap ?? (_bootstrap = new BootstrapHelper(Html)); }
        }
    }
}

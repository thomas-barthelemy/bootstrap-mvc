using System.Web.Mvc;

namespace ExtraMVCExtension.Bootstrap
{
    /// <summary>
    /// Represents an MVC web page with an included Bootstrap HTML helper.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class BootstrapMVCPage<TModel> : WebViewPage<TModel>
    {
        private BootstrapHelper _bootstrap;

        /// <summary>
        /// Execute the server code in the current web page that is marked using the Razor syntax.
        /// </summary>
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

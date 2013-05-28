using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace ExtraMvcExtension.Bootstrap.BootstrapModels
{
    /// <summary>
    /// Represents an MVC web page with an included Bootstrap HTML helper.
    /// </summary>
    public class BootstrapMvcPage : WebViewPage
    {
        private BootstrapHelper _bootstrap;

        protected override void InitializePage()
        {
            base.InitializePage();

            if (Request.Url == null) return;

            var currentUrl = Request.Url.AbsoluteUri;

            if(string.IsNullOrWhiteSpace(ViewBag.Title))
                return;

            if (!NavigationHistory.Any() || !NavigationHistory.Last().Equals(currentUrl))
                AddToHistory(ViewBag.Title, Request.Url);
        }

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
            get { return _bootstrap ?? (_bootstrap = new BootstrapHelper(this)); }
        }

        internal void AddToHistory(string title, Uri uri)
        {
            var history = Session["history"] as List<VisitedPage> ?? new List<VisitedPage>();
            history.Add(new VisitedPage { Title = title, Uri = uri});
            Session["history"] = history;
        }

        /// <summary>
        /// Provides the navigation history as a list of visited URLs.
        /// </summary>
        public IEnumerable<VisitedPage> NavigationHistory
        {
            get { return Session["history"] as List<VisitedPage> ?? new List<VisitedPage>(); }
        }
    }

    /// <summary>
    /// Represents an MVC web page with an included Bootstrap HTML helper.
    /// </summary>
    public class BootstrapMvcPage<TModel> : BootstrapMvcPage
    {

    }
}

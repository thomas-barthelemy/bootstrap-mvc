using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using ExtraMvcExtension.Bootstrap.BootstrapModels;
using ExtraMvcExtension.Bootstrap.Enums;

namespace ExtraMvcExtension.Bootstrap
{
    /// <summary>
    /// Provides methods to generate HTML code for Twitter's Bootstrap.
    /// </summary>
    public class BootstrapHelper
    {
        #region Fields
        private readonly HtmlHelper _html;
        private readonly UrlHelper _url;

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of the Bootstrap helper.
        /// </summary>
        /// <param name="page">The current view</param>
        public BootstrapHelper(WebViewPage page)
        {
            _html = page.Html;
            _url = page.Url;
        }
        #endregion

        #region Typography
        /// <summary>
        /// Makes a paragraph using the lead class to make it stand out.
        /// </summary>
        /// <param name="text">Content of the paragraph</param>
        public MvcHtmlString LeadBody(string text)
        {
            var p = new TagBuilderExt("p");
            p.AddCssClass("lead");
            p.SetInnerText(text);

            return p.ToMvcHtmlString();
        }

        /// <summary>
        /// Creates an emphasized paragraph
        /// </summary>
        /// <param name="text">Content of the emphasized paragraph</param>
        /// <param name="emphasisType">Type of the emphasis</param>
        public MvcHtmlString EmphasizedParagraph(string text, EmphasisType emphasisType)
        {
            var p = new TagBuilderExt("p");

            switch (emphasisType)
            {
                case EmphasisType.Muted:
                    p.AddCssClass("muted");
                    break;
                case EmphasisType.Warning:
                    p.AddCssClass("text-warning");
                    break;
                case EmphasisType.Error:
                    p.AddCssClass("text-error");
                    break;
                case EmphasisType.Info:
                    p.AddCssClass("text-info");
                    break;
                case EmphasisType.Success:
                    p.AddCssClass("text-success");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("emphasisType");
            }

            p.SetInnerText(text);
            return p.ToMvcHtmlString();
        }

        /// <summary>
        /// Creates an HTML abbreviation with it's corresponding definition.
        /// </summary>
        /// <param name="title">Definition of the abbreaviation.</param>
        /// <param name="value">The abbreviation.</param>
        public MvcHtmlString Abbreviation(string title, string value)
        {
            return Abbreviation(title, value, false);
        }
        /// <summary>
        /// Creates an HTML abbreviation with it's corresponding definition.
        /// </summary>
        /// <param name="title">Definition of the abbreaviation.</param>
        /// <param name="value">The abbreviation.</param>
        /// <param name="isReduced">Defines if the abbreviation uses the initialism class for a slightly smaller font-size.</param>
        public MvcHtmlString Abbreviation(string title, string value, bool isReduced)
        {
            var abbr = new TagBuilder("abbr");
            if (isReduced)
                abbr.AddCssClass("initialism");
            abbr.MergeAttribute("title", title);
            abbr.SetInnerText(value);

            return MvcHtmlString.Create(abbr.ToString());
        }

        /// <summary>
        /// Creates an HTML blockquote.
        /// </summary>
        /// <param name="quote">The quote.</param>
        /// <param name="author">The author.</param>
        /// <param name="source">The source.</param>
        /// <param name="sourceTitle">The source title.</param>
        public MvcHtmlString Blockquote(string quote, string author, string source, string sourceTitle)
        {
            return Blockquote(quote, author, source, sourceTitle, false);
        }
        /// <summary>
        /// Creates an HTML blockquote.
        /// </summary>
        /// <param name="quote">The quote.</param>
        /// <param name="author">The author.</param>
        /// <param name="source">The source.</param>
        /// <param name="sourceTitle">The source title.</param>
        /// <param name="isPulledRight">Set to true for a floated, right-aligned blockquote.</param>
        public MvcHtmlString Blockquote(string quote, string author, string source, string sourceTitle, bool isPulledRight)
        {
            var cite = new TagBuilderExt("cite", source);
            cite.MergeAttribute("title", sourceTitle);

            var small = new TagBuilderExt("small") { InnerHtml = String.Concat(author, " ", cite.ToString()) };

            var p = new TagBuilderExt("p", quote);

            var blockquote = new TagBuilderExt("blockquote");
            if (isPulledRight)
                blockquote.AddCssClass("pull-right");

            blockquote.InnerHtml = String.Concat(p.ToString(), small.ToString());

            return blockquote.ToMvcHtmlString();
        }

        /// <summary>
        /// Begins a new list tag
        /// </summary>
        /// <param name="listType">Type of the desired list.</param>
        /// <returns></returns>
        public BootstrapMvcList BeginList(ListType listType)
        {
            var list = new BootstrapMvcList(_html.ViewContext);
            list.BeginList(listType);

            return list;
        }
        /// <summary>
        /// Creates a list with the associated elements.
        /// </summary>
        /// <param name="listType">The type of the list.</param>
        /// <param name="elements">The elements of the list.</param>
        public MvcHtmlString List(ListType listType, IEnumerable<string> elements)
        {
            var root = BootstrapMvcList.GetRootTagBuilder(listType);

            if (elements == null)
                return MvcHtmlString.Create(root.ToString());

            foreach (var element in elements)
            {
                root.AddChildTag(new TagBuilderExt("li", element));
            }

            return root.ToMvcHtmlString();
        }

        /// <summary>
        /// Creates a list of terms with their associated descriptions.
        /// </summary>
        /// <param name="isHorizontal">Make terms and descriptions in dl line up side-by-side.</param>
        public BootstrapMvcList BeginDescriptionList(bool isHorizontal)
        {
            var list = new BootstrapMvcList(_html.ViewContext);
            list.BeginDescriptionList(isHorizontal);

            return list;
        }
        /// <summary>
        /// Creates a description list with the associated descriptions
        /// </summary>
        /// <param name="isHorizontal">Make terms and descriptions in line up side-by-side.</param>
        /// <param name="elements">The dictionary of descriptions by title (key) and description (value).</param>
        public MvcHtmlString DescriptionList(bool isHorizontal, IDictionary<string, string> elements)
        {
            if (elements == null)
                return null;

            var root = new TagBuilderExt("dl");
            if (isHorizontal)
                root.AddCssClass("dl-horizontal");

            foreach (var element in elements)
            {
                root.AddChildTag(new TagBuilderExt("dt", element.Key));
                root.AddChildTag(new TagBuilderExt("dd", element.Value));
            }

            return root.ToMvcHtmlString();
        }
        #endregion

        #region Buttons

        private TagBuilderExt CreateBaseButton(string tagName, ButtonStyle buttonStyle, ButtonSize buttonSize, bool isDisabled)
        {
            var tag = new TagBuilderExt(tagName);
            tag.AddCssClass("btn");

            switch (buttonStyle)
            {
                case ButtonStyle.Primary:
                    tag.AddCssClass("btn-primary");
                    break;
                case ButtonStyle.Info:
                    tag.AddCssClass("btn-info");
                    break;
                case ButtonStyle.Success:
                    tag.AddCssClass("btn-success");
                    break;
                case ButtonStyle.Warning:
                    tag.AddCssClass("btn-warning");
                    break;
                case ButtonStyle.Danger:
                    tag.AddCssClass("btn-danger");
                    break;
                case ButtonStyle.Inverse:
                    tag.AddCssClass("btn-inverse");
                    break;
                case ButtonStyle.Link:
                    tag.AddCssClass("btn-link");
                    break;
            }

            switch (buttonSize)
            {
                case ButtonSize.Large:
                    tag.AddCssClass("btn-large");
                    break;
                case ButtonSize.Small:
                    tag.AddCssClass("btn-small");
                    break;
                case ButtonSize.Mini:
                    tag.AddCssClass("btn-mini");
                    break;
            }

            if (isDisabled)
                tag.AddCssClass("disabled");

            return tag;
        }

        #region Link buttons

        /// <summary>
        /// Creates an "a" tag styled as a button.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        /// <param name="href">Link destination.</param>
        /// <param name="buttonType">The button style type.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <param name="isDisabled">Make buttons look unclickable by fading them back 50%. True to disable, False to enable.</param>
        public MvcHtmlString LinkButton(string text, string href, ButtonStyle buttonType, ButtonSize buttonSize, bool isDisabled)
        {
            var tag = CreateBaseButton("a", buttonType, buttonSize, isDisabled);
            tag.SetInnerText(text);
            tag.MergeAttribute("href", href);

            return tag.ToMvcHtmlString();
        }
        /// <summary>
        /// Creates an "a" tag with a default button style and size.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        /// <param name="href">Link destination.</param>
        public MvcHtmlString LinkButton(string text, string href)
        {
            return LinkButton(text, href, ButtonStyle.Default, ButtonSize.Default, false);
        }
        #endregion

        #region Submit buttons
        /// <summary>
        /// Creates a submit button tag with the given style and size.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        /// <param name="buttonStyle">The button style type.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <param name="isDisabled">Make buttons look unclickable by fading them back 50%. True to disable, False to enable.</param>
        public MvcHtmlString SubmitButton(string text, ButtonStyle buttonStyle, ButtonSize buttonSize, bool isDisabled)
        {
            var tag = CreateBaseButton("button", buttonStyle, buttonSize, isDisabled);
            tag.MergeAttribute("type", "submit");
            tag.SetInnerText(text);

            return new MvcHtmlString(tag.ToString());
        }
        /// <summary>
        /// Creates a submit button tag with a default style and size.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        public MvcHtmlString SubmitButton(string text)
        {
            return SubmitButton(text, ButtonStyle.Default, ButtonSize.Default, false);
        }
        #endregion

        #region Input buttons
        /// <summary>
        /// Creates an input button tag with the given style and size.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        /// <param name="buttonStyle">The button style type.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <param name="isDisabled">Make buttons look unclickable by fading them back 50%. True to disable, False to enable.</param>
        public MvcHtmlString InputButton(string text, ButtonStyle buttonStyle, ButtonSize buttonSize, bool isDisabled)
        {
            var tag = CreateBaseButton("input", buttonStyle, buttonSize, isDisabled);
            tag.MergeAttribute("type", "button");
            tag.SetInnerText(text);

            return new MvcHtmlString(tag.ToString());
        }
        /// <summary>
        /// Creates an input button tag with a default style and size.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        public MvcHtmlString InputButton(string text)
        {
            var tag = CreateBaseButton("input", ButtonStyle.Default, ButtonSize.Default, false);
            tag.MergeAttribute("type", "button");
            tag.SetInnerText(text);

            return new MvcHtmlString(tag.ToString());
        }
        #endregion

        #region Input buttons
        /// <summary>
        /// Creates an input submit button tag with the given style and size.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        /// <param name="buttonStyle">The button style type.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <param name="isDisabled">Make buttons look unclickable by fading them back 50%. True to disable, False to enable.</param>
        public MvcHtmlString InputSubmitButton(string text, ButtonStyle buttonStyle, ButtonSize buttonSize, bool isDisabled)
        {
            var tag = CreateBaseButton("input", buttonStyle, buttonSize, isDisabled);
            tag.MergeAttribute("type", "submit");
            tag.SetInnerText(text);

            return new MvcHtmlString(tag.ToString());
        }
        /// <summary>
        /// Creates an input submit button tag with a default style and size.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        public MvcHtmlString InputSubmitButton(string text)
        {
            var tag = CreateBaseButton("input", ButtonStyle.Default, ButtonSize.Default, false);
            tag.MergeAttribute("type", "submit");
            tag.SetInnerText(text);

            return new MvcHtmlString(tag.ToString());
        }
        #endregion

        #endregion

        #region Images
        /// <summary>
        /// Define an image
        /// </summary>
        /// <param name="source">the url for the image</param>
        /// <param name="alt">alternate text for the image</param>
        /// <param name="imageType">type of image</param>
        public MvcHtmlString Image(string source, string alt, ImageType imageType)
        {
            var img = new TagBuilderExt("img");
            img.MergeAttribute("alt", alt);
            img.MergeAttribute("src", source);

            switch (imageType)
            {
                case ImageType.Rounded:
                    img.AddCssClass("img-rounded");
                    break;
                case ImageType.Circle:
                    img.AddCssClass("img-circle");
                    break;
                case ImageType.Polaroid:
                    img.AddCssClass("img-polaroid");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("imageType");
            }

            return img.ToMvcHtmlString();
        }
        /// <summary>
        /// Define an Image(source as the alternate text). 
        /// </summary>
        /// <param name="source">url and alternate text</param>
        /// <param name="imageType">type of image</param>
        public MvcHtmlString Image(string source, ImageType imageType)
        {
            return Image(source, source, imageType);
        }
        #endregion

        #region Menu

        /// <summary>
        /// Creates a basic default menu (navbar) that can contains menu componenents.
        /// </summary>
        public BootstrapMvcMenu BeginMenu()
        {
            return BeginMenu(MenuType.Basic, false);
        }
        /// <summary>
        /// Creates a menu (navbar) that can contains menu componenents.
        /// </summary>
        /// <param name="menuType">The menu type</param>
        public BootstrapMvcMenu BeginMenu(MenuType menuType)
        {
            return BeginMenu(menuType, false);
        }
        /// <summary>
        /// Creates a menu (navbar) that can contains menu componenents.
        /// </summary>
        /// <param name="menuType">The menu type</param>
        /// <param name="isInversed">Reverse the navbar colors: True to reverse, False (default) for normal.</param>
        public BootstrapMvcMenu BeginMenu(MenuType menuType, bool isInversed)
        {
            var menu = new BootstrapMvcMenu(_html.ViewContext);
            menu.BeginMenu(menuType, isInversed);
            return menu;
        }

        /// <summary>
        /// Creates a menu title (have to be used inside a navbar menu).
        /// </summary>
        /// <param name="title">Title of the menu</param>
        /// <param name="action">The name of the a action</param>
        /// <param name="controller">The name of the controller</param>
        /// <param name="routeValues">An object that contains the parameters for a route.
        /// The parameters are retrieved through reflection by examining the properties of the object.
        /// The object is typically created by using object initializer syntax.</param>
        public MvcHtmlString MenuTitle(string title, string action, string controller, object routeValues)
        {
            return MenuTitle(title, _url.Action(action, controller, routeValues));
        }
        /// <summary>
        /// Creates a menu title (have to be used inside a navbar menu).
        /// </summary>
        /// <param name="title">Title of the menu</param>
        /// <param name="action">The action name</param>
        /// <param name="controller">The controller name</param>
        public MvcHtmlString MenuTitle(string title, string action, string controller)
        {
            return MenuTitle(title, _url.Action(action, controller));
        }
        /// <summary>
        /// Creates a menu title (have to be used inside a navbar menu).
        /// </summary>
        /// <param name="title">Title of the menu</param>
        /// <param name="url">A Fully quallified URL</param>
        public MvcHtmlString MenuTitle(string title, string url)
        {
            var a = new TagBuilderExt("a", title);
            a.AddCssClass("brand");
            a.MergeAttribute("href", url);

            return a.ToMvcHtmlString();
        }

        /// <summary>
        /// Begins a menu items container.
        /// </summary>
        public BootstrapMvcMenuItemsContainer BeginMenuItems()
        {
            var menuItems = new BootstrapMvcMenuItemsContainer(_html.ViewContext);
            menuItems.BeginMenuItems();

            return menuItems;
        }

        /// <summary>
        /// Creates one menu item entry with the specified title and link.
        /// </summary>
        /// <param name="title">The title of the menu item</param>
        /// <param name="action">The name of the action</param>
        /// <param name="controller">The name of the controller</param>
        /// <param name="routeValues">An object that contains the parameters for a route.
        /// The parameters are retrieved through reflection by examining the properties of the object.
        /// The object is typically created by using object initializer syntax.</param>
        /// <param name="isActive">Defines if the link have an active (selected) style or not: True to apply the active style, False for normal style.</param>
        public MvcHtmlString MenuItem(string title, string action, string controller, object routeValues, bool isActive)
        {
            return MenuItem(title, _url.Action(action, controller, routeValues), isActive);
        }
        /// <summary>
        /// Creates one menu item entry with the specified title and link.
        /// </summary>
        /// <param name="title">The title of the menu item</param>
        /// <param name="action">The name of the action</param>
        /// <param name="controller">The name of the controller</param>
        /// <param name="routeValues">An object that contains the parameters for a route.
        /// The parameters are retrieved through reflection by examining the properties of the object.
        /// The object is typically created by using object initializer syntax.</param>
        public MvcHtmlString MenuItem(string title, string action, string controller, object routeValues)
        {
            return MenuItem(title, _url.Action(action, controller, routeValues), false);
        }
        /// <summary>
        /// Creates one menu item entry with the specified title and link.
        /// </summary>
        /// <param name="title">The title of the menu item</param>
        /// <param name="action">The name of the action</param>
        /// <param name="controller">The name of the controller</param>
        /// <param name="isActive">Defines if the link have an active (selected) style or not: True to apply the active style, False for normal style.</param>
        public MvcHtmlString MenuItem(string title, string action, string controller, bool isActive)
        {
            return MenuItem(title, _url.Action(action, controller), isActive);
        }
        /// <summary>
        /// Creates one menu item entry with the specified title and link.
        /// </summary>
        /// <param name="title">The title of the menu item</param>
        /// <param name="action">The name of the action</param>
        /// <param name="controller">The name of the controller</param>
        public MvcHtmlString MenuItem(string title, string action, string controller)
        {
            return MenuItem(title, _url.Action(action, controller), false);
        }
        /// <summary>
        /// Creates one menu item entry with the specified title and link.
        /// </summary>
        /// <param name="title">The title of the menu item</param>
        /// <param name="url">The fully quallified URL</param>
        /// <param name="isActive">Defines if the link have an active (selected) style or not: True to apply the active style, False for normal style.</param>
        public MvcHtmlString MenuItem(string title, string url, bool isActive)
        {
            var listItem = new TagBuilderExt("li");
            if (isActive)
                listItem.AddCssClass("active");
            var link = new TagBuilderExt("a");
            link.MergeAttribute("href", url);
            link.SetInnerText(title);
            listItem.AddChildTag(link);

            return listItem.ToMvcHtmlString();
        }
        /// <summary>
        /// Creates one menu item entry with the specified title and link.
        /// </summary>
        /// <param name="title">The title of the menu item</param>
        /// <param name="url">The fully quallified URL</param>
        public MvcHtmlString MenuItem(string title, string url)
        {
            return MenuItem(title, url, false);
        }

        /// <summary>
        /// Creates a vertical menu-item separator
        /// </summary>
        public MvcHtmlString MenuItemSeparator()
        {
            var tag = new TagBuilderExt("li");
            tag.AddCssClass("divider-vertical");

            return tag.ToMvcHtmlString();
        }


        #endregion
    }
}

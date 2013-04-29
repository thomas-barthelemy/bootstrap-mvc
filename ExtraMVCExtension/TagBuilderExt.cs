using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using System.Linq;

namespace ExtraMVCExtension.Bootstrap
{
    /// <summary>
    /// Extends the basic TagBuilder in order to better support hierarchical tags
    /// </summary>
    public class TagBuilderExt : TagBuilder
    {
        private Collection<TagBuilderExt> _childrenTags;

        #region Constructors

        /// <summary>
        /// Instanciates a new tag with a given name.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        public TagBuilderExt(string tagName) : base(tagName)
        {
        }

        /// <summary>
        /// Instanciates a new tag with a given name and a set of child tags.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <param name="childs">Child tags collection.</param>
        public TagBuilderExt(string tagName, IEnumerable<TagBuilderExt> childs) : base(tagName)
        {
            if (childs == null)
                return;

            foreach (var tag in childs)
            {
                ChildrenTags.Add(tag);
            }
        }

        /// <summary>
        /// Instanciates a new tag with a given name and a simple text content.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <param name="innerText">Inner text of the tag.</param>
        public TagBuilderExt(string tagName, string innerText) : base(tagName)
        {
            SetInnerText(innerText);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Lists all the tag inner tag.
        /// </summary>
        public Collection<TagBuilderExt> ChildrenTags
        {
            get { return _childrenTags ?? (_childrenTags = new Collection<TagBuilderExt>()); }
        }
        #endregion

        /// <summary>
        /// Add a tag as an innner tag of the current instance.
        /// </summary>
        /// <param name="child">Child tag.</param>
        public void AddChildTag(TagBuilderExt child)
        {
            ChildrenTags.Add(child);
        }

        public override string ToString()
        {
            return ToString(TagRenderMode.Normal);
        }

        /// <summary>
        /// Renders HTML code for the tag.
        /// </summary>
        /// <param name="tagRenderMode">Rendering mode used to create the HTML code.</param>
        /// <returns>HTML code for the tag and its child tags.</returns>
        public new string ToString(TagRenderMode tagRenderMode)
        {
            var tmp = InnerHtml;

            InnerHtml += string.Concat(ChildrenTags.SelectMany(c => c.ToString()));
            var result = base.ToString(tagRenderMode);
            InnerHtml = tmp;
            return result;
        }
        
    }
}

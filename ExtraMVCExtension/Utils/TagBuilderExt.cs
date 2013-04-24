using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace ExtraMVCExtension.Bootstrap.Utils
{
    internal class TagBuilderExt : TagBuilder
    {
        private List<TagBuilderExt> _childrenTags;

        #region Constructors
        public TagBuilderExt(string tagName) : base(tagName)
        {
        }

        public TagBuilderExt(string tagName, IEnumerable<TagBuilderExt> childs) : base(tagName)
        {
            ChildrenTags.AddRange(childs);
        }

        public TagBuilderExt(string tagName, string innerText) : base(tagName)
        {
            SetInnerText(innerText);
        }
        #endregion

        #region Properties
        public List<TagBuilderExt> ChildrenTags
        {
            get { return _childrenTags ?? (_childrenTags = new List<TagBuilderExt>()); }
        }
        #endregion

        public void AddChildTag(TagBuilderExt child)
        {
            ChildrenTags.Add(child);
        }

        public override string ToString()
        {
            return ToString(TagRenderMode.Normal);
        }

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

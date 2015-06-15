using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Snoopy.Data;
using Snoopy.Data.Model;
using Snoopy.Model;

namespace Snoopy.Core
{
    public static class Extends
    {
        public static user_profile Touser_profile(this ApplicationUser user)
        {
            var userprofile = new user_profile();
            userprofile.ID = UserQuery.GetCount("where 1=1") + 1;
            userprofile.user_id = user.Id;
            userprofile.birthday = user.Birthday;
            userprofile.user_name = user.UserName;
            userprofile.user_nickname = user.DisplayName;
            userprofile.phone_number = user.PhoneNumber;
            userprofile.gender = user.Gender;
            userprofile.Email_addr = user.Email;
            return userprofile;

        }

        public static ApplicationUser ToApplicationUser(this user_profile userprofile)
        {
            if (userprofile == null) return null;
            ApplicationUser user = new ApplicationUser();
            //var userprofile = new user_profile();
            //userprofile.ID = UserQuery.GetCount("where 1=1") + 1;
            user.Id = userprofile.user_id; ;
            user.Birthday = userprofile.birthday; ;
            user.UserName = userprofile.user_name; ;
            user.DisplayName = userprofile.user_nickname; ;
            user.PhoneNumber = userprofile.phone_number; ;
            user.Gender = userprofile.gender; ;
            user.Email = userprofile.Email_addr; ;
            return user;
        }

    }
    public enum Position
    {
        Horizontal,
        Vertical
    } 
    public static class CheckBoxListExtensions
    {
        #region 水平方向
        /// <summary>
        /// CheckBoxList
        /// </summary>
        /// <param name="htmlHelper">HTML helper</param>
        /// <param name="name"></param>
        /// <param name="listInfo"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="number">每行显式的个数</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper,
            string name,
            IEnumerable<SelectListItem> listInfo,
            IDictionary<string, object> htmlAttributes,
            int number)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("必须给CheckBoxList一个name值", "name");
            }
            if (listInfo == null)
            {
                throw new ArgumentNullException("listInfo", "List<SelectListItem> listInfo不能为null");
            }

            var selectListItems = listInfo as SelectListItem[] ?? listInfo.ToArray();
            if (!selectListItems.Any())
            {
                throw new ArgumentException("List<SelectListItem> listInfo 至少有一组资料", "listInfo");
            }

            var sb = new StringBuilder();
            var lineNumber = 0;
            foreach (var info in selectListItems)
            {
                lineNumber++;
                var builder = new TagBuilder("input");
                if (info.Selected)
                {
                    builder.MergeAttribute("checked", "checked");
                }
                builder.MergeAttributes(htmlAttributes);
                builder.MergeAttribute("type", "checkbox");
                builder.MergeAttribute("value", info.Value);
                builder.MergeAttribute("name", name);
                builder.MergeAttribute("id", string.Format("{0}_{1}", name, info.Value));
                sb.Append(builder.ToString(TagRenderMode.Normal));

                var labelBuilder = new TagBuilder("label");
                labelBuilder.MergeAttribute("for", string.Format("{0}_{1}", name, info.Value));
                labelBuilder.InnerHtml = info.Text;
                sb.Append(labelBuilder.ToString(TagRenderMode.Normal));

                if (number == 0 || (lineNumber % number == 0))
                {
                    sb.Append("<br />");
                }
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper,
            string name,
            IEnumerable<SelectListItem> listInfo)
        {
            return htmlHelper.CheckBoxList(name, listInfo, (IDictionary<string, object>)null, 0);
        }
        #endregion

        #region 垂直方向

        public static MvcHtmlString CheckBoxListVertical(this HtmlHelper htmlHelper,
            string name,
            IEnumerable<SelectListItem> listInfo,
            IDictionary<string, object> htmlAttributes,
            int columnNumber = 1)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("必须给CheckBoxList的name属性赋值", "name");
            }
            if (listInfo == null)
            {
                throw new ArgumentNullException("listInfo", "List<SelectListInfo> listInfo不能为null");
            }
 
            var selectListItmes = listInfo as SelectListItem[] ?? listInfo.ToArray();
            if (!selectListItmes.Any())
            {
                throw new ArgumentException("List<SelectListItem> listInfo不能为null","listInfo");
            }
 
            var dataCount = selectListItmes.Count();
            var rows = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(dataCount)/Convert.ToDecimal(columnNumber)));
            if (dataCount <= columnNumber || dataCount - columnNumber == 1)
            {
                rows = dataCount;
            }
 
            var wrapBuilder = new TagBuilder("div");
            wrapBuilder.MergeAttribute("style","float:left; line-height:25px; padding-right:5px;");
            var wrapStart = wrapBuilder.ToString(TagRenderMode.StartTag);
               var wrapClose = string.Concat(wrapBuilder.ToString(TagRenderMode.EndTag),
                " <div style=\"clear:both;\"></div>");
            var wrapBreak = string.Concat("</div>", wrapBuilder.ToString(TagRenderMode.StartTag));
            var sb = new StringBuilder();
            sb.Append(wrapStart);
 
            var lineNumber = 0;
            foreach (var info in selectListItmes)
            {
                var builder = new TagBuilder("input");
                if (info.Selected)
                {
                    builder.MergeAttribute("checked","checked");
                }
                builder.MergeAttributes(htmlAttributes);
                builder.MergeAttribute("type","checkbox");
                builder.MergeAttribute("value", info.Value);
                builder.MergeAttribute("name", name);
                builder.MergeAttribute("id", string.Format("{0}_{1}",name, info.Value));
                sb.Append(builder.ToString(TagRenderMode.Normal));
 
                var labelBuilder = new TagBuilder("label");
                labelBuilder.MergeAttribute("for", string.Format("{0}_{1}", name, info.Value));
                labelBuilder.InnerHtml = info.Text;
                sb.Append(labelBuilder.ToString(TagRenderMode.Normal));
 
                lineNumber++;
 
                if (lineNumber.Equals(rows))
                {
                    sb.Append(wrapBreak);
                    lineNumber = 0;
                }
                else
                {
                    sb.Append("<br />");
                }
            }
            sb.Append(wrapClose);
            return MvcHtmlString.Create(sb.ToString());
        }
        #endregion

        #region 水平或垂直方向

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper,
            string name,
            IEnumerable<SelectListItem> listInfo,
            IDictionary<string, object> htmlAttributes,
            Position position = Position.Horizontal,
            int number = 0)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("必须给CheckBoxList的name属性赋值","name");
            }
            if (listInfo == null)
            {
                throw new ArgumentNullException("listInfo", "List<SelectListItem> listInfo不能为null");
            }
            var selectListItems = listInfo as SelectListItem[] ?? listInfo.ToArray();
            if (!selectListItems.Any())
            {
                throw new ArgumentException("List<SelectListItem> listInfo至少要一组资料","listInfo");
            }
 
            var sb = new StringBuilder();
            var lineNumber = 0;
            switch (position)
            {
                case Position.Horizontal:
                    foreach (var info in selectListItems)
                    {
                        lineNumber++;
                        sb.Append(CreateCheckBoxItem(info, name, htmlAttributes));
 
                        if (number == 0 || (lineNumber%number == 0))
                        {
                            sb.Append("<br />");
                        }
                    }
                    sb.Append("<br />");
                    break;
                case Position.Vertical:
                    var dataCount = selectListItems.Count();
                    var rows = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(dataCount)/Convert.ToDecimal(number)));
                    if (dataCount <= number || dataCount - number == 1)
                    {
                        rows = dataCount;
                    }
 
                    var wrapBuilder = new TagBuilder("div");
                    wrapBuilder.MergeAttribute("style","float:left; line-height:25px; padding-right:5px;");
                    var wrapStart = wrapBuilder.ToString(TagRenderMode.StartTag);
                    var wrapClose = string.Concat(wrapBuilder.ToString(TagRenderMode.EndTag), " <div style=\"clear:both;\"></div>");
                    var wrapBreak = string.Concat("</div>", wrapBuilder.ToString(TagRenderMode.StartTag));
                    sb.Append(wrapStart);
 
                    foreach (var info in selectListItems)
                    {
                        lineNumber++;
                        sb.Append(CreateCheckBoxItem(info, name, htmlAttributes));
 
                        if (lineNumber.Equals(rows))
                        {
                            sb.Append(wrapBreak);
                            lineNumber = 0;
                        }
                        else
                        {
                            sb.Append(wrapClose);
                        }
                    }
                    sb.Append(wrapClose);
                    break;
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        internal static string CreateCheckBoxItem(SelectListItem info, string name,
            IDictionary<string, object> htmlAttributes)
        {
            var sb = new StringBuilder();
            var builder = new TagBuilder("input");
            if (info.Selected)
            {
                builder.MergeAttribute("checked", "checked");
            }
            builder.MergeAttributes(htmlAttributes);
            builder.MergeAttribute("type", "checkbox");
            builder.MergeAttribute("value", info.Value);
            builder.MergeAttribute("name", name);
            builder.MergeAttribute("id", string.Format("{0}_{1}", name, info.Value));
            sb.Append(builder.ToString(TagRenderMode.Normal));

            var labelBuilder = new TagBuilder("label");
            labelBuilder.MergeAttribute("for", string.Format("{0}_{1}", name, info.Value));
            labelBuilder.InnerHtml = info.Text;
            sb.Append(labelBuilder.ToString(TagRenderMode.Normal));

            return sb.ToString();
        }
        #endregion
    }
   
}

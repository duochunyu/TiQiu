using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace TiQiu.Web.WebPages.Utilities
{
    public static class HtmlHelper
    {
        public static string RadioButton<TModel>(TModel model, Predicate<TModel> isChecked, string htmlAttributes)
        {
            return string.Format("<input {0} type='radio' value={1} {2} />", htmlAttributes ??
                string.Empty, model, isChecked(model) ? "checked='checked'" : string.Empty);
        }
        public static string Options<TModel>(IEnumerable<TModel> source,
            Func<TModel, string> key,
            Func<TModel, string> value,
            string selectedValue = null)
        {
            if (source == null || !source.Any())
                return string.Empty;


            selectedValue = selectedValue ?? key(source.First());

            StringBuilder dpStr = new StringBuilder();
            foreach (var item in source)
            {
                dpStr.AppendFormat("<option title='" + value(item) + "' value='{0}' {2}>{1}</option>{3}", key(item), value(item), selectedValue == key(item) ?
                    "selected='selected'" : "",Environment.NewLine);
            }
            return dpStr.ToString();
        }
        public static string DropDownList<TModel>(IEnumerable<TModel> source,
            Func<TModel, string> key,
            Func<TModel, string> value,
            string selectedValue = null,
            string htmlAttributes = null)
        {
            StringBuilder dpStr = new StringBuilder();
            dpStr.AppendFormat("<select {0} >{1}", htmlAttributes ?? string.Empty, Environment.NewLine);
            dpStr.AppendLine(Options(source, key, value, selectedValue));
            return dpStr.AppendFormat("</select>").ToString();
        }
        #region Repeater
        public static void Repeater<TModel>(IEnumerable<TModel> source, Action<TModel, int> render, Action emptyTemp)
        {
            if (emptyTemp != null && (source == null || source.Count() == 0))
            {
                emptyTemp();
            }
            else
            {
                Repeater<TModel>(source, render);
            }
        }
        public static void Repeater<TModel>(IEnumerable<TModel> source, Action<TModel, int> render)
        {
            if (source == null)
                return;
            var indexSource = source.Select((s, n) => new { s, n });
            foreach (var item in indexSource)
            {
                render(item.s, item.n);
            }
        }
        public static void Repeater<TModel>(IEnumerable<TModel> source, Action<TModel> render)
        {
            if (source == null)
                return;
            foreach (var item in source)
            {
                render(item);
            }
        }
        public static void Repeater<TModel>(IEnumerable<TModel> source, Action<TModel> render, Action emptyTemp)
        {
            if (emptyTemp != null && (source == null || source.Count() == 0))
            {
                emptyTemp();
            }
            else
            {
                foreach (var item in source)
                {
                    render(item);
                }
            }
        }
        #endregion

        #region Paging
        public static string AjaxPaging(string urlFormat, int pageIndex, int pageSize, int listSum)
        {
            int pageCount = (int)Math.Ceiling(listSum / (double)pageSize);
            if (pageCount <= 1)
            {
                return string.Empty;
            }
            StringBuilder strNav = new StringBuilder();
            int divPageNum = 0;

            if (pageIndex > 1) strNav.Append("<a href='" + string.Format(urlFormat, pageIndex - 1) + "' onclick='ajaxpaging(" + (pageIndex - 1) + ");return false;'>&laquo;上一页</a>&nbsp;&nbsp;");
            if (pageIndex > 6) strNav.Append("<a href='" + string.Format(urlFormat, 1) + "' onclick='ajaxpaging(1);return false;'>1</a>...");
            if (pageCount > pageIndex + 5)
                divPageNum = pageIndex + 5;
            else
                divPageNum = pageCount;

            for (int i = pageIndex - 6; i < divPageNum; i++)
            {
                if (i >= 0)
                {
                    if (pageIndex == i + 1)
                        strNav.Append("<span class=\"current\">" + (i + 1).ToString() + "</span>");
                    else
                        strNav.Append(" <a href='" + string.Format(urlFormat, i + 1) + "' onclick='ajaxpaging(" + (i + 1) + ");return false;'>" + (i + 1) + "</a> ");
                }
            }
            if (pageIndex + 5 < pageCount) strNav.Append("...&nbsp;&nbsp;<a href='" + string.Format(urlFormat, pageCount) + "' onclick='ajaxpaging(" + pageCount + ");return false;'>" + pageCount + "</a>");

            if (pageIndex != pageCount) strNav.Append("&nbsp;&nbsp;<a href='" + string.Format(urlFormat, pageIndex + 1) + "' onclick='ajaxpaging(" + (pageIndex + 1) + ");return false;'>下一页&raquo;</a>&nbsp;");

            return strNav.ToString();
        }

        public static string AjaxPaging(int pageIndex,
            int pageSize,
            int listSum,
            string ajaxpagingName = "ajaxpaging")
        {
            int pageCount = (int)Math.Ceiling(listSum / (double)pageSize);
            if (pageCount <= 1)
            {
                return string.Empty;
            }
            StringBuilder strNav = new StringBuilder();
            int divPageNum = 0;

            if (pageIndex > 1) strNav.Append("<a href='javascript:void(0)' onclick='" + ajaxpagingName + "(" + (pageIndex - 1) + ")'>&laquo;上一页</a>&nbsp;&nbsp;");
            if (pageIndex > 4) strNav.Append("<a href='javascript:void(0)' onclick='" + ajaxpagingName + "(1)'>1</a>...");
            if (pageCount > pageIndex + 3)
                divPageNum = pageIndex + 3;
            else
                divPageNum = pageCount;

            for (int i = pageIndex - 4; i < divPageNum; i++)
            {
                if (i >= 0)
                {
                    if (pageIndex == i + 1)
                        strNav.Append("<span class=\"current\">" + (i + 1).ToString() + "</span>");
                    else
                        strNav.Append(" <a href='javascript:void(0)' onclick='" + ajaxpagingName + "(" + (i + 1) + ")'>" + (i + 1) + "</a> ");
                }
            }
            if (pageIndex + 3 < pageCount) strNav.Append("...&nbsp;&nbsp;<a href='javascript:void(0)' onclick='" + ajaxpagingName + "(" + pageCount + ")'>" + pageCount + "</a>");

            if (pageIndex != pageCount) strNav.Append("&nbsp;&nbsp;<a href='javascript:void(0)' onclick='" + ajaxpagingName + "(" + (pageIndex + 1) + ")'>下一页&raquo;</a>&nbsp;");

            return strNav.ToString();
        }

        public static string PagingLite(string pagingUrlFormat, int pageIndex, int pageSize, int listSum)
        {
            return PagingLite(null, pagingUrlFormat, pageIndex, pageSize, listSum);
        }
        public static string PagingLite(string firsPageUrl, string pagingUrlFormat, int pageIndex, int pageSize, int listSum)
        {
            int pageCount = (int)Math.Ceiling(listSum / (double)pageSize);
            if (pageCount <= 1)
            {
                return string.Empty;
            }
            StringBuilder strNav = new StringBuilder();
            if (pageIndex > 1)
            {
                strNav.Append("&nbsp;<a href='" + (pageIndex - 1 == 1 && !string.IsNullOrEmpty(firsPageUrl) ? firsPageUrl : string.Format(pagingUrlFormat, (pageIndex - 1))) + "'>上一页</a>");
            }
            else if (pageIndex == 1)
            {
                strNav.Append("&nbsp;<span>上一页<span>");
            }
            strNav.Append(string.Format("&nbsp;{0}/{1}", pageIndex, pageCount));
            if (pageIndex < pageCount)
            {
                strNav.Append("&nbsp;<a href='" + string.Format(pagingUrlFormat, (pageIndex + 1)) + "'>下一页</a>");
            }
            else if (pageIndex == pageCount)
            {
                strNav.Append("&nbsp;<span>下一页<span>");
            }
            return strNav.ToString();
        }

        public static string Paging(string pagingUrlFormat, int pageIndex, int pageSize, int listSum)
        {
            return Paging(6, null, pagingUrlFormat, pageIndex, pageSize, listSum);
        }
        public static string Paging(int pagingFactor, string firsPageUrl, string pagingUrlFormat, int pageIndex, int pageSize, int listSum)
        {
            int factor = pagingFactor;
            int pageCount = (int)Math.Ceiling(listSum / (double)pageSize);
            if (pageCount <= 1)
            {
                return string.Empty;
            }
            StringBuilder strNav = new StringBuilder();
            int divPageNum = 0;

            if (pageIndex > 1) strNav.Append("<a href='" + (pageIndex - 1 == 1 && !string.IsNullOrEmpty(firsPageUrl) ? firsPageUrl : string.Format(pagingUrlFormat, (pageIndex - 1))) + "'>&laquo;上一页</a>");
            if (pageIndex > factor) strNav.Append("<a href='" + string.Format(pagingUrlFormat, 1) + "'>1</a>&nbsp;&nbsp;&nbsp;...&nbsp;");
            if (pageCount > pageIndex + (factor - 1))
                divPageNum = pageIndex + (factor - 1);
            else
                divPageNum = pageCount;

            for (int i = pageIndex - factor; i < divPageNum; i++)
            {
                if (i >= 0)
                {
                    if (pageIndex == i + 1)
                        strNav.Append("<span class=\"current\">" + (i + 1).ToString() + "</span>");
                    else
                        strNav.Append(" <a href='" + (i + 1 == 1 && !string.IsNullOrEmpty(firsPageUrl) ? firsPageUrl : string.Format(pagingUrlFormat, (i + 1))) + "'>" + (i + 1) + "</a> ");
                }
            }
            if (pageIndex + (factor - 1) < pageCount) strNav.Append("&nbsp;&nbsp;...&nbsp;&nbsp;<a href='" + string.Format(pagingUrlFormat, pageCount) + "'>" + pageCount + "</a>");

            if (pageIndex != pageCount) strNav.Append("<a href='" + string.Format(pagingUrlFormat, (pageIndex + 1)) + "'>下一页&raquo;</a>");

            return strNav.ToString();
        }
        #endregion

    }
}
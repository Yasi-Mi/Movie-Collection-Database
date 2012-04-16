using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace DDW.BodyworkBuddy.Integration.Web.HtmlHelpers
{
    public static class HtmlHelperExtension
    {

        public static MvcHtmlString DecimalBoxFor<TModel,TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string format, object htmlAttributes = null)
        {
            var name = ExpressionHelper.GetExpressionText(expression);

            string value = "";
            if (ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model != null)
                value = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model.ToString();

            decimal dec;
            if (decimal.TryParse(value, out dec))
            {
                // Here you can format value as you wish
                value = (!string.IsNullOrEmpty(format) ? dec.ToString(format) : value);
            }
          

            return helper.TextBox(name, value, htmlAttributes);    
        }

        /// <summary>
        /// Adds a class name to HTML attributes.
        /// </summary>
        /// <param name="startDateHtmlAttributes">The start date HTML attributes.</param>
        /// <param name="className">Name of the class.</param>
        public static void AddClassToHtmlAttributes(ref IDictionary<string, object> startDateHtmlAttributes,
                                                                        string className)
        {
            if (!String.IsNullOrEmpty(className))
            {
                if (startDateHtmlAttributes == null)
                    startDateHtmlAttributes = new RouteValueDictionary();

                if (startDateHtmlAttributes["class"] != null)
                    startDateHtmlAttributes["class"] = string.Format("{0} {1}", startDateHtmlAttributes["class"], className);
                else
                    startDateHtmlAttributes.Add("class", className);    
            }
            

        }


        

        #region Would love to use this
        public static MvcHtmlString HelpIconFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string customText = null)
        {
            if (customText == null)
                return new MvcHtmlString(String.Empty);

            // Can do all sorts of things here -- eg: reflect over attributes and add hints, etc...
            //<img src="/Content/Broom/Images/icons/question_blue.png" alt="Help" title="Choose the Client for this session by pressing the 'Select Client' button."  />
            var helperImage = new TagBuilder("img");
            helperImage.Attributes.Add("alt", "Help");
            helperImage.Attributes.Add("title", customText);

            return new MvcHtmlString(helperImage.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString LabelWithHelpIconFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string labelText, string helpText = null)
        {
            return helper.LabelFor(expression, labelText);
        }
        #endregion

        #region Not really used
        public static MvcHtmlString FormLineEditorFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string templateName = null, string labelText = null, string customHelpText = null, object htmlAttributes = null)
        {
            return FormLine(
                helper.LabelFor(expression, labelText).ToString() +
                helper.HelpIconFor(expression, customHelpText),
                helper.EditorFor(expression, templateName, htmlAttributes).ToString() +
                helper.ValidationMessageFor(expression));
        }


        // http://stackoverflow.com/questions/4804833/asp-net-mvc-3-custom-html-helpers-best-practices-uses
        private static MvcHtmlString FormLine(string labelContent, string fieldContent, object htmlAttributes = null)
        {
            var editorLabel = new TagBuilder("div");
            editorLabel.AddCssClass("editor-label");
            editorLabel.InnerHtml += labelContent;

            var editorField = new TagBuilder("div");
            editorField.AddCssClass("editor-field");
            editorField.InnerHtml += fieldContent;

            var container = new TagBuilder("div");
            if (htmlAttributes != null)
                container.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            container.AddCssClass("form-line");
            container.InnerHtml += editorLabel;
            container.InnerHtml += editorField;

            return MvcHtmlString.Create(container.ToString());
        }



        public static MvcHtmlString FormLineDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string labelText = null, string customHelpText = null, object htmlAttributes = null)
        {
            return FormLine(
                helper.LabelFor(expression, labelText).ToString() +
                helper.HelpIconFor(expression, customHelpText),
                helper.DropDownListFor(expression, selectList, htmlAttributes).ToString() +
                helper.ValidationMessageFor(expression));
        }
        #endregion
        





    }
}
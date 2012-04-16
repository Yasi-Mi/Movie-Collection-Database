using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace DDW.BodyworkBuddy.Integration.Web.HtmlHelpers
{
    public static class DateTimeHtmlHelperExtension
    {
        #region Date Picker
        public static MvcHtmlString DatePickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return helper.DatePickerFor(expression, null);
        }

        public static MvcHtmlString DatePickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return helper.DatePickerFor(expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString DatePickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var value = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;

            return helper.DatePicker(name, value, htmlAttributes);
        }

        public static MvcHtmlString DatePicker(this HtmlHelper helper, string name, object value)
        {
            return helper.DatePicker(name, value, null);
        }

        public static MvcHtmlString DatePicker(this HtmlHelper helper, string name, object value, object htmlAttributes)
        {
            return helper.DatePicker(name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString DatePicker(this HtmlHelper helper, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            var fullName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            var dateInputTagBuilder = new TagBuilder("input");

            dateInputTagBuilder.MergeAttributes(htmlAttributes);
            dateInputTagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            dateInputTagBuilder.MergeAttribute("name", fullName, true);
            dateInputTagBuilder.MergeAttribute("value", String.Format("{0:MM/dd/yyyy}", value));
            dateInputTagBuilder.AddCssClass("datepicker");
            dateInputTagBuilder.AddCssClass("datetime");
            dateInputTagBuilder.GenerateId(fullName);
            var dateID = dateInputTagBuilder.Attributes["id"];

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(dateInputTagBuilder.ToString(TagRenderMode.SelfClosing));

            // add the script to initialize this date picker
            stringBuilder.AppendLine("<script type='text/javascript'>");
            stringBuilder.AppendLine("   $(document).ready(function() {");
            stringBuilder.AppendLine(string.Format("      $(\"#{0}\").mask(\"99/99/9999\");", dateID));
            stringBuilder.AppendLine("   });");
            stringBuilder.AppendLine("</script>");

            return new MvcHtmlString(stringBuilder.ToString());
        }
        #endregion


        #region DateRange Picker
        // DateRangePicker__



        public static MvcHtmlString DateRangePickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> startDateExpression, Expression<Func<TModel, TProperty>> endDateExpression)
        {
            return helper.DateRangePickerFor(startDateExpression, endDateExpression,
                                                null,
                                                null);
        }


        public static MvcHtmlString DateRangePickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> startDateExpression, Expression<Func<TModel, TProperty>> endDateExpression, object startDateHtmlAttributes, object endDateHtmlAttributes)
        {
            return helper.DateRangePickerFor(startDateExpression, endDateExpression,
                                                HtmlHelper.AnonymousObjectToHtmlAttributes(startDateHtmlAttributes),
                                                HtmlHelper.AnonymousObjectToHtmlAttributes(endDateHtmlAttributes));
        }



        public static MvcHtmlString DateRangePickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> startDateExpression, Expression<Func<TModel, TProperty>> endDateExpression, IDictionary<string, object> startDateHtmlAttributes, IDictionary<string, object> endDateHtmlAttributes)
        {
            var startDateName = ExpressionHelper.GetExpressionText(startDateExpression);
            var startDateValue = ModelMetadata.FromLambdaExpression(startDateExpression, helper.ViewData).Model;

            var endDateName = ExpressionHelper.GetExpressionText(endDateExpression);
            var endDateValue = ModelMetadata.FromLambdaExpression(endDateExpression, helper.ViewData).Model;

            return helper.DateRangePicker(startDateName, startDateValue, endDateName, endDateValue, startDateHtmlAttributes, endDateHtmlAttributes);
        }

        public static MvcHtmlString DateRangePicker(this HtmlHelper helper, string startDateName, object startDateValue, string endDateName, object endDateValue, object startDateHtmlAttributes, object endDateHtmlAttributes)
        {
            return helper.DateRangePicker(startDateName, startDateValue, endDateName, endDateValue, HtmlHelper.AnonymousObjectToHtmlAttributes(startDateHtmlAttributes), HtmlHelper.AnonymousObjectToHtmlAttributes(endDateHtmlAttributes));
        }

        public static MvcHtmlString DateRangePicker(this HtmlHelper helper, string startDateName, object startDateValue, string endDateName, object endDateValue)
        {
            return helper.DateRangePicker(startDateName, startDateValue, endDateName, endDateValue, null, null);
        }

        public static MvcHtmlString DateRangePicker(this HtmlHelper helper,
            string startDateName, object startDateValue,
            string endDateName, object endDateValue,
            IDictionary<string, object> startDateHtmlAttributes,
            IDictionary<string, object> endDateHtmlAttributes)
        {
            //var dateRangeClassName = "dateRange" + DateTime.Now.ToBinary();
            //HtmlHelperExtension.AddClassToHtmlAttributes(ref startDateHtmlAttributes, dateRangeClassName);
            //HtmlHelperExtension.AddClassToHtmlAttributes(ref endDateHtmlAttributes, dateRangeClassName);

            // define the labels
            var startDateLabelTagBuilder = new TagBuilder("label") { InnerHtml = "Start Date:" };
            var endDateLabelTagBuilder = new TagBuilder("label") { InnerHtml = "End Date:" };

            // create the date picker controls
            var startDatePicker = helper.TextBox(startDateName, startDateValue, startDateHtmlAttributes);
            var endDatePicker = helper.TextBox(endDateName, endDateValue, endDateHtmlAttributes);

            //var startDatePicker = helper.DatePicker(startDateName, startDateValue, startDateHtmlAttributes);
            //var endDatePicker = helper.DatePicker(endDateName, endDateValue, endDateHtmlAttributes);

            var sb = new StringBuilder();

            // put the two controls together
            sb.Append(startDateLabelTagBuilder.ToString(TagRenderMode.Normal));
            sb.Append(startDatePicker);
            sb.Append(endDateLabelTagBuilder.ToString(TagRenderMode.Normal));
            sb.Append(endDatePicker);

            // javascript
            sb.AppendLine("<script type='text/javascript'>");
            sb.AppendLine("$(document).ready(function() {");

            sb.AppendLine(string.Format("    $('#{0}, #{1}').daterangepicker({{dateFormat: 'mm/dd/yy'}});", TagBuilder.CreateSanitizedId(startDateName), TagBuilder.CreateSanitizedId(endDateName)));
            //sb.AppendLine("    $('#{0}, #{1}').daterangepicker({dateFormat: 'mm/dd/YYYY', posX: 100, posY: '16.8em'});");


            //sb.AppendLine(string.Format("$.validator.addMethod(\"{0}\", function() {{", dateRangeClassName));
            //sb.AppendLine(string.Format("    var startDate = new Date( $('#{0}').val() );", TagBuilder.CreateSanitizedId(startDateName)));
            //sb.AppendLine(string.Format("    var endDate = new Date( $('#{0}').val() );", TagBuilder.CreateSanitizedId(endDateName)));
            //sb.AppendLine("    if( endDate >= startDate )");
            //sb.AppendLine("        return true;");
            //sb.AppendLine("    return false;");
            //sb.AppendLine("}, \"Please check your times. The start time must be before the end time.\");");

            sb.AppendLine("});");
            sb.AppendLine("</script>");

            return new MvcHtmlString(sb.ToString());
        }


        #endregion

        #region DateTime Picker

        public static MvcHtmlString DateTimePickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return helper.DateTimePickerFor(expression, false, 15, null, null, null);
        }

        /// <summary>
        /// Creates a DateTime Picker Control
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="show24Hours">if set to <c>true</c> [show24 hours].</param>
        /// <param name="timeStep">The time step.</param>
        /// <param name="hiddenHtmlAttributes">The HTML attributes.</param>
        /// <param name="dateHtmlAttributes">The date HTML attributes.</param>
        /// <param name="timeHtmlAttributes">The time HTML attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString DateTimePickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, bool show24Hours, int timeStep, object hiddenHtmlAttributes, object dateHtmlAttributes, object timeHtmlAttributes)
        {
            return helper.DateTimePickerFor(expression, show24Hours, timeStep,
                                            HtmlHelper.AnonymousObjectToHtmlAttributes(hiddenHtmlAttributes),
                                            HtmlHelper.AnonymousObjectToHtmlAttributes(dateHtmlAttributes),
                                            HtmlHelper.AnonymousObjectToHtmlAttributes(timeHtmlAttributes));
        }

        /// <summary>
        /// Creates a DateTime Picker Control
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="show24Hours">if set to <c>true</c> [show24 hours].</param>
        /// <param name="timeStep">The time step.</param>
        /// <param name="hiddenHtmlAttributes">The HTML attributes.</param>
        /// <param name="dateHtmlAttributes">The date HTML attributes.</param>
        /// <param name="timeHtmlAttributes">The time HTML attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString DateTimePickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, bool show24Hours, int timeStep, IDictionary<string, object> hiddenHtmlAttributes, IDictionary<string, object> dateHtmlAttributes, IDictionary<string, object> timeHtmlAttributes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var value = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;

            return helper.DateTimePicker(name, value, show24Hours, timeStep, hiddenHtmlAttributes, dateHtmlAttributes, timeHtmlAttributes);
        }

        /// <summary>
        /// Creates a DateTime Picker Control
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static MvcHtmlString DateTimePicker(this HtmlHelper helper, string name, object value)
        {
            return helper.DateTimePicker(name, value, false, 15, null, null, null);
        }

        /// <summary>
        /// Creates a DateTime Picker Control
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="show24Hours">if set to <c>true</c> [show24 hours].</param>
        /// <param name="timeStep">The time step.</param>
        /// <param name="hiddenHtmlAttributes">The HTML attributes.</param>
        /// <param name="dateHtmlAttributes">The date HTML attributes.</param>
        /// <param name="timeHtmlAttributes">The time HTML attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString DateTimePicker(this HtmlHelper helper, string name, object value, bool show24Hours, int timeStep, object hiddenHtmlAttributes, object dateHtmlAttributes, object timeHtmlAttributes)
        {
            return helper.DateTimePicker(name, value, show24Hours, timeStep,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(hiddenHtmlAttributes),
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(dateHtmlAttributes),
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(timeHtmlAttributes));
        }

        /// <summary>
        /// Creates a DateTime Picker Control
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="show24Hours">if set to <c>true</c> [show24 hours].</param>
        /// <param name="timeStep">The time step.</param>
        /// <param name="hiddenHtmlAttributes">The hidden HTML attributes.</param>
        /// <param name="dateHtmlAttributes">The date input HTML attributes.</param>
        /// <param name="timeHtmlAttributes">The time input HTML attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString DateTimePicker(this HtmlHelper helper, string name, object value, bool show24Hours, int timeStep, IDictionary<string, object> hiddenHtmlAttributes, IDictionary<string, object> dateHtmlAttributes, IDictionary<string, object> timeHtmlAttributes, bool ignoreChange = false)
        {
            string fullName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            var dateInputTagBuilder = new TagBuilder("input");

            dateInputTagBuilder.MergeAttributes(dateHtmlAttributes);
            dateInputTagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            dateInputTagBuilder.MergeAttribute("name", fullName + "_Date", true);
            dateInputTagBuilder.MergeAttribute("value", String.Format("{0:MM/dd/yyyy}", value));
            dateInputTagBuilder.AddCssClass("datepicker");
            dateInputTagBuilder.AddCssClass("datetime");
            dateInputTagBuilder.MergeAttribute("style", "display:inline !important;");

            dateInputTagBuilder.GenerateId(fullName + "_Date");
            var dateID = dateInputTagBuilder.Attributes["id"];

            var timeInputTagBuilder = new TagBuilder("input");
            timeInputTagBuilder.MergeAttributes(timeHtmlAttributes);
            timeInputTagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            timeInputTagBuilder.MergeAttribute("name", fullName + "_Time", true);
            timeInputTagBuilder.MergeAttribute("value", String.Format("{0:hh:mm tt}", value));
            timeInputTagBuilder.MergeAttribute("style", "display:inline !important;");
            timeInputTagBuilder.AddCssClass("timepicker");
            timeInputTagBuilder.GenerateId(fullName + "_Time");
            var timeID = timeInputTagBuilder.Attributes["id"];

            var hiddenInputTagBuilder = new TagBuilder("input");
            hiddenInputTagBuilder.MergeAttributes(hiddenHtmlAttributes);
            hiddenInputTagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Hidden));
            hiddenInputTagBuilder.MergeAttribute("name", fullName, true);
            hiddenInputTagBuilder.MergeAttribute("value", String.Format("{0:MM/dd/yyyy hh:mm tt}", value));
            hiddenInputTagBuilder.GenerateId(fullName);
            var dateTimeID = hiddenInputTagBuilder.Attributes["id"];

            var sb = new StringBuilder();
            sb.AppendLine(dateInputTagBuilder.ToString(TagRenderMode.SelfClosing));
            sb.AppendLine(timeInputTagBuilder.ToString(TagRenderMode.SelfClosing));
            sb.AppendLine(hiddenInputTagBuilder.ToString(TagRenderMode.SelfClosing));

            // add the script to initialize this date time picker
            sb.AppendLine("<script type='text/javascript'>");
            sb.AppendLine("$(document).ready(function() {");

            // initialize the timepicker for the time input
            sb.AppendLine("$(\"#" + timeID + "\").timePicker({");
            sb.AppendLine(String.Format("  show24Hours: {0},", show24Hours.ToString().ToLower()));
            sb.AppendLine(String.Format("  step: {0}", timeStep));
            sb.AppendLine("});");

            //$(".datetime-pick").mask("99/99/9999 99:99 ~M");
            // mask the date and time fields
            sb.AppendLine("$(\"#" + dateID + "\").mask(\"99/99/9999\");");
            sb.AppendLine("$(\"#" + timeID + "\").mask(\"99:99 ~M\");");


            if (!ignoreChange)
            {
                // on change of date, update the hidden field
                sb.AppendLine("   $(\"#" + dateID + "\").change(function() {");
                sb.AppendLine("      $(\"#" + dateTimeID + "\").val($(\"#" + dateID + "\").val() + \" \" + $(\"#" + timeID + "\").val());");
                sb.AppendLine("   });");

                // on change of time, update the hidden field
                sb.AppendLine("   $(\"#" + timeID + "\").change(function() {");
                sb.AppendLine("      $(\"#" + dateTimeID + "\").val($(\"#" + dateID + "\").val() + \" \" + $(\"#" + timeID + "\").val());");
                sb.AppendLine("   });");
            }



            sb.AppendLine("});");
            sb.AppendLine("</script>");

            return new MvcHtmlString(sb.ToString());
        }
        #endregion

        #region DateTime Range Picker

        // DateTimeRangePicker
        public static MvcHtmlString DateTimeRangePickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> startDateExpression, Expression<Func<TModel, TProperty>> endDateExpression)
        {
            return helper.DateTimeRangePickerFor(startDateExpression, endDateExpression, false, 15,
                                                 null, null, null, null, null, null);
        }

        public static MvcHtmlString DateTimeRangePickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> startDateExpression, Expression<Func<TModel, TProperty>> endDateExpression, bool show24Hours, int timeStep, object startHiddenHtmlAttributes, object startDateHtmlAttributes, object startTimeHtmlAttributes, object endHiddenHtmlAttributes, object endDateHtmlAttributes, object endTimeHtmlAttributes)
        {
            return helper.DateTimeRangePickerFor(startDateExpression, endDateExpression, show24Hours, timeStep,
                                                 HtmlHelper.AnonymousObjectToHtmlAttributes(startHiddenHtmlAttributes),
                                                 HtmlHelper.AnonymousObjectToHtmlAttributes(startDateHtmlAttributes),
                                                 HtmlHelper.AnonymousObjectToHtmlAttributes(startTimeHtmlAttributes),
                                                 HtmlHelper.AnonymousObjectToHtmlAttributes(endHiddenHtmlAttributes),
                                                 HtmlHelper.AnonymousObjectToHtmlAttributes(endDateHtmlAttributes),
                                                 HtmlHelper.AnonymousObjectToHtmlAttributes(endTimeHtmlAttributes));
        }

        public static MvcHtmlString DateTimeRangePickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> startDateExpression, Expression<Func<TModel, TProperty>> endDateExpression, bool show24Hours, int timeStep, IDictionary<string, object> startHiddenHtmlAttributes, IDictionary<string, object> startDateHtmlAttributes, IDictionary<string, object> startTimeHtmlAttributes, IDictionary<string, object> endHiddenHtmlAttributes, IDictionary<string, object> endDateHtmlAttributes, IDictionary<string, object> endTimeHtmlAttributes)
        {
            var startDateName = ExpressionHelper.GetExpressionText(startDateExpression);
            var startDateValue = ModelMetadata.FromLambdaExpression(startDateExpression, helper.ViewData).Model;

            var endDateName = ExpressionHelper.GetExpressionText(endDateExpression);
            var endDateValue = ModelMetadata.FromLambdaExpression(endDateExpression, helper.ViewData).Model;


            //var dateRangeClassName = "dateRange" + DateTime.Now.ToBinary();
            var endDateRangeClassName = "endDateTimeRange" + DateTime.Now.ToBinary();

            // start daterange class modifications
            //HtmlHelperExtension.AddClassToHtmlAttributes(ref startHiddenHtmlAttributes, dateRangeClassName);
            //HtmlHelperExtension.AddClassToHtmlAttributes(ref startDateHtmlAttributes, dateRangeClassName);
            //HtmlHelperExtension.AddClassToHtmlAttributes(ref startTimeHtmlAttributes, dateRangeClassName);

            // end daterange class modifications
            //HtmlHelperExtension.AddClassToHtmlAttributes(ref endHiddenHtmlAttributes, dateRangeClassName);
            HtmlHelperExtension.AddClassToHtmlAttributes(ref endDateHtmlAttributes, endDateRangeClassName);
            HtmlHelperExtension.AddClassToHtmlAttributes(ref endTimeHtmlAttributes, endDateRangeClassName);

            var startDateTimePicker = helper.DateTimePicker(startDateName, startDateValue, show24Hours, timeStep, startHiddenHtmlAttributes, startDateHtmlAttributes, startTimeHtmlAttributes, true);
            var endDateTimePicker = helper.DateTimePicker(endDateName, endDateValue, show24Hours, timeStep, endHiddenHtmlAttributes, endDateHtmlAttributes, endTimeHtmlAttributes, true);

            //<label for="Session_EventList_0__StartDate">*Start and end time:</label>
            var startDateLabelTagBuilder = new TagBuilder("label");
            startDateLabelTagBuilder.InnerHtml = "Start Date and Time:";

            var endDateLabelTagBuilder = new TagBuilder("label");
            endDateLabelTagBuilder.InnerHtml = "End Date and Time:";

            var sb = new StringBuilder();
            // put the two controls together
            sb.Append(startDateLabelTagBuilder.ToString(TagRenderMode.Normal));
            sb.Append(startDateTimePicker);
            sb.Append(endDateLabelTagBuilder.ToString(TagRenderMode.Normal));
            sb.Append(endDateTimePicker);


            var startDateID = TagBuilder.CreateSanitizedId(startDateName) + "_Date";
            var startTimeID = TagBuilder.CreateSanitizedId(startDateName) + "_Time";
            var startDateTimeID = TagBuilder.CreateSanitizedId(startDateName);

            var endDateID = TagBuilder.CreateSanitizedId(endDateName) + "_Date";
            var endTimeID = TagBuilder.CreateSanitizedId(endDateName) + "_Time";
            var endDateTimeID = TagBuilder.CreateSanitizedId(endDateName);

            // javascript
            sb.AppendLine("<script type='text/javascript'>");
            
            // start document ready 
            sb.AppendLine("$(document).ready(function() {");

            // Store time used by duration.
            sb.AppendLine(string.Format("   var oldStartDateTime = new Date( $('#{0}').val() );", startDateTimeID));

            sb.AppendLine(string.Format("$.validator.addMethod(\"{0}\", function() {{", endDateRangeClassName));

            //sb.AppendLine("    if($.timePicker('#" + startTimeID + "').getTime() > $.timePicker(this).getTime()) {");
            //sb.AppendLine("      return false;");
            //sb.AppendLine("    }");
            //sb.AppendLine("    return true;");
            //sb.AppendLine("}, \"Please check your times. The start time must be before the end time.\");");

            sb.AppendLine(string.Format("    var startDate = new Date( $('#{0}').val() );", startDateTimeID));
            sb.AppendLine(string.Format("    var endDate = new Date( $('#{0}').val() );", endDateTimeID));
            sb.AppendLine("    if( endDate >= startDate )");
            sb.AppendLine("        return true;");
            sb.AppendLine("    return false;");
            sb.AppendLine("}, \"Please check your times. The start time must be before the end time.\");");
            // end validator method

            // on change of start date, update the hidden field
            sb.AppendLine(string.Format("   $('#{0}').change(function() {{", startDateID));
            sb.AppendLine(string.Format("      $('#{0}').val($('#{1}').val() + ' ' + $('#{2}').val());", startDateTimeID, startDateID, startTimeID));

            // validate the end date and time
            sb.AppendLine(string.Format("      $('#{0}').valid();", endDateID));
            sb.AppendLine(string.Format("      $('#{0}').valid();", endTimeID));
            sb.AppendLine("   });");

            // on change of start time, update the hidden field
            sb.AppendLine("   $(\"#" + startTimeID + "\").change(function() {");
            sb.AppendLine(string.Format("      $('#{0}').val($('#{1}').val() + ' ' + $('#{2}').val());", startDateTimeID, startDateID, startTimeID));

            // Keep the duration between the two inputs.
            sb.AppendLine("  if ($('#" + endDateTimeID + "').val()) { "); // Only update when second input has a value.
            // Calculate duration.
            sb.AppendLine(string.Format("    var startDate = new Date( $('#{0}').val() );", startDateTimeID));
            sb.AppendLine(string.Format("    var endDate = new Date( $('#{0}').val() );", endDateTimeID));
            sb.AppendLine("    var duration = endDate.getTime() - oldStartDateTime.getTime();");
            //sb.AppendLine("    var time = $.timePicker('#time3').getTime();");
            // Calculate and update the time in the second input.
            sb.AppendLine("    $('#" + endDateTimeID + "').val(new Date(startDate.getTime() + duration));");
            sb.AppendLine("    $('#" + endDateID + "').val($.fullCalendar.formatDate( new Date(startDate.getTime() + duration), 'MM/dd/yyyy'));");
            sb.AppendLine("    $('#" + endTimeID + "').val($.fullCalendar.formatDate( new Date(startDate.getTime() + duration), 'hh:mm TT'));");

            sb.AppendLine("    oldStartDateTime = startDate;");
            sb.AppendLine("  }");
            
            
            sb.AppendLine(string.Format("      $('#{0}').valid();", endDateID));
            sb.AppendLine(string.Format("      $('#{0}').valid();", endTimeID));

            sb.AppendLine("   });");
            

            // on change of end date, update the hidden field
            sb.AppendLine(string.Format("   $('#{0}').change(function() {{", endDateID));
            sb.AppendLine(string.Format("      $('#{0}').val($('#{1}').val() + ' ' + $('#{2}').val());", endDateTimeID, endDateID, endTimeID));
            sb.AppendLine(string.Format("      $('#{0}').valid();", endDateID));
            sb.AppendLine(string.Format("      $('#{0}').valid();", endTimeID));
            sb.AppendLine("   });");

            // on change of end time, update the hidden field
            sb.AppendLine("$(\"#" + endTimeID + "\").change(function() {");
            sb.AppendLine(string.Format("      $('#{0}').val($('#{1}').val() + ' ' + $('#{2}').val());", endDateTimeID, endDateID, endTimeID));
            sb.AppendLine(string.Format("      $('#{0}').valid();", endDateID));
            sb.AppendLine(string.Format("      $('#{0}').valid();", endTimeID));
            sb.AppendLine("});");

           


            sb.AppendLine("});");
            // end document ready 

            sb.AppendLine("</script>");

            return new MvcHtmlString(sb.ToString());
        }



        #endregion
    }
}
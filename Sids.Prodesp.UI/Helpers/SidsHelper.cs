using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Sids.Prodesp.UI.Helpers
{
    public static class SidsHelper
    {
        public static IHtmlString StatusIcone(string status, string msg, string sistema)
        {
            TagBuilder tb = new TagBuilder("i");
            tb.Attributes.Add("class", "");

            tb.Attributes.Add("data-toggle", "popover");
            tb.Attributes.Add("data-placement", "auto");
            tb.Attributes.Add("data-content", msg);
            tb.Attributes.Add("aria-hidden", "true");

            switch (status)
            {
                case "S":
                    tb.Attributes.Add("style", "color:green;");
                    tb.AddCssClass("fa fa-check-circle fa-lg");
                    tb.Attributes.Add("title", sistema + " - Sucesso");
                    break;
                case "E":
                    tb.Attributes.Add("style", "color:red;");
                    tb.AddCssClass("fa fa-exclamation-circle fa-lg");
                    tb.Attributes.Add("title", sistema + " - Erro");
                    break;
                default:
                    tb.Attributes.Add("style", "color:orange;");
                    tb.AddCssClass("fa fa-exclamation-circle fa-lg");
                    tb.Attributes.Add("title", sistema + " - Não");
                    break;
            }

            return new MvcHtmlString(tb.ToString());
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            TypeConverter converter = TypeDescriptor.GetConverter(enumType);

            IEnumerable<SelectListItem> items =
                from value in values
                select new SelectListItem
                {
                    Text = converter.ConvertToString(value),
                    Value = value.ToString(),
                    Selected = value.Equals(metadata.Model)
                };

            if (metadata.IsNullableValueType)
            {
                items = SingleEmptyItem.Concat(items);
            }

            return htmlHelper.DropDownListFor(
                expression,
                items
                );
        }

        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }
            return realModelType;
        }

    }
}
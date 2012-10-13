using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;

public static class EnumExtensions
{
    /// <summary>
    /// Creates a select list from an enum.
    /// </summary>
    /// <seealso cref="http://stackoverflow.com/questions/388483/how-do-you-create-a-dropdownlist-from-an-enum-in-asp-net-mvc"/>
    /// <returns>
    /// SelectList
    /// </returns>
    public static IEnumerable<SelectListItem> ToSelectList(this Enum enumValue)
    {
        return from Enum e in Enum.GetValues(enumValue.GetType())
               select new SelectListItem
               {
                   Selected = e.Equals(enumValue),
                   Text = e.ToDescription(),
                   Value = e.ToString()
               };
    }

    public static string ToDescription(this Enum value)
    {
        var attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}
using System;
using System.Linq;

namespace Site.Core.Infrastructures.Utilities.Extensions
{
    public static class EnumExtensions
    {
        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            if (Assert.NotNull<Enum>(value))
            {
                object attribute = value.GetType().GetField(value.ToString()).GetCustomAttributes(false).FirstOrDefault();
                if (Assert.NotNull<object>(attribute))
                {
                    object propVal = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute);
                    return propVal.ToString();
                }
                    
            }
            return value.ToString();
        }
    }
    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }
}

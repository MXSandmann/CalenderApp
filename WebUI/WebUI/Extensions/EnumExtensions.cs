using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WebUI.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName<TEnum>(this TEnum enumValue) where TEnum : Enum
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .Single()
                .GetCustomAttribute<DisplayAttribute>()?
                .Name ?? string.Empty;                        
        }
    }
}

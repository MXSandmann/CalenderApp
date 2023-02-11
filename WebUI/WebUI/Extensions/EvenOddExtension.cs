using System.ComponentModel.DataAnnotations;
using System.Reflection;
using WebUI.Models.Enums;

namespace WebUI.Extensions
{
    public static class EvenOddExtension
    {
        public static string GetDisplayName(this EvenOdd evenOdd)
        {
            return evenOdd.GetType()
                .GetMember(evenOdd.ToString())
                .Single()
                .GetCustomAttribute<DisplayAttribute>()?
                .Name ?? string.Empty;                        
        }
    }
}

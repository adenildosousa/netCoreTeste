using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WAGym.Domain.Extension
{
    public static class EnumExtension
    {
        public static string GetName(this Enum enumValue)
        {
            return enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .First()?
                .GetCustomAttribute<DisplayAttribute>()?
                .Name;
        }
    }
}

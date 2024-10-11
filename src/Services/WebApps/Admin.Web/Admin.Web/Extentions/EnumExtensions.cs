using System.ComponentModel.DataAnnotations;

namespace Admin.Web.Extentions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var member = enumType.GetMember(enumValue.ToString()).FirstOrDefault();

            if (member != null)
            {
                var displayAttribute = member.GetCustomAttributes(false)
                    .OfType<DisplayAttribute>()
                    .FirstOrDefault();

                if (displayAttribute != null)
                {
                    return displayAttribute.Name;
                }
            }

            return enumValue.ToString(); // Trả về tên mặc định nếu không có DisplayAttribute
        }
    }

}

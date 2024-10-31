namespace Admin.Web.Extentions
{
    public static class RoleTranslator
    {
        private static readonly Dictionary<string, string> RoleTranslations = new Dictionary<string, string>
        {
            { "Admin", "Quản trị viên" },
            { "Receptionist", "Tiếp tân" },
            { "Security guard", "Bảo vệ" },
            { "Guest", "Khách hàng" }
        };

        public static string RoleTranslate(this string englishStatus)
        {
            if (RoleTranslations.TryGetValue(englishStatus, out var vietnameseStatus))
            {
                //Console.WriteLine(vietnameseStatus);
                return vietnameseStatus;
            }
            //Console.WriteLine(englishStatus);
            return englishStatus; // Trả về trạng thái gốc nếu không tìm thấy
        }
    }
}

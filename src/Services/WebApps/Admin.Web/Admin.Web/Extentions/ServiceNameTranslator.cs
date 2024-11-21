namespace Admin.Web.Extentions
{
    public static class ServiceNameTranslator
    {
        private static readonly Dictionary<string, string> StatusTranslations = new Dictionary<string, string>
        {
            { "CarRental", "Thuê xe ô tô" },
            { "BikeRental", "Thuê xe đạp" },
            { "Laundry", "Giặt ủi" },
            { "SwimmingPool", "Hồ bơi" },
            { "InstantNoodles", "Mì gói" },
            { "Bottledwater", "Nước đóng chai" },
            { "Honda", "Thuê xe máy" },
            { "Softdrink", "Nước ngọt" }

        };

        public static string ServiceNameTranslate(this string englishStatus)
        {
            if (StatusTranslations.TryGetValue(englishStatus, out var vietnameseStatus))
            {
                //Console.WriteLine(vietnameseStatus);
                return vietnameseStatus;
            }
            //Console.WriteLine(englishStatus);
            return englishStatus; // Trả về trạng thái gốc nếu không tìm thấy
        }
    }
}

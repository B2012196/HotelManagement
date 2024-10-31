namespace Admin.Web.Extentions
{
    public static class RoomStatusTranslator
    {
        private static readonly Dictionary<string, string> StatusTranslations = new Dictionary<string, string>
        {
            { "Available", "Sẵn sàng" },
            { "Reserved", "Đã đặt" },
            { "Occupied", "Đã có người" },
            { "Cleaning", "Đang dọn dẹp" },
            { "Maintenance", "Bảo trì" }

        };

        public static string Translate(this string englishStatus)
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

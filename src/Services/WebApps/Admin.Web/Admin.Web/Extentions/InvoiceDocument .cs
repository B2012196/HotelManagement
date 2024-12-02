namespace Admin.Web.Extentions
{
    public class InvoiceDocument(string BookingCode, DateTime CreatedAt, string Name, DateTime CheckinDate, DateTime CheckoutDate, string RoomTypeName, decimal RoomTypePrice,
            List<InvoiceServiceView> InvoiceServiceViews, decimal TotalBooking, decimal TotalServiceUsed, decimal TotalPrice, decimal PaymentTotal,  decimal RemainingAmount) : IDocument
    {
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A5);
                page.Margin(20);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Content().Column(column =>
                {
                    column.Spacing(10); // Khoảng cách giữa các thành phần

                    // Tiêu đề và thông tin khách sạn
                    column.Item().Text("GOLDEN SANDS HOTEL").Bold().FontSize(16).AlignCenter();
                    column.Item().Text("3/2, Xuân Khánh, Ninh Kiều, Cần Thơ").AlignCenter();
                    column.Item().Text("Hotline: 123.456.789").Bold().AlignCenter();
                    column.Item().LineHorizontal(1);

                    // Thông tin hóa đơn
                    column.Item().Text("HÓA ĐƠN THANH TOÁN").Bold().FontSize(16).AlignCenter();
                    column.Item().Text($"Mã đơn: {BookingCode}").Bold().FontSize(14).AlignCenter();

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text($"Khách hàng: {Name}").AlignLeft();
                        row.RelativeItem().Text($"Ngày lập: {CreatedAt:dd/MM/yyyy HH:mm:ss}").AlignRight();
                    });

                    // Bảng dữ liệu
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(); // Cột tên sản phẩm
                            columns.ConstantColumn(60); // Cột số lượng
                            columns.ConstantColumn(100); // Cột số lượng
                            columns.ConstantColumn(100); // Cột giá tiền
                        });

                        // Tiêu đề bảng
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Dịch vụ").Bold();
                            header.Cell().Element(CellStyle).Text("Đơn giá").Bold();
                            header.Cell().Element(CellStyle).Text("Số lượng").Bold();
                            header.Cell().Element(CellStyle).Text("Phí phòng").Bold();
                        });

                        // Dòng dữ liệu
                        table.Cell().Element(CellStyle).Text($"{RoomTypeName}");
                        table.Cell().Element(CellStyle).Text($"{RoomTypePrice.ToString("N0")}");
                        table.Cell().Element(CellStyle).Text($"Checkin: {CheckinDate:dd/MM/yyyy HH:mm:ss}. Checkout {CheckoutDate:dd/MM/yyyy HH:mm:ss}");
                        table.Cell().Element(CellStyle).Text(TotalBooking.ToString("N0"));

                        foreach(var service in InvoiceServiceViews)
                        {
                            table.Cell().Element(CellStyle).Text($"{service.ServiceName.ServiceNameTranslate()}");
                            table.Cell().Element(CellStyle).Text($"{service.ServicePrice.ToString("N0")}");
                            table.Cell().Element(CellStyle).Text($"{service.ServiceNumber}");
                            table.Cell().Element(CellStyle).Text($"{service.TotalServiceUsed.ToString("N0")}");
                        }
                    });

                    // Tổng tiền và lời cảm ơn
                    column.Item().Text($"Phí dịch vụ: {TotalServiceUsed.ToString("N0")} VND").AlignRight();
                    column.Item().Text($"Tổng hóa đơn: {TotalPrice.ToString("N0")} VND").AlignRight();
                    column.Item().Text($"Thanh toán online: {PaymentTotal.ToString("N0")} VND").AlignRight();
                    column.Item().Text($"Còn lại: {RemainingAmount.ToString("N0")} VND").Bold().FontSize(14).AlignRight();
                    column.Item().Text("XIN CẢM ƠN VÀ HẸN GẶP LẠI!").AlignCenter();
                });
            });
        }

        // Phong cách ô bảng
        private static IContainer CellStyle(IContainer container)
        {
            return container.Padding(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten1);
        }

    }
}

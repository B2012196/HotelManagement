namespace Admin.Web.Extentions
{
    public class InvoiceDocument(string BookingCode, DateTime CreatedAt, string Name, decimal ToTalPrice) : IDocument
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
                            columns.ConstantColumn(80); // Cột số lượng
                            columns.ConstantColumn(80); // Cột số lượng
                            columns.ConstantColumn(100); // Cột giá tiền
                        });

                        // Tiêu đề bảng
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Dịch vụ").Bold();
                            header.Cell().Element(CellStyle).Text("Đơn giá").Bold();
                            header.Cell().Element(CellStyle).Text("Số lượng").Bold();
                            header.Cell().Element(CellStyle).Text("Giá").Bold();
                        });

                        // Dòng dữ liệu
                        table.Cell().Element(CellStyle).Text("Room Booking");
                        table.Cell().Element(CellStyle).Text("500");
                        table.Cell().Element(CellStyle).Text("2");
                        table.Cell().Element(CellStyle).Text("1000 USD");
                    });

                    // Tổng tiền và lời cảm ơn
                    column.Item().Text($"Tổng thanh toán: {ToTalPrice:C}").AlignRight();
                    column.Item().Text($"Tiền đã thanh toán online: {ToTalPrice:C}").AlignRight();
                    column.Item().Text($"Còn lại: {ToTalPrice:C}").Bold().FontSize(14).AlignRight();
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

﻿namespace Admin.Web.Models
{
    public class RoomType
    {
        public Guid TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string ImageContentType { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
    }

    public record GetRoomTypesResponse(IEnumerable<RoomType> RoomTypes);
    public record GetRoomTypeByIdResponse(RoomType RoomType);
    public record CreateRoomTypeResponse(Guid TypeId);
    public record UpdateRoomTypeResponse(bool IsSuccess);
    public record UploadRoomTypeImageResponse(bool IsSuccess);
    public record DeleteRoomTypeResponse(bool IsSuccess);

}

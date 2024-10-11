namespace Admin.Web.Models
{
    public class RoomStatus
    {
        public Guid StatusId { get; set; }
        public string Name { get; set; }
    }


    public record GetRoomStatusesResponse(IEnumerable<RoomStatus> Statuses);
    public record GetRoomStatusByIdResponse(RoomStatus RoomStatus);
    public record CreateRoomStatusResponse(Guid StatusId);
    public record UpdateRoomStatusResponse(bool IsSuccess);
    public record DeleteRoomStatusResponse(bool IsSuccess);
}

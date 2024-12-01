namespace Admin.Web.Services
{
    public interface IHotelService
    {
        //room
        [Get("/hotels/rooms")]
        Task<GetRoomsResponse> GetRooms(int? pageNumber, int? pageSize);

        [Get("/hotels/rooms/available")]
        Task<GetRoomsAvaResult> GetRoomsAvailable();

        [Get("/hotels/rooms/roomid/{RoomId}")]
        Task<GetRoomByIdResponse> GetRoomtByRoomId(Guid RoomId);

        [Get("/hotels/rooms/ava/{TypeId}")]
        Task<GetRoomsAvaResponse> GetRoomtavaByTypeId(Guid TypeId);

        [Get("/hotels/rooms/type/{TypeId}")]
        Task<GetRoomsByTypeResponse> GetRoomtByTypeId(Guid TypeId);

        [Post("/hotels/rooms")]
        Task<CreateRoomResponse> CreateRoom(Room Room);

        [Put("/hotels/rooms")]
        Task<UpdateRoomResponse> UpdateRoom(Room Room);

        [Delete("/hotels/rooms/{RoomId}")]
        Task<DeleteRoomResponse> DeleteRoom(Guid RoomId);


        //roomtype
        [Get("/hotels/roomtypes")]
        Task<GetRoomTypesResponse> GetRoomTypes();

        [Get("/hotels/roomtypes/id/{TypeId}")]
        Task<GetRoomTypeByIdResponse> GetRoomTypeById(Guid TypeId);

        [Post("/hotels/roomtypes")]
        Task<CreateRoomTypeResponse> CreateRoomType(object obj);

        [Put("/hotels/roomtypes")]
        Task<UpdateRoomTypeResponse> UpdateRoomType(object obj);

        [Multipart]
        [Put("/hotels/roomtypes/upload-image/{TypeId}")]
        Task<UploadRoomTypeImageResponse> UploadImageRoomType(Guid TypeId, [AliasAs("File")] StreamPart file);

        [Delete("/hotels/roomtypes/{RoomtypeId}")]
        Task<DeleteRoomTypeResponse> DeleteRoomType(Guid RoomtypeId);

        //roomstatus
        [Get("/hotels/roomstatus")]
        Task<GetRoomStatusesResponse> GetRoomStatuses();
        [Get("/hotels/roomstatus/id/{StatusId}")]
        Task<GetRoomStatusByIdResponse> GetRoomStatusById(Guid StatusId);

        [Post("/hotels/roomstatus")]
        Task<CreateRoomStatusResponse> CreateRoomStatus(object obj);

        [Put("/hotels/roomstatus")]
        Task<UpdateRoomStatusResponse> UpdateRoomStatus(RoomStatus RoomStatus);

        [Delete("/hotels/roomstatus/{StatusId}")]
        Task<DeleteRoomStatusResponse> DeleteRoomStatus(Guid StatusId);
    }
}

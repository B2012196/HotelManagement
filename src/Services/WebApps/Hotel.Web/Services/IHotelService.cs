namespace Hotel.Web.Services
{
    public interface IHotelService
    {
        //hotel
        [Get("/hotels/hotels")]
        Task<GetHotelsResponse> GetHotels();
        [Get("/guests/guests/type/{TypeId}")]
        Task<GetRoomsByTypeResponse> GetRoomtByTypeId(Guid TypeId);

        [Post("/hotels/hotels")]
        Task<CreateHotelResponse> CreateHotel(HotelModel Hotel);

        [Put("/hotels/hotels")]
        Task<UpdateHotelResponse> UpdateHotel(HotelModel Hotel);

        [Delete("/hotels/hotels/{HotelId}")]
        Task<DeleteHotelResponse> DeleteHotel(Guid HotelId);

        //roomtype
        [Get("/hotels/roomtypes")]
        Task<GetRoomTypesResponse> GetRoomTypes();
        [Get("/hotels/roomtypes/id/{TypeId}")]
        Task<GetRoomTypeByIdResponse> GetRoomTypeById(Guid TypeId);

        [Post("/hotels/roomtypes")]
        Task<CreateRoomTypeResponse> CreateRoomType(RoomType Type);

        [Put("/hotels/roomtypes")]
        Task<UpdateRoomTypeResponse> UpdateRoomType(RoomType Type);

        [Delete("/hotels/roomtypes/{RoomtypeId}")]
        Task<DeleteRoomTypeResponse> DeleteRoomType(Guid RoomtypeId);

        //room
        [Get("/hotels/rooms")]
        Task<GetRoomsResponse> GetRooms();

    }
}

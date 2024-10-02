namespace Hotel.Web.Services
{
    public interface IHotelService
    {
        //hotel
        [Get("/hotels/hotels")]
        Task<GetHotelsResponse> GetHotels();

        [Post("/hotels/hotels")]
        Task<CreateHotelResponse> CreateHotel(HotelModel Hotel);

        [Put("/hotels/hotels")]
        Task<UpdateHotelResponse> UpdateHotel(HotelModel Hotel);

        [Delete("/hotels/hotels/{HotelId}")]
        Task<DeleteHotelResponse> DeleteHotel(Guid HotelId);

        //roomtype
        [Get("/hotels/roomtypes")]
        Task<GetRoomTypesResponse> GetRoomTypes();

        [Post("/hotels/roomtypes")]
        Task<CreateRoomTypeResponse> CreateRoomType(RoomTypeModel Type);

        [Put("/hotels/roomtypes")]
        Task<UpdateRoomTypeResponse> UpdateRoomType(RoomTypeModel Type);

        [Delete("/hotels/roomtypes/{RoomtypeId}")]
        Task<DeleteRoomTypeResponse> DeleteRoomType(Guid RoomtypeId);


    }
}

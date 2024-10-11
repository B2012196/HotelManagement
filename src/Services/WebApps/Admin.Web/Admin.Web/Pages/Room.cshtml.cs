using System;

namespace Admin.Web.Pages
{
    public class RoomModel(IHotelService hotelService, ILogger<BookingModel> logger) : PageModel
    {
        public IEnumerable<RoomView> RoomList { get; set; } = new List<RoomView>();
        public IEnumerable<RoomType> TypeList { get; set; } = new List<RoomType>();
        public IEnumerable<RoomStatus> StatusList { get; set; } = new List<RoomStatus>();
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var resultroom = await hotelService.GetRooms();

                var RoomViewList = new List<RoomView>();

                foreach (var room in resultroom.Rooms)
                {
                    var status = await hotelService.GetRoomStatusById(room.StatusId);
                    var type = await hotelService.GetRoomTypeById(room.TypeId);
                    var roomView = new RoomView
                    {
                        RoomId = room.RoomId,
                        Number = room.Number,
                        TypeId = room.TypeId,
                        TypeName = type.RoomType.Name,
                        StatusId = room.StatusId,
                        StatusName = status.RoomStatus.Name,
                    };
                    RoomViewList.Add(roomView);
                }
                RoomList = RoomViewList;


                var resultType = await hotelService.GetRoomTypes();
                TypeList = resultType.RoomTypes;

                var resultStatus = await hotelService.GetRoomStatuses();
                StatusList = resultStatus.Statuses;


            }
            catch (Exception ex)
            {
                logger.LogInformation($"{ex.Message}");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddRoomAsync(string Number, string TypeId, string StatusId)
        {
            Guid typeIdGuid;
            Guid statusIdGuid;
            Guid hotelIdGuid;
            if (!Guid.TryParse(TypeId, out typeIdGuid) || !Guid.TryParse(StatusId, out statusIdGuid) || !Guid.TryParse("15c7bc29-7380-43f8-87bb-bf062bcf5b14", out hotelIdGuid))
            {
                ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                logger.LogInformation("Dữ liệu không hợp lệ.");
                return Page();
            }

            var room = new Room
            {
                Number = Number,
                HotelId = hotelIdGuid,
                TypeId = typeIdGuid,
                StatusId = statusIdGuid,
            };

            var resultCreateRoom = await hotelService.CreateRoom(room);
            if (resultCreateRoom == null)
            {
                logger.LogInformation("Error: Cannot create the room");
            }

            return RedirectToPage("Room");
        }

        public async Task<IActionResult> OnPostUpdateRoomAsync(string RoomId, string Number, string TypeId, string StatusId)
        {
            Guid typeIdGuid;
            Guid statusIdGuid;
            Guid hotelIdGuid;
            Guid roomIdGuid;
            if (!Guid.TryParse(TypeId, out typeIdGuid) || !Guid.TryParse(StatusId, out statusIdGuid) 
                || !Guid.TryParse("15c7bc29-7380-43f8-87bb-bf062bcf5b14", out hotelIdGuid) || !Guid.TryParse(RoomId, out roomIdGuid))
            {
                ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                logger.LogInformation("Dữ liệu không hợp lệ.");
                return Page();
            }

            var room = new Room
            {
                RoomId = roomIdGuid,
                Number = Number,
                HotelId = hotelIdGuid,
                TypeId = typeIdGuid,
                StatusId = statusIdGuid,
            };

            var resultCreateRoom = await hotelService.UpdateRoom(room);
            if (!resultCreateRoom.IsSuccess)
            {
                logger.LogInformation("Error: Cannot update the room");
            }

            return RedirectToPage("Room");
        }

        public async Task<IActionResult> OnPostDeleteRoomAsync(string RoomId)
        {
            Guid roomIdGuid;
            if (!Guid.TryParse(RoomId, out roomIdGuid))
            {
                ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                logger.LogInformation("Dữ liệu không hợp lệ.");
                return RedirectToPage("Room");
            }

            var resultCreateRoom = await hotelService.DeleteRoom(roomIdGuid);
            if (!resultCreateRoom.IsSuccess)
            {
                logger.LogInformation("Error: Cannot update the room");
            }

            return RedirectToPage("Room");
        }
    }
}

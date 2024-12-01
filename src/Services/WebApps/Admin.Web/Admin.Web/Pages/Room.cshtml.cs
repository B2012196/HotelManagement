namespace Admin.Web.Pages
{
    public class RoomModel(IHotelService hotelService, ILogger<BookingModel> logger) : PageModel
    {
        public IEnumerable<RoomView> RoomList { get; set; } = new List<RoomView>();
        public IEnumerable<RoomType> TypeList { get; set; } = new List<RoomType>();
        public IEnumerable<RoomStatus> StatusList { get; set; } = new List<RoomStatus>();
        public RoomPage RoomPage { get; set; } = new RoomPage();
        public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                int totalCount;
                var resultType = await hotelService.GetRoomTypes();
                TypeList = resultType.RoomTypes;

                var resultStatus = await hotelService.GetRoomStatuses();
                StatusList = resultStatus.Statuses;

                var resultroom = await hotelService.GetRooms(pageNumber, pageSize);

                RoomPage.PageNumber = pageNumber;
                RoomPage.PageSize = pageSize;
                totalCount = resultroom.TotalCount;
                RoomPage.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var RoomViewList = new List<RoomView>();

                foreach (var room in resultroom.Rooms)
                {
                    var status = StatusList.SingleOrDefault(s => s.StatusId == room.StatusId);
                    var type = TypeList.SingleOrDefault(t => t.TypeId == room.TypeId);
                    if(status != null && type != null)
                    {
                        var roomView = new RoomView
                        {
                            RoomId = room.RoomId,
                            Number = room.Number,
                            TypeId = room.TypeId,
                            TypeName = type.Name,
                            StatusId = room.StatusId,
                            StatusName = status.Name,
                        };
                        RoomViewList.Add(roomView);
                    }
                }
                RoomList = RoomViewList;
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddRoomAsync(string Number, string TypeId, string StatusId)
        {
            try
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
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }
            return RedirectToPage("Room");
        }

        public async Task<IActionResult> OnPostUpdateRoomAsync(string RoomId, string Number, string TypeId, string StatusId)
        {
            try
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
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }

            return RedirectToPage("Room");
        }

        public async Task<IActionResult> OnPostDeleteRoomAsync(string RoomId)
        {
            try
            {
                Guid roomIdGuid;
                if (!Guid.TryParse(RoomId, out roomIdGuid))
                {
                    ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return RedirectToPage("Room");
                }

                var resultDeleteRoom = await hotelService.DeleteRoom(roomIdGuid);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }

            return RedirectToPage("Room");
        }

        public async Task<IActionResult> OnPostAddRoomStatusAsync(string StatusName)
        {
            try
            {
                var status = new
                {
                    Name = StatusName,
                };
                var resultAddStatus = await hotelService.CreateRoomStatus(status);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }
            return RedirectToPage("Room");
        }

        public async Task<IActionResult> OnPostUpdateRoomStatusAsync(string StatusId, string StatusName)
        {
            try
            {
                Guid statusIdGuid;
                if (!Guid.TryParse(StatusId, out statusIdGuid))
                {
                    ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return Page();
                }

                var status = new RoomStatus
                {
                    StatusId = statusIdGuid,
                    Name = StatusName
                };

                var resultUpdateStatus = await hotelService.UpdateRoomStatus(status);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }
            return RedirectToPage("Room");

        }

        public async Task<IActionResult> OnPostDeleteRoomStatusAsync(string StatusId)
        {
            try
            {
                Guid statusIdGuid;
                if (!Guid.TryParse(StatusId, out statusIdGuid))
                {
                    ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return Page();
                }
                var resultDeleteStatus = await hotelService.DeleteRoomStatus(statusIdGuid);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }
            return RedirectToPage("Room");
        }

        public async Task<IActionResult> OnPostAddRoomTypeAsync(string TypeName, string Description, decimal PricePerNight, int Capacity)
        {
            try
            {
                var type = new
                {
                    Name = TypeName,
                    Description = Description,
                    PricePerNight = PricePerNight,
                    Capacity = Capacity
                };

                var resultAddType = await hotelService.CreateRoomType(type);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }
            return RedirectToPage("Room");
        }

        public async Task<IActionResult> OnPostUpdateRoomTypeAsync(string TypeId, string TypeName, string Description, decimal PricePerNight, int Capacity)
        {
            try
            {
                Guid typeIdGuid;
                if(!Guid.TryParse(TypeId, out typeIdGuid))
                {
                    ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return Page();
                }
                var type = new
                {
                    TypeId = typeIdGuid,
                    Name = TypeName,
                    Description = Description,
                    PricePerNight = PricePerNight,
                    Capacity = Capacity
                };

                var resultUpdateRoomType = await hotelService.UpdateRoomType(type);

            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }
            return RedirectToPage("Room");

        }


        public async Task<IActionResult> OnPostDeleteRoomTypeAsync(string TypeId)
        {
            try
            {
                Guid typeIdGuid;
                if (!Guid.TryParse(TypeId, out typeIdGuid))
                {
                    ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return Page();
                }
                var resultDeleteType = await hotelService.DeleteRoomType(typeIdGuid);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }
            return RedirectToPage("Room");
        }

        public async Task<IActionResult> OnPostUploadImageRoomTypeAsync(string TypeId, IFormFile Image)
        {
            try
            {
                Guid typeIdGuid;
                if (!Guid.TryParse(TypeId, out typeIdGuid))
                {
                    ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return Page();
                }

                // Gửi file dưới dạng multipart/form-data
                using var stream = Image.OpenReadStream();
                var streamPart = new StreamPart(stream, Image.FileName, Image.ContentType);
                var resultUploadImage = await hotelService.UploadImageRoomType(typeIdGuid, streamPart);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }
            return RedirectToPage("Room");
        }

        private void HandleApiException(ApiException apiEx)
        {
            switch (apiEx.StatusCode)
            {
                case System.Net.HttpStatusCode.BadRequest:
                    Console.WriteLine("Bad request: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không tìm thấy nội dung";
                    break;

                case System.Net.HttpStatusCode.Unauthorized:
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Đăng nhập để tiếp tục";
                    break;

                case System.Net.HttpStatusCode.Forbidden:
                    Console.WriteLine("Forbidden: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không có quyền truy cập";
                    break;

                default:
                    Console.WriteLine($"Error: {apiEx.StatusCode}, Content: {apiEx.Content}");
                    TempData["ErrorApiException"] = "Lỗi hệ thống";
                    break;
            }
        }
    }
}

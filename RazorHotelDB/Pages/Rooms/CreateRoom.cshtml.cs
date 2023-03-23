using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Rooms
{
    public class CreateRoomModel : PageModel
    {
        [BindProperty]
        public Room Room { get; set; }
        [BindProperty]
        public int HotelNr { get; set; }
        [BindProperty]
        public RoomType TheRoomType { get; set; }

        private IRoomService rService;

        public CreateRoomModel(IRoomService roomService)
        {
            rService = roomService;
        }

        public void OnGet(int hotelNr)
        {
            HotelNr = hotelNr;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            HotelNr = Room.HotelNr;
            Room.Types = TheRoomType.ToString()[0];
            await rService.CreateRoomAsync(HotelNr ,Room);
            return RedirectToPage("GetAllRooms");
        }
    }
}

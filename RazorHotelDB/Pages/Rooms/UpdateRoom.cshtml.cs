using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Rooms
{
    public class UpdateRoomModel : PageModel
    {
        private IRoomService _rService;

        [BindProperty]
        public Room RoomToUpdate { get; set; }
        [BindProperty]
        public RoomType TheRoomType { get; set; }
        public UpdateRoomModel(IRoomService roomService)
        {
            _rService = roomService;
        }

        public async Task OnGetAsync(int hotelnr, int roomnr)
        {

            RoomToUpdate = await _rService.RoomSearch(hotelnr, roomnr);

        }

        public async Task<IActionResult> OnPostAsync(int hotelnr, int roomnr)
        {
            RoomToUpdate.Types = TheRoomType.ToString()[0];
            bool ok = await _rService.UpdateRoomAsync(RoomToUpdate, roomnr, hotelnr);
            if (ok)
            {
                return RedirectToPage("GetAllRooms");
            }
            else
            {
                return Page();
            }
        }
    }
}

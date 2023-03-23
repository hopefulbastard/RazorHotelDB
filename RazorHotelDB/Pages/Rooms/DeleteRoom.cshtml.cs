using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Rooms
{
    public class DeleteRoomModel : PageModel
    {
        private IRoomService rService;
        [BindProperty]
        public Room RoomToDelete { get; set; }
        public DeleteRoomModel(IRoomService roomService)
        {
            rService = roomService;
        }

        public async Task OnGetAsync(int hotelnr, int roomnr)
        {
            RoomToDelete = await rService.RoomSearch(hotelnr, roomnr);

        }

        public async Task<IActionResult> OnPostAsync(int hotelnr, int roomnr)
        {
            bool ok = await rService.DeleteRoomAsync(roomnr, hotelnr);
            if (ok == true)
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

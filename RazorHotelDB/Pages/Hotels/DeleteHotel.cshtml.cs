using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Hotels
{
    public class DeleteHotelModel : PageModel
    {
        private IHotelService hService;
        [BindProperty]
        public Hotel HotelToDelete { get; set; }
        public DeleteHotelModel(IHotelService hotelService)
        {
            hService = hotelService;
        }

        public async Task OnGetAsync(int hotelnr)
        {
            HotelToDelete = await hService.GetHotelFromIdAsync(hotelnr);

        }

        public async Task<IActionResult> OnPostAsync(int hotelnr)
        {
            Hotel deletedHotel = await hService.DeleteHotelAsync(hotelnr);
            if( deletedHotel != null)
            {
                return RedirectToPage("GetAllHotels");
            }
            else
            {
                return Page();
            }
        }
    }
}

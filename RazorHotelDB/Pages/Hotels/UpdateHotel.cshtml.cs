using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Hotels
{
    public class UpdateHotelModel : PageModel
    {
        private IHotelService _hService;

        [BindProperty]
        public Hotel HotelToUpdate { get; set; }
        public UpdateHotelModel(IHotelService hotelService)
        {
            _hService = hotelService;
        }

        public async Task OnGetAsync(int hotelnr)
        {

            HotelToUpdate = await _hService.GetHotelFromIdAsync(hotelnr);

        }

        public async Task<IActionResult> OnPostAsync(int hotelnr)
        {
            bool ok = await _hService.UpdateHotelAsync(HotelToUpdate, hotelnr);
            if (ok)
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

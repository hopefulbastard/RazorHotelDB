using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Hotels
{
    public class GetAllHotelsModel : PageModel
    {
        public List<Hotel> Hotels { get; set; }
        private IHotelService hService;
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public GetAllHotelsModel(IHotelService hotelService)
        {
            hService = hotelService;
        }

        public async Task OnGetAsync()
        {
            if (!FilterCriteria.IsNullOrEmpty())
            {
                Hotels = await hService.GetHotelsByNameAsync(FilterCriteria);
            }
            else
            {
                Hotels = await hService.GetAllHotelAsync();
            }  
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Rooms
{
    public class GetAllRoomsModel : PageModel
    {
        //de viste værelser, som hentes fra databasen i OnGet
        public List<Room> Rooms { get; set; }
        private IRoomService rService;

        //til dropdown menuuen, henter alle hoteller fra databasen i OnGet
        public List<Hotel> Hotels { get; set; }
        private IHotelService hService;

        //henter værdien i dropdown menuuen
        [BindProperty(SupportsGet = true)]
        public int FilterCriteria { get; set; }

        public GetAllRoomsModel(IRoomService roomService, IHotelService hotelService)
        {
            rService = roomService;
            hService = hotelService;

        }

        public async Task OnGetAsync()
        {
            //henter alle hoteller fra databasen ned i Hotels, så de kan vises i dropdown menuuen
            Hotels = await hService.GetAllHotelAsync();

            //hvis intet er valgt i dropdown menuen hentes alle hoteller
            if (FilterCriteria == 0)
            {
                Rooms = await rService.GetAllRoomsAsync();
            }
            //hvis et hotel er valgt i menuen, hentes kun de værelser med det hotelnr ned
            else
            {
                Rooms = await rService.GetRoomFromHotelAsync(FilterCriteria);
            }
        }

    }
}

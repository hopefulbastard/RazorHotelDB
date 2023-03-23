using RazorHotelDB.Models;

namespace RazorHotelDB.Interfaces
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllRoomsAsync();

        Task<List<Room>> GetRoomFromHotelAsync(int hotelNr);

        Task<Room> RoomSearch(int hotelNr, int roomNr);

        Task<bool> CreateRoomAsync(int hotelNr, Room room);

        Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr);

        Task<bool> DeleteRoomAsync(int roomNr, int hotelNr);

    }
}

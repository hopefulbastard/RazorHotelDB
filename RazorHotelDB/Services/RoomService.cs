using Microsoft.Data.SqlClient;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Services
{
    public class RoomService : Connection, IRoomService
    {

        private String queryString = "select * from Room";
        private String insertSql = "insert into Room Values (@RoomNo, @HotelNo, @Type, @Price)";
        private String deleteSql = "delete from Room where Hotel_No=@HotelNo and Room_No=@RoomNo";
        private String updateSql = "update Room " +
                                   "set Room_No=@RoomNo, Hotel_No= @HotelNo, Types=@RoomType, Price=@RoomPrice " +
                                   "where Hotel_No = @ID and Room_No=@RoomID";
        private string queryStringFromID = "select * from Room where Hotel_No = @ID";
        private String roomSearch = "select * from Room where Hotel_No = @ID and Room_No = @RoomID";


        public RoomService(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<bool> CreateRoomAsync(int hotelNr, Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@RoomNo", room.RoomNr);
                    command.Parameters.AddWithValue("@HotelNo", room.HotelNr);
                    command.Parameters.AddWithValue("@Type", room.Types);
                    command.Parameters.AddWithValue("@Price", room.Pris);
                    try
                    {
                        command.Connection.Open();
                        int noOfRows = await command.ExecuteNonQueryAsync(); //bruges ved update, delete, insert
                        if (noOfRows == 1)
                        {
                            return true;
                        }

                        return false;
                    }
                    catch (SqlException sqlex)
                    {
                        Console.WriteLine("Database error");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel error");
                    }
                }

            }
            return false;
        }

        public async Task<bool> DeleteRoomAsync(int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@HotelNo", hotelNr);
                    command.Parameters.AddWithValue("@RoomNo", roomNr);

                    try
                    {
                        command.Connection.Open();
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        if (noOfRows == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Generel fejl" + exp.Message);
                    }
                }
            }
            return false;
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();//aSynkront
                        SqlDataReader reader = await command.ExecuteReaderAsync();//aSynkront
                        while (await reader.ReadAsync())
                        {
                            int roomNr = reader.GetInt32(0);
                            int hotelNo = reader.GetInt32(1);
                            char types = reader.GetString(2)[0];
                            double pris = reader.GetDouble(3);
                            Room room = new Room(roomNr, types, pris, hotelNo);
                            rooms.Add(room);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                        return null;
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Generel fejl" + exp.Message);
                        return null;
                    }
                }
            }
            return rooms;
        }

        public async Task<List<Room>> GetRoomFromHotelAsync(int hotelNr)
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                    try
                    {
                        SqlCommand command = new SqlCommand(queryStringFromID, connection);
                        command.Parameters.AddWithValue("@ID", hotelNr);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int roomNr = reader.GetInt32(0);
                            int hotelNo = reader.GetInt32(1);
                            char types = reader.GetString(2)[0];
                            double pris = reader.GetDouble(3);
                            Room room = new Room(roomNr, types, pris, hotelNo);
                            rooms.Add(room);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                        return null;
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Generel fejl" + exp.Message);
                        return null;
                    }
                }
            return rooms;
        }

        public async Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    command.Parameters.AddWithValue("@RoomID", roomNr);
                    command.Parameters.AddWithValue("@RoomNo", room.RoomNr);
                    command.Parameters.AddWithValue("@HotelNo", room.HotelNr);
                    command.Parameters.AddWithValue("@RoomType", room.Types);
                    command.Parameters.AddWithValue("@RoomPrice", room.Pris);
                    try
                    {
                        command.Connection.Open();
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        if (noOfRows == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Generel fejl" + exp.Message);
                    }
                }
            }
            return false;
        }

        public async Task<Room> RoomSearch(int hotelNr, int roomNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(roomSearch, connection);
                    commmand.Parameters.AddWithValue("@ID", hotelNr);
                    commmand.Parameters.AddWithValue("@RoomID", roomNr);
                    await commmand.Connection.OpenAsync();

                    SqlDataReader reader = await commmand.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int roomNo = reader.GetInt32(0);
                        int hotelNo = reader.GetInt32(1);
                        char types = reader.GetString(2)[0];
                        double pris = reader.GetDouble(3);
                        Room room = new Room(roomNo, types, pris, hotelNo);
                        return room;
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }

            }
            return null;

        }
    }
}

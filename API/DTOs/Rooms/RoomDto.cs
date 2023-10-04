using API.Models;

namespace API.DTOs.Rooms;

public class RoomDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public int Floor { get; set; }
    public int Capacity { get; set; }
    
    public static explicit operator RoomDto(Room room) // Operator explicit untuk mengkonversi Room menjadi RoomDto.
    {
        return new RoomDto // Mengembalikan object RoomDto dengan data dari property Room.
        {
            Guid = room.Guid,
            Name = room.Name,
            Floor = room.Floor,
            Capacity = room.Capacity
        };
    }
    
    public static implicit operator Room(RoomDto roomDto) // Operator implicit untuk mengkonversi RoomDto menjadi Room.
    {
        return new Room // Mengembalikan object Room dengan data dari property RoomDto.
        {
            Guid = roomDto.Guid,
            Name = roomDto.Name,
            Floor = roomDto.Floor,
            Capacity = roomDto.Capacity,
            ModifiedDate = DateTime.Now
        };
    }
}
using API.Models;

namespace API.DTOs.Rooms;

public class CreateRoomDto
{
    public string Name { get; set; }
    public int Floor { get; set; }
    public int Capacity { get; set; }
    
    public static implicit operator Room(CreateRoomDto createRoomDto)
    {
        return new Room
        {
            Name = createRoomDto.Name,
            Floor = createRoomDto.Floor,
            Capacity = createRoomDto.Capacity,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
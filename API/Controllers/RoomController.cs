using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // Untuk menunjukkan bahwa ini adalah controller API
[Route("api/[controller]")] // Untuk menunjukkan route dari controller ini
public class RoomController : ControllerBase // ControllerBase untuk controller tanpa view
{
    private readonly IRoomRepository _roomRepository;
    
    public RoomController(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    // Untuk menangani request GET dengan route api/[controller]/GetAll
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _roomRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }
    
    // Untuk menangani request GET dengan route api/[controller]/guid
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _roomRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }
    
    // Untuk menangani request POST dengan route api/[controller]
    [HttpPost]
    public IActionResult Create(Room room)
    {
        var result = _roomRepository.Create(room);
        if (result is null)
        {
            return BadRequest("Data Not Created");
        }
        return Ok(result);
    }
    
    // Untuk menangani request PUT dengan route api/[controller]
    [HttpPut]
    public IActionResult Update(Room room)
    {
        var result  = _roomRepository.Update(room);
        if (!result)
        {
            return BadRequest("Data Not Updated");
        }
        return Ok("Data has been updated successfully");
    }
    
    // Untuk menangani request DELETE dengan route api/[controller]
    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var room = _roomRepository.GetByGuid(guid);
        var result = _roomRepository.Delete(room);
        if (!result)
        {
            return BadRequest("Data Not Deleted");
        }
        
        return Ok("Data has been deleted successfully");
    }
}
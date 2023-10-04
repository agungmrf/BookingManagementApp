using API.Contracts;
using API.DTOs.Roles;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // Untuk menunjukkan bahwa ini adalah controller API
[Route("api/[controller]")] // Untuk menunjukkan route dari controller ini
public class RoleController : ControllerBase // ControllerBase untuk controller tanpa view
{
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    
    // Untuk menangani request GET dengan route api/[controller]
    [HttpGet]
    public IActionResult GetAll()
    {
        // Mengambil semua data dari database.
        var result = _roleRepository.GetAll();
        if (!result.Any())
        {
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        }
        // Mengubah IEnumerable<Role> menjadi IEnumerable<RoleDto>
        var data = result.Select(x => (RoleDto)x);
        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<IEnumerable<RoleDto>>(data));
    }
    
    // Untuk menangani request GET dengan route api/[controller]/guid
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Mengambil data dari database berdasarkan guid.
        var result = _roleRepository.GetByGuid(guid);
        if (result is null)
        {
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        }
        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<RoleDto>((RoleDto)result));
    }
    
    // Untuk menangani request POST dengan route api/[controller]
    [HttpPost]
    public IActionResult Create(CreateRoleDto createRoleDto)
    {
        try
        {
            // Membuat data baru di database.
            var result = _roleRepository.Create(createRoleDto);
            
            // Setelah data berhasil dibuat, maka akan mengembalikan response 201 Created.
            return Ok(new ResponseOKHandler<RoleDto>("Data has been created successfully") { Data = (RoleDto)result });
        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }
    
    // Untuk menangani request PUT dengan route api/[controller]
    [HttpPut]
    public IActionResult Update(RoleDto roleDto)
    {
        try
        {
            // Mengambil data di database berdasarkan guid.
            var entity = _roleRepository.GetByGuid(roleDto.Guid);
            if (entity is null)
            {
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
        
            Role toUpdate = roleDto;
            toUpdate.CreatedDate = entity.CreatedDate; // Menyalin CreatedDate dari entity
        
            _roleRepository.Update(toUpdate);

            // Setelah data berhasil diupdate, maka akan mengembalikan response 200 OK.
            return Ok(new ResponseOKHandler<RoleDto>("Data has been updated successfully") { Data = (RoleDto)toUpdate });
        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to update data", ex.Message));
        }
    }

    // Untuk menangani request DELETE dengan route api/[controller]/guid
    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            // Mengambil data di database berdasarkan guid.
            var entity = _roleRepository.GetByGuid(guid);
            if (entity is null)
            {
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }

            // Menghapus data di database berdasarkan guid.
            _roleRepository.Delete(entity);

            // Setelah data berhasil dihapus, maka akan mengembalikan response 200 OK.
            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }
}
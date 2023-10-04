using API.Contracts;
using API.DTOs.Accounts;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase // Controller is for MVC
{
    private readonly IAccountRepository _accountRepository; // readonly is for dependency injection

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    // Untuk menangani request GET dengan route /api/[controller].
    [HttpGet]
    public IActionResult GetAll()
    {
        // Mengambil semua data dari database.
        var result = _accountRepository.GetAll();
        if (!result.Any())
        {
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found")); 
        }
        
        // Mengubah IEnumerable<Account> menjadi IEnumerable<AccountDto>
        var data = result.Select(x => (AccountDto)x);
        
        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<IEnumerable<AccountDto>>(data)); 
    }
    
    // Untuk menangani request GET dengan route /api/[controller]/guid.
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Mengambil data dari database berdasarkan guid.
        var result = _accountRepository.GetByGuid(guid);
        if (result is null)
        {
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found")); 
        }
        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result)); 
    }
    
    // Untuk menangani request POST dengan route /api/[controller].
    [HttpPost]
    public IActionResult Create(CreateAccountDto createAccountDto)
    {
        try
        {
            Account toCreate = createAccountDto;
            // Untuk menghash password sebelum disimpan ke database
            toCreate.Password = HashingHandler.HashPassword(createAccountDto.Password); 
        
            var result = _accountRepository.Create(toCreate);
            
            // Setelah data berhasil dibuat, maka akan mengembalikan response 201 Created.
            return Ok(new ResponseOKHandler<AccountDto>("Data has been created successfully") { Data = (AccountDto)result });
        }
        catch (ExceptionHandler ex) // ExceptionHandler untuk menangani exception ketika terjadi error
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }

    // Untuk menangani request PUT dengan route /api/[controller].
    [HttpPut]
    public IActionResult Update(AccountDto accountDto)
    {
        try
        {
            // Mengambil data di database berdasarkan guid.
            var entity = _accountRepository.GetByGuid(accountDto.Guid);
            if (entity is null)
            {
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Data Not Found")); 
            }

            Account toUpdate = accountDto;
            toUpdate.CreatedDate = entity.CreatedDate; // Menyalin CreatedDate dari entity yang diambil dari database.
            toUpdate.Password = HashingHandler.HashPassword(accountDto.Password); // Mengambil password dari request body, kemudian dihash sebelum disimpan ke database
        
            _accountRepository.Update(toUpdate);
            
            // Setelah data berhasil diubah, maka akan mengembalikan response 200 OK.
            return Ok(new ResponseOKHandler<AccountDto>("Data has been updated successfully") { Data = (AccountDto)toUpdate });
        }
        catch (ExceptionHandler ex) // ExceptionHandler untuk menangani exception ketika terjadi error
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to update data", ex.Message));
        }
    }
    
    // Untuk menangani request DELETE dengan route /api/[controller].
    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            // Menghapus data di database berdasarkan guid.
            var entity = _accountRepository.GetByGuid(guid);
            if (entity is null)
            {
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }

            _accountRepository.Delete(entity);

            // Setelah data berhasil dihapus, maka akan mengembalikan response 200 OK.
            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex) // ExceptionHandler untuk menangani exception ketika terjadi error
        {
            // ResponseServerErrorHandler untuk response 500 Internal Server Error
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }
}
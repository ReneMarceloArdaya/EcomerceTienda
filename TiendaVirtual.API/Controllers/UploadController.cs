using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using TiendaVirtual.API.Models;

[Route("api/[controller]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly Cloudinary _cloudinary;

    public UploadController(IConfiguration configuration)
    {
        _configuration = configuration;
        var account = new Account(
            _configuration["Cloudinary:CloudName"],
            _configuration["Cloudinary:ApiKey"],
            _configuration["Cloudinary:ApiSecret"]
        );
        _cloudinary = new Cloudinary(account);
    }

    [HttpPost("image")]
    public async Task<IActionResult> Post(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No se ha seleccionado ningún archivo.");
        }

        // Definimos los parámetros para la subida
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            // Opcional: Asigna una carpeta en Cloudinary para organizar tus imágenes
            Folder = "productos"
        };

        // Realizamos la subida
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
        {
            return BadRequest(uploadResult.Error.Message);
        }

        // Devolvemos un objeto JSON con la URL segura de la imagen
        return Ok(new { url = uploadResult.SecureUrl.ToString() });
    }

    [HttpPost("from-bytes")]
    public async Task<IActionResult> PostFromBytes([FromBody] FileUploadModel model)
    {
        if (model == null || string.IsNullOrEmpty(model.FileContentBase64))
        {
            return BadRequest("No se ha proporcionado contenido de archivo.");
        }

        // Convertir la cadena Base64 de nuevo a un array de bytes
        var fileBytes = Convert.FromBase64String(model.FileContentBase64);

        // Usar un MemoryStream para que el SDK de Cloudinary pueda leer los bytes
        using var stream = new MemoryStream(fileBytes);

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(model.FileName, stream),
            Folder = "productos" // Carpeta en Cloudinary
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
        {
            return BadRequest(uploadResult.Error.Message);
        }

        return Ok(new { url = uploadResult.SecureUrl.ToString() });
    }
}
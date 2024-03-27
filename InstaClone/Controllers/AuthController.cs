using InstaClone.Commons.Interfaces.IServices;
using InstaClone.Commons.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaClone.Controllers
{
    /// <summary>
    /// Controlador de Autenticación
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService service; 

        public AuthController(IAuthService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Login para el usuario
        /// </summary>
        /// <param name="request">Usuario y contraseña</param>
        /// <param name="cancellationToken">Token de Cancelación</param>
        /// <returns>Token y Expiración</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.LoginAsync(request, cancellationToken));
        }


    }
}

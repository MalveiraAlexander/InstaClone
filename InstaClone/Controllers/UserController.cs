using InstaClone.Commons.Filters;
using InstaClone.Commons.Interfaces.IServices;
using InstaClone.Commons.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InstaClone.Controllers
{
    /// <summary>
    /// Controlador de usuarios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        private int UserId => int.Parse(User.Claims.First(i => i.Type == "userId").Value);

        public UserController(IUserService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Crea un usuario
        /// </summary>
        /// <param name="request">Usuario a crear</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Usuario creado</returns>
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.CreateAsync(request, cancellationToken));
        }

        /// <summary>
        /// Actualiza un usuario
        /// </summary>
        /// <param name="request">Usuario a actualizar</param>
        /// <param name="id">Id de usuario</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Usuario actualizado</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UserRequest request, [FromRoute] int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.UpdateAsync(request, id, cancellationToken));
        }

        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="id">Id de usuario</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Usuario eliminado</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromRoute] int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.DeleteAsync(id, cancellationToken));
        }

        /// <summary>
        /// Obtiene un usuario por id
        /// </summary>
        /// <param name="id">Id de Usuario</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Usuario</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById([FromRoute] int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.GetByIdAsync(id, cancellationToken));
        }

        /// <summary>
        /// Obtiene un usuario por id almacenado en token
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Usuario</returns>
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetUserMe(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.GetByIdAsync(UserId, cancellationToken));
        }

        /// <summary>
        /// Obtiene una lista de usuarios en base a filtros
        /// </summary>
        /// <param name="filter">Filtros</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Usuarios</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserFilter filter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.GetAllAsync(filter, cancellationToken));
        }


        /// <summary>
        /// Agrega un seguidor a un usuario
        /// </summary>
        /// <param name="followerId">Id de usuario a seguir</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>bool</returns>
        [HttpPost("follow")]
        [Authorize]
        public async Task<IActionResult> ReactPost([FromQuery][Required]int followerId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.AddFollow(UserId, followerId, cancellationToken));
        }

        /// <summary>
        /// Elimina un seguidor a un usuario
        /// </summary>
        /// <param name="followerId">Id de usuario a dejar seguir</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>bool</returns>
        [HttpDelete("unfollow")]
        [Authorize]
        public async Task<IActionResult> UnreactPost([FromQuery][Required] int followerId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.RemoveFollow(UserId, followerId, cancellationToken));
        }
    }
}

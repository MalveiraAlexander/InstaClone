using InstaClone.Commons.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaClone.Controllers
{
    /// <summary>
    /// Controlador de Tipo de Reacciones
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionTypeController : ControllerBase
    {
        private readonly IReactionTypeService service;

        public ReactionTypeController(IReactionTypeService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Obtiene todos los tipos de reacciones
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Tipo de Reacciones</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.GetAllAsync(cancellationToken));
        }

        /// <summary>
        /// Obtiene un tipo de reacción por su id
        /// </summary>
        /// <param name="id">Id del Tipo de Reacción</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Tipo de Reacción</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.GetByIdAsync(id, cancellationToken));
        }
    }
}

using InstaClone.Commons.Filters;
using InstaClone.Commons.Interfaces.IServices;
using InstaClone.Commons.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaClone.Controllers
{
    /// <summary>
    /// Controlador de comentarios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService service;
        private int UserId => int.Parse(User.Claims.First(i => i.Type == "userId").Value);

        public CommentController(ICommentService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Agrega un nuevo comentario a un post
        /// </summary>
        /// <param name="request">Comentario</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Comentario creado</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] CommentRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            request.UserId = UserId;
            return Ok(await service.CreateAsync(request, cancellationToken));
        }

        /// <summary>
        /// Actualiza un comentario
        /// </summary>
        /// <param name="request">Comentario</param>
        /// <param name="id">Id del comentario</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Comentario actualizado</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateComment([FromBody] CommentRequest request, [FromRoute] Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.UpdateAsync(request, id, cancellationToken));
        }

        /// <summary>
        /// Borra un comentario
        /// </summary>
        /// <param name="id">Id del comentario</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Comentario Borrado</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.DeleteAsync(id, cancellationToken));
        }

        /// <summary>
        /// Obtiene un comentario por su id
        /// </summary>
        /// <param name="id">Id del comentario</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Comentario</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetCommentById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.GetByIdAsync(id, cancellationToken));
        }

        /// <summary>
        /// Obtiene una lista de comentarios con filtros
        /// </summary>
        /// <param name="filter">Filtros</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Comentarios</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllComments([FromQuery] CommentFilter filter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.GetAllAsync(filter, cancellationToken));
        }
    }
}

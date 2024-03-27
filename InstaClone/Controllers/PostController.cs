using InstaClone.Commons.Filters;
using InstaClone.Commons.Interfaces.IServices;
using InstaClone.Commons.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaClone.Controllers
{
    /// <summary>
    /// Controlador de posts
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService service;
        private int UserId => int.Parse(User.Claims.First(i => i.Type == "userId").Value);

        public PostController(IPostService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Agrega un nuevo post
        /// </summary>
        /// <param name="request">Post a crear</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Post creado</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPost([FromBody] PostRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            request.UserId = UserId;
            return Ok(await service.CreateAsync(request, cancellationToken));
        }

        /// <summary>
        /// Actualiza un post
        /// </summary>
        /// <param name="request">Post a actualizar</param>
        /// <param name="id">Id del post</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Post actualizado</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePost([FromBody] PostRequest request, [FromRoute] Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            request.UserId = UserId;
            return Ok(await service.UpdateAsync(request, id, cancellationToken));
        }

        /// <summary>
        /// Elimina un post
        /// </summary>
        /// <param name="id">Id del post</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Post eliminado</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.DeleteAsync(id, cancellationToken));
        }

        /// <summary>
        /// Obtiene un post por su id
        /// </summary>
        /// <param name="id">Id del post</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Post</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetPostById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.GetByIdAsync(id, cancellationToken));
        }

        /// <summary>
        /// Obtiene todos los posts en base a filtros
        /// </summary>
        /// <param name="filter">Filtros</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Posts</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPosts([FromQuery] PostFilter filter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.GetAllAsync(filter, cancellationToken));
        }

        /// <summary>
        /// Agrega una reacción a un post por usuario
        /// </summary>
        /// <param name="id">Id del post</param>
        /// <param name="request">Reacción a agregar</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>bool</returns>
        [HttpPost("{id}/react")]
        [Authorize]
        public async Task<IActionResult> ReactPost([FromRoute] Guid id, PostReactionRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            request.UserId = UserId;
            request.PostId = id;
            return Ok(await service.ReactAsync(request, cancellationToken));
        }

        /// <summary>
        /// Elimina una reacción de un post por usuario
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>bool</returns>
        [HttpDelete("unreact")]
        [Authorize]
        public async Task<IActionResult> UnreactPost(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(await service.UnReactAsync(UserId, cancellationToken));
        }
    }
}

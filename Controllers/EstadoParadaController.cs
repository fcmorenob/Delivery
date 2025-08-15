using Bussines.Repository.Interfaces;
using Core.Dtos;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers
{
    /// <summary>
    /// controlador de cliente
    /// </summary>
    [Route("api/[controller]")]
    public class EstadoParadaController : Controller
    {
        private readonly IUnitWork unitWork;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_unitWork"></param>
        public EstadoParadaController(IUnitWork _unitWork)
        {
            unitWork = _unitWork;
        }

        [HttpGet]
        public async Task<ActionResult<Response>> Get([FromQuery] string? filter, string? orderby, string? includeProperties, int? pageNumber, int? pagesize)
        {
            var estadoparada = await unitWork.EstadoParadaRepository.GetAll(filter, orderby, includeProperties, Convert.ToInt32(pageNumber), Convert.ToInt32(pagesize));

            if (estadoparada.data == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            return Ok(new Response { success = true, data = new { estadoparada.data, estadoparada.count, estadoparada.pagesize, estadoparada.pageNumber }, message = "ok", error = "", code = ErrorCode.ok });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> getbyid(long id)
        {
            var estadoparada = await unitWork.EstadoParadaRepository.GetFirstOrDefault(x => x.EstadoParadaId == id);

            if (estadoparada == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            return Ok(new Response { success = true, data = estadoparada, message = "ok", error = "", code = ErrorCode.ok });
        }

        
        [HttpPost]
        public async Task<ActionResult<Response>> post(EstadoParada estadoParada)
        {
            if (estadoParada == null)
                return NoContent();
            try
            {
                await unitWork.EstadoParadaRepository.Add(estadoParada);
                unitWork.Save();
                return Ok(new Response { success = true, data = null, message = "ok", error = "", code = ErrorCode.ok });
            }
            catch (Exception ex)
            {
                return Problem((new Response { success = true, data = null, message = "", error = ex.Message, code = ErrorCode.ServerError }).ToString());
            }
        }

       
        [HttpPatch]
        public async Task<ActionResult<Response>> Update(EstadoParada estadoParada)
        {
            if (estadoParada == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });
            try
            {
                var tabup = await unitWork.EstadoParadaRepository.GetFirstOrDefault(x => x.EstadoParadaId == estadoParada.EstadoParadaId);
                unitWork.EstadoParadaRepository.Update(estadoParada, tabup);
                unitWork.Save();
                return Ok(new Response { success = true, data = null, message = "ok", error = "", code = ErrorCode.ok });
            }
            catch (Exception ex)
            {
                return Problem((new Response { success = true, data = null, message = "", error = ex.Message, code = ErrorCode.ServerError }).ToString());
            }
        }

        
        [HttpDelete]
        public async Task<ActionResult<Response>> Delete(Cliente tabdepto)
        {
            if (tabdepto == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            try
            {
                await unitWork.ClienteRepository.Remove(tabdepto);
                unitWork.Save();
                return Ok(new Response { success = true, data = null, message = "ok", error = "", code = ErrorCode.ok });
            }
            catch (Exception ex)
            {
                return Problem((new Response { success = true, data = null, message = "", error = ex.Message, code = ErrorCode.ServerError }).ToString());
            }
        }

    }
}

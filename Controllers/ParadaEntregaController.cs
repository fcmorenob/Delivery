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
    public class ParadaEntregaController : Controller
    {
        private readonly IUnitWork unitWork;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_unitWork"></param>
        public ParadaEntregaController(IUnitWork _unitWork)
        {
            unitWork = _unitWork;
        }

        [HttpGet]
        public async Task<ActionResult<Response>> Get([FromQuery] string? filter, string? orderby, string? includeProperties, int? pageNumber, int? pagesize)
        {
            var paradaEntrega = await unitWork.ParadaEntregaRepository.GetAll(filter, orderby, includeProperties, Convert.ToInt32(pageNumber), Convert.ToInt32(pagesize));

            if (paradaEntrega.data == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            return Ok(new Response { success = true, data = new { paradaEntrega.data, paradaEntrega.count, paradaEntrega.pagesize, paradaEntrega.pageNumber }, message = "ok", error = "", code = ErrorCode.ok });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> Getbyid(long id)
        {
            var paradaEntrega = await unitWork.ParadaEntregaRepository.GetFirstOrDefault(x => x.ParadaId == id);

            if (paradaEntrega == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            return Ok(new Response { success = true, data = paradaEntrega, message = "ok", error = "", code = ErrorCode.ok });
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post(ParadaEntrega paradaEntrega)
        {
            if (paradaEntrega == null)
                return NoContent();
            try
            {
                await unitWork.ParadaEntregaRepository.Add(paradaEntrega);
                unitWork.Save();
                return Ok(new Response { success = true, data = null, message = "ok", error = "", code = ErrorCode.ok });
            }
            catch (Exception ex)
            {
                return Problem((new Response { success = true, data = null, message = "", error = ex.Message, code = ErrorCode.ServerError }).ToString());
            }
        }

       
        [HttpPatch]
        public async Task<ActionResult<Response>> Update(ParadaEntrega paradaEntrega)
        {
            if (paradaEntrega == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });
            try
            {
                var tabup = await unitWork.ParadaEntregaRepository.GetFirstOrDefault(x => x.ParadaId == paradaEntrega.ParadaId);
                unitWork.ParadaEntregaRepository.Update(paradaEntrega, tabup);
                unitWork.Save();
                return Ok(new Response { success = true, data = null, message = "ok", error = "", code = ErrorCode.ok });
            }
            catch (Exception ex)
            {
                return Problem((new Response { success = true, data = null, message = "", error = ex.Message, code = ErrorCode.ServerError }).ToString());
            }
        }

       
        [HttpDelete]
        public async Task<ActionResult<Response>> Delete(ParadaEntrega paradaEntrega)
        {
            if (paradaEntrega == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            try
            {
                await unitWork.ParadaEntregaRepository.Remove(paradaEntrega);
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

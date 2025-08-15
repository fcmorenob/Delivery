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
    public class PedidoDetalleController : Controller
    {
        private readonly IUnitWork unitWork;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_unitWork"></param>
        public PedidoDetalleController(IUnitWork _unitWork)
        {
            unitWork = _unitWork;
        }

        [HttpGet]
        public async Task<ActionResult<Response>> Get([FromQuery] string? filter, string? orderby, string? includeProperties, int? pageNumber, int? pagesize)
        {
            var pedidoDetalle = await unitWork.PedidoDetalleRepository.GetAll(filter, orderby, includeProperties, Convert.ToInt32(pageNumber), Convert.ToInt32(pagesize));

            if (pedidoDetalle.data == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            return Ok(new Response { success = true, data = new { pedidoDetalle.data, pedidoDetalle.count, pedidoDetalle.pagesize, pedidoDetalle.pageNumber }, message = "ok", error = "", code = ErrorCode.ok });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> Getbyid(long id)
        {
            var pedidoDetalle = await unitWork.PedidoDetalleRepository.GetFirstOrDefault(x => x.PedidoDetalleId == id);

            if (pedidoDetalle == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            return Ok(new Response { success = true, data = pedidoDetalle, message = "ok", error = "", code = ErrorCode.ok });
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post(PedidoDetalle pedidoDetalle)
        {
            if (pedidoDetalle == null)
                return NoContent();
            try
            {
                await unitWork.PedidoDetalleRepository.Add(pedidoDetalle);
                unitWork.Save();
                return Ok(new Response { success = true, data = null, message = "ok", error = "", code = ErrorCode.ok });
            }
            catch (Exception ex)
            {
                return Problem((new Response { success = true, data = null, message = "", error = ex.Message, code = ErrorCode.ServerError }).ToString());
            }
        }

       
        [HttpPatch]
        public async Task<ActionResult<Response>> Update(PedidoDetalle pedidoDetalle)
        {
            if (pedidoDetalle == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });
            try
            {
                var tabup = await unitWork.PedidoDetalleRepository.GetFirstOrDefault(x => x.PedidoDetalleId == pedidoDetalle.PedidoDetalleId);
                unitWork.PedidoDetalleRepository.Update(pedidoDetalle, tabup);
                unitWork.Save();
                return Ok(new Response { success = true, data = null, message = "ok", error = "", code = ErrorCode.ok });
            }
            catch (Exception ex)
            {
                return Problem((new Response { success = true, data = null, message = "", error = ex.Message, code = ErrorCode.ServerError }).ToString());
            }
        }

       
        [HttpDelete]
        public async Task<ActionResult<Response>> Delete(PedidoDetalle pedidoDetalle)
        {
            if (pedidoDetalle == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            try
            {
                await unitWork.PedidoDetalleRepository.Remove(pedidoDetalle);
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

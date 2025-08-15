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
    public class EstadoPedidoController : Controller
    {
        private readonly IUnitWork unitWork;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_unitWork"></param>
        public EstadoPedidoController(IUnitWork _unitWork)
        {
            unitWork = _unitWork;
        }

        [HttpGet]
        public async Task<ActionResult<Response>> Get([FromQuery] string? filter, string? orderby, string? includeProperties, int? pageNumber, int? pagesize)
        {
            var estadosPedido = await unitWork.EstadoPedidoRepository.GetAll(filter, orderby, includeProperties, Convert.ToInt32(pageNumber), Convert.ToInt32(pagesize));

            if (estadosPedido.data == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            return Ok(new Response { success = true, data = new { estadosPedido.data, estadosPedido.count, estadosPedido.pagesize, estadosPedido.pageNumber }, message = "ok", error = "", code = ErrorCode.ok });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> Getbyid(long id)
        {
            var estadosPedido = await unitWork.EstadoPedidoRepository.GetFirstOrDefault(x => x.EstadoPedidoId == id);

            if (estadosPedido == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            return Ok(new Response { success = true, data = estadosPedido, message = "ok", error = "", code = ErrorCode.ok });
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post(EstadoPedido estadoPedido)
        {
            if (estadoPedido == null)
                return NoContent();
            try
            {
                await unitWork.EstadoPedidoRepository.Add(estadoPedido);
                unitWork.Save();
                return Ok(new Response { success = true, data = null, message = "ok", error = "", code = ErrorCode.ok });
            }
            catch (Exception ex)
            {
                return Problem((new Response { success = true, data = null, message = "", error = ex.Message, code = ErrorCode.ServerError }).ToString());
            }
        }

        
        [HttpPatch]
        public async Task<ActionResult<Response>> Update(EstadoPedido estadoPedido)
        {
            if (estadoPedido == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });
            try
            {
                var tabup = await unitWork.EstadoPedidoRepository.GetFirstOrDefault(x => x.EstadoPedidoId == estadoPedido.EstadoPedidoId);
                unitWork.EstadoPedidoRepository.Update(estadoPedido, tabup);
                unitWork.Save();
                return Ok(new Response { success = true, data = null, message = "ok", error = "", code = ErrorCode.ok });
            }
            catch (Exception ex)
            {
                return Problem((new Response { success = true, data = null, message = "", error = ex.Message, code = ErrorCode.ServerError }).ToString());
            }
        }

      
        [HttpDelete]
        public async Task<ActionResult<Response>> Delete(EstadoPedido estadoPedido)
        {
            if (estadoPedido == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            try
            {
                await unitWork.EstadoPedidoRepository.Remove(estadoPedido);
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

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
    public class ClienteController : Controller
    {
        private readonly IUnitWork unitWork;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_unitWork"></param>
        public ClienteController(IUnitWork _unitWork)
        {
            unitWork = _unitWork;
        }

        [HttpGet]
        public async Task<ActionResult<Response>> Get([FromQuery] string? filter, string? orderby, string? includeProperties, int? pageNumber, int? pagesize)
        {
            var clientes = await unitWork.ClienteRepository.GetAll(filter, orderby, includeProperties, Convert.ToInt32(pageNumber), Convert.ToInt32(pagesize));

            if (clientes.data == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            return Ok(new Response { success = true, data = new { clientes.data, clientes.count, clientes.pagesize, clientes.pageNumber }, message = "ok", error = "", code = ErrorCode.ok });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> getbyid(long id)
        {
            var clientes = await unitWork.ClienteRepository.GetFirstOrDefault(x => x.ClienteId == id);

            if (clientes == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            return Ok(new Response { success = true, data = clientes, message = "ok", error = "", code = ErrorCode.ok });
        }

       
        [HttpPost]
        public async Task<ActionResult<Response>> post(Cliente cliente)
        {
            if (cliente == null)
                return NoContent();
            try
            {
                await unitWork.ClienteRepository.Add(cliente);
                unitWork.Save();
                return Ok(new Response { success = true, data = null, message = "ok", error = "", code = ErrorCode.ok });
            }
            catch (Exception ex)
            {
                return Problem((new Response { success = true, data = null, message = "", error = ex.Message, code = ErrorCode.ServerError }).ToString());
            }
        }

       
        [HttpPatch]
        public async Task<ActionResult<Response>> Update(Cliente cliente)
        {
            if (cliente == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });
            try
            {
                var tabup = await unitWork.ClienteRepository.GetFirstOrDefault(x => x.ClienteId == cliente.ClienteId);
                unitWork.ClienteRepository.Update(cliente, tabup);
                unitWork.Save();
                return Ok(new Response { success = true, data = null, message = "ok", error = "", code = ErrorCode.ok });
            }
            catch (Exception ex)
            {
                return Problem((new Response { success = true, data = null, message = "", error = ex.Message, code = ErrorCode.ServerError }).ToString());
            }
        }

        [NonAction]
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

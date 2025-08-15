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
    public class ConductorController : Controller
    {
        private readonly IUnitWork unitWork;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_unitWork"></param>
        public ConductorController(IUnitWork _unitWork)
        {
            unitWork = _unitWork;
        }

        [HttpGet]
        public async Task<ActionResult<Response>> Get([FromQuery] string? filter, string? orderby, string? includeProperties, int? pageNumber, int? pagesize)
        {
            var conductor = await unitWork.ConductorRepository.GetAll(filter, orderby, includeProperties, Convert.ToInt32(pageNumber), Convert.ToInt32(pagesize));

            if (conductor.data == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            return Ok(new Response { success = true, data = new { conductor.data, conductor.count, conductor.pagesize, conductor.pageNumber }, message = "ok", error = "", code = ErrorCode.ok });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> getbyid(long id)
        {
            var conductor = await unitWork.ConductorRepository.GetFirstOrDefault(x => x.ConductorId == id);

            if (conductor == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            return Ok(new Response { success = true, data = conductor, message = "ok", error = "", code = ErrorCode.ok });
        }

       
        [HttpPost]
        public async Task<ActionResult<Response>> post(Conductor conductor)
        {
            if (conductor == null)
                return NoContent();
            try
            {
                await unitWork.ConductorRepository.Add(conductor);
                unitWork.Save();
                return Ok(new Response { success = true, data = null, message = "ok", error = "", code = ErrorCode.ok });
            }
            catch (Exception ex)
            {
                return Problem((new Response { success = true, data = null, message = "", error = ex.Message, code = ErrorCode.ServerError }).ToString());
            }
        }

        
        [HttpPatch]
        public async Task<ActionResult<Response>> Update(Conductor conductor)
        {
            if (conductor == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });
            try
            {
                var tabup = await unitWork.ConductorRepository.GetFirstOrDefault(x => x.ConductorId == conductor.ConductorId);
                unitWork.ConductorRepository.Update(conductor, tabup);
                unitWork.Save();
                return Ok(new Response { success = true, data = null, message = "ok", error = "", code = ErrorCode.ok });
            }
            catch (Exception ex)
            {
                return Problem((new Response { success = true, data = null, message = "", error = ex.Message, code = ErrorCode.ServerError }).ToString());
            }
        }

        
        [HttpDelete]
        public async Task<ActionResult<Response>> Delete(Conductor conductor)
        {
            if (conductor == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            try
            {
                await unitWork.ConductorRepository.Remove(conductor);
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

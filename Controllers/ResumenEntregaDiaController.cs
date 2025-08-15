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
    public class ResumenEntregaDiaController : Controller
    {
        private readonly IUnitWork unitWork;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_unitWork"></param>
        public ResumenEntregaDiaController(IUnitWork _unitWork)
        {
            unitWork = _unitWork;
        }

        [HttpGet]
        public async Task<ActionResult<Response>> Get([FromQuery] string? filter, string? orderby, string? includeProperties, int? pageNumber, int? pagesize)
        {
            var resumen = await unitWork.VwResumenEntregaDiasRepository.GetAll(filter, orderby, includeProperties, Convert.ToInt32(pageNumber), Convert.ToInt32(pagesize));

            if (resumen.data == null)
                return NotFound(new Response { success = false, data = null, message = "", error = "No hay resultados", code = ErrorCode.NotFound });

            return Ok(new Response { success = true, data = new { resumen.data, resumen.count, resumen.pagesize, resumen.pageNumber }, message = "ok", error = "", code = ErrorCode.ok });
        }


    }
}

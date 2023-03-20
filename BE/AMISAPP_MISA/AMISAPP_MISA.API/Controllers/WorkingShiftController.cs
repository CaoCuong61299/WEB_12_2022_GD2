using AMISAPP_MISA.BL.WorkingShiftBL;
using AMISAPP_MISA.Common.Entities;
using AMISAPP_MISA.Common.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMISAPP_MISA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingShiftController : BasesController<WorkingShift>
    {
        #region Fiel
        
        private IWorkingShiftBL _workingShiftBL;

        #endregion

        #region Contructor

        public WorkingShiftController(IWorkingShiftBL workingShiftBL) : base(workingShiftBL)
        {
            _workingShiftBL = workingShiftBL;
        }

        #endregion

        /// <summary>
        /// Hàm export file excel
        /// </summary>
        /// <param name="cancellationToken">theo dõi trạng thái của Token hiện tại</param>
        /// <returns>File excel</returns>
        /// Author : NCCONG (09/03/2023)
        [HttpGet("export")]
        public IActionResult GetEmployeeExcel(CancellationToken cancellationToken)
        {
            Task.Yield();
            var stream = _workingShiftBL.ExportExcel();
            string excelName = $"WorkingShift-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        /// <summary>
        /// Hàm Paging
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="workingShiftFilter"></param>
        /// <param name="whereSql"></param>
        /// <returns>ListPaging</returns>
        /// Author: NCCONG(19/03/2022)

        [HttpPost("paging")]
        public IActionResult Paging([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1, [FromQuery] string? workingShiftFilter = null, List<CustomParam> customParam = null)
        {
            var result = _workingShiftBL.Paging(pageSize, pageNumber, workingShiftFilter,customParam);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}

using AMISAPP_MISA.BL.ColumnTableBL;
using AMISAPP_MISA.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMISAPP_MISA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableColumnController : ControllerBase
    {
        private IColumnTableBL _columnTableBL;
        public TableColumnController(IColumnTableBL columnTableBL)
        {
            _columnTableBL= columnTableBL;
        }
        [HttpGet]
        public IActionResult GetALl()
        {
            var result = _columnTableBL.GetAll();
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpPost] 
        public IActionResult InsertColumnTable(List<ColumnTable> columns) 
        {
            var result = _columnTableBL.InsertColumnTable(columns);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpDelete]
        public IActionResult DeleteColumnTable()
        {
            var result = _columnTableBL.DeleteColumnTable();
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}

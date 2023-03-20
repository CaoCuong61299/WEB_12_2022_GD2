using AMISAPP_MISA.BL.BaseBL;
using AMISAPP_MISA.Common.Entities.DTO;
using AMISAPP_MISA.Common.Resources;
using AMISAPP_MISA.Common.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMISAPP_MISA.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Contructor

        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API lấy toàn bộ bản ghi trong 1 bảng
        /// </summary>
        /// <returns>Danh sách bản ghi trong 1 bảng</returns>
        ///  Author: NCCONG (09/03/2023)
        [HttpGet]
        public IActionResult GetAllRecord()
        {
            try
            {
                var result = _baseBL.GetAllRecord();
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                Common.Enums.ErrorCode.Exception,
                Resource.DevMsg_Exception,
                Resource.UserMsg_Exception,
                Resource.MoreInfo_Exception,
                HttpContext.TraceIdentifier));
            }
        }


        /// <summary>
        /// API lấy thông tin chi tiết một bản ghi
        /// </summary>
        /// <param name="recordId">Id bản ghi</param>
        /// <returns>lấy thông tin chi tiết một bản ghi</returns>
        ///  Author: NCCONG (09/03/2023)
        [HttpGet("{recordId}")]
        public IActionResult GetRecordById(Guid recordId)
        {
            try
            {
                var result = _baseBL.GetRecordById(recordId);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                Common.Enums.ErrorCode.Exception,
                Resource.DevMsg_Exception,
                Resource.UserMsg_Exception,
                Resource.MoreInfo_Exception,
                HttpContext.TraceIdentifier));
            }
        }

        /// <summary>
        /// Hàm phân trang và tìm kiếm
        /// </summary>
        /// <param name="pageSize">số bản ghi</param>
        /// <param name="pageNumber">số trang</param>
        /// <param name="workingShiftFilter">từ khóa để tìm kiếm</param>
        /// <returns>số bản ghi có trong điều kiện</returns>
        /// Author: NCCONG(09/03/2023)
        [HttpPost("pagingAndFilter")]
        public IActionResult GetRecordByPagingAndFilter(List<CustomParam>? customParams,int pageSize = 10, int pageNumber = 1, string? workingShiftFilter = null)
        {
            try
            {
                var result = _baseBL.GetPagingAndFilter(pageSize, pageNumber, workingShiftFilter,customParams);

                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, result);

                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                Common.Enums.ErrorCode.Exception,
                Resource.DevMsg_Exception,
                Resource.UserMsg_Exception,
                Resource.MoreInfo_Exception,
                HttpContext.TraceIdentifier));
            }
        }

        /// <summary>
        /// Lấy employee by id
        /// </summary>
        /// <param name="recordIds">employeeId</param>
        /// <returns></returns>
        /// Author: NCCONG(09/03/2023)
        [HttpPost("listRecordIds")]
        public virtual IActionResult GetRecordsById(List<Guid> recordIds)
        {
            try
            {
                var result = _baseBL.GetRecordsById(recordIds);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                Common.Enums.ErrorCode.Exception,
                Resource.DevMsg_Exception,
                Resource.UserMsg_Exception,
                Resource.MoreInfo_Exception,
                HttpContext.TraceIdentifier));
            }
        }

        /// <summary>
        /// API thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Đối tượng cần thêm mới</param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        ///  Author: NCCONG (09/03/2023)
        [HttpPost]
        public virtual IActionResult InsertRecord([FromBody] T record)
        {
            try
            {
                var result = _baseBL.InsertRecord(record);

                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status201Created, result.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                    Common.Enums.ErrorCode.InsertFailed,
                    Resource.DevMsg_InsertFailed,
                    Resource.UserMsg_InsertFailed,
                    result.Data,
                    HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                Common.Enums.ErrorCode.Exception,
                Resource.DevMsg_Exception,
                Resource.UserMsg_Exception,
                Resource.MoreInfo_Exception,
                HttpContext.TraceIdentifier));
            }
        }


        /// <summary>
        /// API thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Đối tượng cần thêm mới</param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        ///  Author: NCCONG (09/03/2023)
        [HttpPut("{recordId}")]
        public virtual IActionResult UpdateRecord([FromRoute] Guid recordId, [FromBody] T record)
        {
            try
            {
                var result = _baseBL.UpdateRecord(recordId, record);

                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status200OK, result.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                    Common.Enums.ErrorCode.UpdateFailed,
                    Resource.DevMsg_UpdateFailed,
                    Resource.UserMsg_UpdateFailed,
                    result.Data,
                    HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                Common.Enums.ErrorCode.Exception,
                Resource.DevMsg_Exception,
                Resource.UserMsg_Exception,
                Resource.MoreInfo_Exception,
                HttpContext.TraceIdentifier));
            }
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="recordIds">Id bản ghi cần xóa</param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        /// Author: NCCong (09/03/2023)
        [HttpDelete("recordIds")]
        public virtual IActionResult DeleteRecord(List<Guid> recordIds)
        {
            try
            {
                var result = _baseBL.DeleteRecord(recordIds);

                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent, new ErrorResult(
                    Common.Enums.ErrorCode.DeleteFailed,
                    Resource.DevMsg_DeleteFailed,
                    Resource.UserMsg_DeleteFailed,
                    Resource.MoreInfo_DeleteFailed,
                    HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                Common.Enums.ErrorCode.Exception,
                Resource.DevMsg_Exception,
                Resource.UserMsg_Exception,
                Resource.MoreInfo_Exception,
                HttpContext.TraceIdentifier));
            }
        }


        /// <summary>
        /// Hàm xóa 1 bản ghi
        /// </summary>
        /// <param name="recordId">Id bản ghi</param>
        /// <returns>số bản đã xóa</returns>
        /// Author: NCCONG(09/03/2023)
        [HttpDelete("{recordId}")]
        public virtual IActionResult DeleteOneRecord([FromRoute] Guid recordId)
        {
            try
            {
                int numberOfAffectedRows = _baseBL.DeleteOneRecord(recordId);
                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, numberOfAffectedRows);
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent, new ErrorResult(
                    Common.Enums.ErrorCode.DeleteFailed,
                    Resource.DevMsg_DeleteFailed,
                    Resource.UserMsg_DeleteFailed,
                    Resource.MoreInfo_DeleteFailed,
                    HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                Common.Enums.ErrorCode.Exception,
                Resource.DevMsg_Exception,
                Resource.UserMsg_Exception,
                Resource.MoreInfo_Exception,
                HttpContext.TraceIdentifier));
            }
        }
        #endregion
    }
}

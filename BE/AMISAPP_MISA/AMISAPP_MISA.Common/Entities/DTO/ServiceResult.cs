using AMISAPP_MISA.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISAPP_MISA.Common.Entities.DTO
{
    public class ServiceResult
    {
        #region property

        /// <summary>
        /// Biến kiểm tra thành công : True thành công, False thất bại
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCode? ErrorCode { get; set; }

        /// <summary>
        /// Dữ liệu đi kèm khi thành công hoặc thất bại 
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Lỗi trả về
        /// </summary>
        public string? Message { get; set; }

        #endregion
    }
}

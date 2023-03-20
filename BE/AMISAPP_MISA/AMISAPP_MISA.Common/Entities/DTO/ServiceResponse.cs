using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISAPP_MISA.Common.Entities.DTO
{
    public class ServiceResponse
    {
        #region property

        /// <summary>
        /// Thành công hay thất bại
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Dữ liệu đi kèm khi thành công hoặc thất bại
        /// </summary>
        public object Data { get; set; }

        #endregion
    }
}

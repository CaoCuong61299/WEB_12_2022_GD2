using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISAPP_MISA.Common.Entities.DTO
{
    public class PagingResult<T>
    {
        #region property

        /// <summary>
        /// Tổng số Trang
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Tổng số bản ghi hiện tại
        /// </summary>
        public int CurrentPageRecords { get; set; }

        /// <summary>
        /// trả về dữ liệu
        /// </summary>
        public List<T> Data { get; set; }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISAPP_MISA.Common.Constants
{
    public class ProcedureName
    {
        /// <summary>
        /// Proceduce lấy tất cả
        /// </summary>
        public static string GetAll = "Proc_WorkingShift_GetAll";

        /// <summary>
        /// Proceduce lấy theo id
        /// </summary>
        public static string GetById = "Proc_WorkingShift_GetById";

        /// <summary>
        /// Proceduce thêm
        /// </summary>
        public static string Insert = "Proc_WorkingShift_Insert";

        /// <summary>
        /// Proceduce sửa
        /// </summary>
        public static string Update = "Proc_WorkingShift_Update";

        /// <summary>
        /// Proceduce xóa
        /// </summary>
        public static string Delete = "Proc_WorkingShift_Delete";

        /// <summary>
        /// Proceduce kiểm tra trùng mã
        /// </summary>
        public static string GetCheckCode = "Proc_WorkingShift_GetCheckCode";

        /// <summary>
        /// Proceduce phân trang và tìm kiếm
        /// </summary>
        public static string PagingAndFilter = "Proc_WorkingShift_PageAndFilter";

        public static string GetAllColumnTable = "Proc_ColumnTable_GetAll";

        public static string PagingAndFilterTest = "Proc_WorkingShift_PageAndFilterTest";
    }
}

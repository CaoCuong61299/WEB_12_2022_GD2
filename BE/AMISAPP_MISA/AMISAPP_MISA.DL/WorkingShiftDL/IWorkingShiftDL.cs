using AMISAPP_MISA.Common.Entities;
using AMISAPP_MISA.Common.Entities.DTO;
using AMISAPP_MISA.Common.ViewModel;
using AMISAPP_MISA.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISAPP_MISA.DL.WorkingShiftDL
{
    public interface IWorkingShiftDL: IBaseDL<WorkingShift>
    {
        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Author: NCCONG(09/03/2023)
        List<WorkingShift> GetAll();

        /// <summary>
        /// Hàm kiểm tra trùng mã
        /// </summary>
        /// <param name="workingShiftCode">đôi tượng</param>
        /// <returns>id bản ghi</returns>
        object GetCheckCode(string workingShiftCode);

        /// <summary>
        /// hàm lấy bản ghi để xuất file excel
        /// </summary>
        /// <returns></returns>
        dynamic ExportExcel();

        /// <summary>
        /// Hàm Paging
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="workingShiftFilter"></param>
        /// <param name="whereSql"></param>
        /// <returns>ListPaging</returns>
        /// Author: NCCONG(19/03/2022)

        PagingResult<WorkingShift> Paging(int pageSize, int pageNumber, string workingShiftFilter,string whereSql);

    }
}

using AMISAPP_MISA.BL.BaseBL;
using AMISAPP_MISA.Common.Entities;
using AMISAPP_MISA.Common.Entities.DTO;
using AMISAPP_MISA.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISAPP_MISA.BL.WorkingShiftBL
{
    public interface IWorkingShiftBL:IBaseBL<WorkingShift>
    {
        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Author: NCCONG(09/03/2023)
        List<WorkingShift> GetAll();

        /// <summary>
        /// Hàm export excel
        /// </summary>
        /// <returns>File excel</returns>
        /// Author: NCCONG(09/03/2023)
        dynamic ExportExcel();

        PagingResult<WorkingShift> Paging(int pageSize, int pageNumber, string workingShiftFilter,List<CustomParam> customParam);

    }
}

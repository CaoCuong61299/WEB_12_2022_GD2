using AMISAPP_MISA.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISAPP_MISA.DL.BaseDL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Hàm lấy toàn bộ bản ghi
        /// </summary>
        /// <returns>toàn bộ bản ghi</returns>
        /// Author : NCCONG (08/03/2023)
        dynamic GetAllRecord();

        /// <summary>
        /// Hàm lấy thông tin bản ghi theo Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>1 bản ghi</returns>
        /// Author : NCCONG (08/03/2023)
        dynamic GetRecordById(Guid recordId);

        /// <summary>
        /// Hàm lấy id theo list Id
        /// </summary>
        /// <param name="recordIds">list id</param>
        /// <returns>Trả về tất cả các bản ghi trong list id</returns>
        /// Author: NCCONG(09/03/2023)
        List<T> GetRecordsById(List<Guid> recordIds);

        /// <summary>
        /// Hàm thêm mới 1 bản ghi 
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm </param>
        /// <returns> Trả về 1 nếu insert thành công trả về 0 nếu insert thất bại</returns>
        /// Author : NCCONG (08/03/2023)
        int InsertRecord(T record);

        /// <summary>
        /// Hàm sửa 1 bản ghi 
        /// </summary>
        /// <param name="recordId">Id bản ghi cần sửa</param>
        /// <param name="record">Bản ghi muốn sửa </param>
        /// <returns> Trả về 1 nếu insert thành công trả về 0 nếu insert thất bại</returns>
        /// Author : NCCONG (08/03/2023)
        int UpdateRecord(Guid recordId, T record);

        /// <summary>
        /// Hàm xóa 1 bản ghi theo Id
        /// </summary>
        /// <param name="recordId">Id bản ghi cần xóa </param>
        /// <returns>Trả về 1 nếu insert thành công trả về 0 nếu insert thất bại</returns>
        /// Author : NCCONG (08/03/2023)
        int DeleteRecord(List<Guid> recordIds);

        /// <summary>
        /// Hàm phân trang và tìm kiếm
        /// </summary>
        /// <param name="pageSize">số bản ghi</param>
        /// <param name="pageNumber">số trang</param>
        /// <param name="workingShiftFilter">từ khóa để tìm kiếm</param>
        /// <returns>số bản ghi có trong điều kiện</returns>
        /// Author: NCCONG(09/03/2023)
        PagingResult<T> GetPagingAndFilter(int pageSize,int pageNumber,string workingShiftFilter,string whereSql);

        /// <summary>
        /// Hàm xóa 1 bản ghi theo id
        /// </summary>
        /// <param name="recordId">id</param>
        /// <returns>số bản ghi đã xóa</returns>
        /// Author : NCCONG(09/03/2023)
        int DeleteOneRecord(Guid recordId);

    }
}

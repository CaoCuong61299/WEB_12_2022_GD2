using AMISAPP_MISA.Common.Constants;
using AMISAPP_MISA.Common.Entities;
using AMISAPP_MISA.Common.Entities.DTO;
using AMISAPP_MISA.Common.Resources;
using AMISAPP_MISA.DL.DataContext;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AMISAPP_MISA.Common.Attributes.AmisAttributes;

namespace AMISAPP_MISA.DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region method

        /// <summary>
        /// Hàm xóa nhiều bản ghi theo Id
        /// </summary>
        /// <param name="recordId">Id bản ghi cần xóa </param>
        /// <returns>Trả về 1 nếu insert thành công trả về 0 nếu insert thất bại</returns>
        /// Author : NCCONG (08/03/2023)
        public int DeleteRecord(List<Guid> recordIds)
        {
            string sql = Resource.Proc_WorkingShift_DeleteListId;
            int numberOfAffectedRows = 0;
            var parameters = new DynamicParameters();
            // Khởi tạo kết nối tới DB MySQL
            using (var mysqlConnection = new MySqlConnection(DataBaseContext.ConnectionString))
            {
                // Mở kết nối
                mysqlConnection.Open();

                // Thực hiện transaction
                using (var transaction = mysqlConnection.BeginTransaction())
                {
                    try
                    {
                        numberOfAffectedRows = mysqlConnection.Execute(string.Format(sql, string.Join("','", recordIds)), parameters, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
                mysqlConnection.Close();
            }
            return numberOfAffectedRows;
        }

        /// <summary>
        /// Hàm lấy id theo list Id
        /// </summary>
        /// <param name="recordIds">list id</param>
        /// <returns>Trả về tất cả các bản ghi trong list id</returns>
        /// Author: NCCONG(09/03/2023)
        public List<T> GetRecordsById(List<Guid> recordIds)
        {
            string sql = Resource.Proc_WorkingShift_GetByListId;
            List<T> records = new List<T>();
            var parameters = new DynamicParameters();
            // Khởi tạo kết nối tới DB MySQL
            using (var mysqlConnection = new MySqlConnection(DataBaseContext.ConnectionString))
            {
                // Mở kết nối
                mysqlConnection.Open();

                // Thực hiện transaction
                using (var transaction = mysqlConnection.BeginTransaction())
                {
                    try
                    {
                        var result = mysqlConnection.QueryMultiple(string.Format(sql, string.Join("','", recordIds)), parameters, transaction: transaction);
                        records = result.Read<T>().ToList();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
                mysqlConnection.Close();

            }
            return records;
        }

        /// <summary>
        /// Hàm lấy toàn bộ bản ghi
        /// </summary>
        /// <returns>toàn bộ bản ghi</returns>
        /// Author: NCCONG (09/03/2023)
        public dynamic GetAllRecord()
        {
            string storedProcedureName = string.Format(ProcedureName.GetAll);
            dynamic records;
            using (var mySqlConnection = new MySqlConnection(DataBaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                records = result.Read<T>().ToList();
            }

            return records;
        }

        /// <summary>
        /// Hàm lấy thông tin bản ghi theo Id
        /// </summary>
        /// <param name="recordId">Id</param>
        /// <returns>1 bản ghi</returns>
        /// Author: NCCONG (09/03/2023)
        public dynamic GetRecordById(Guid recordId)
        {
            string storedProcedureName = string.Format(ProcedureName.GetById);
            var parameters = new DynamicParameters();
            string propertyName = typeof(T).GetProperties()[0].Name;
            parameters.Add($"p_{propertyName}", recordId);
            dynamic getRecordById;
            using (var mySqlConnection = new MySqlConnection(DataBaseContext.ConnectionString))
            {
                getRecordById = mySqlConnection.QueryFirstOrDefault(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return getRecordById;
        }

        /// <summary>
        /// Hàm thêm mới 1 bản ghi 
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm </param>
        /// <returns> Trả về 1 nếu insert thành công trả về 0 nếu insert thất bại</returns>
        /// Author: NCCONG (09/03/2023) 
        public int InsertRecord(T record)
        {
            string storedProcedureName = string.Format(ProcedureName.Insert, typeof(T).Name);
            var parameters = new DynamicParameters();
            var newRecordID = Guid.NewGuid();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                object propertyValue;

                var primaryKeyAttribute = (PrimaryKeyAttribute?)Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute));

                if (primaryKeyAttribute != null)
                {
                    propertyValue = newRecordID;
                }
                else
                {
                    propertyValue = property.GetValue(record, null);
                }
                parameters.Add($"p_{propertyName}", propertyValue);
            }

            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DataBaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
        }

        /// <summary>
        /// Hàm sửa 1 bản ghi 
        /// </summary>
        /// <param name="recordId">Id bản ghi cần sửa</param>
        /// <param name="record">Bản ghi muốn sửa </param>
        /// <returns> Trả về 1 nếu insert thành công trả về 0 nếu insert thất bại</returns>
        /// Author: NCCONG (09/03/2023)
        public int UpdateRecord(Guid recordId, T record)
        {
            string storedProcedureName = string.Format(ProcedureName.Update, typeof(T).Name);
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                object propertyValue;

                var primaryKeyAttribute = (PrimaryKeyAttribute?)Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute));

                if (primaryKeyAttribute != null)
                {
                    propertyValue = recordId;
                }
                else
                {
                    propertyValue = property.GetValue(record, null);
                }
                parameters.Add($"p_{propertyName}", propertyValue);
            }

            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DataBaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
        }

        /// <summary>
        /// Hàm phân trang và tìm kiếm
        /// </summary>
        /// <param name="pageSize">số bản ghi</param>
        /// <param name="pageNumber">số trang</param>
        /// <param name="workingShiftFilter">từ khóa để tìm kiếm</param>
        /// <returns>số bản ghi có trong điều kiện</returns>
        /// Author: NCCONG(09/03/2023)
        public PagingResult<T> GetPagingAndFilter(int pageSize, int pageNumber, string workingShiftFilter,string whereSql)
        {
            string storedProcedureName = string.Format(ProcedureName.PagingAndFilter);

            var parameters = new DynamicParameters();
            parameters.Add("p_Limit", pageSize);
            parameters.Add("p_OffSet", pageNumber);
            parameters.Add("p_WorkingShiftFilter", workingShiftFilter);
            parameters.Add("p_Where", whereSql);

            var mySqlConnection = new MySqlConnection(DataContext.DataBaseContext.ConnectionString);
            mySqlConnection.Open();

            var getWorkingShiftFiltersFilter = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            var workingShiftFilters = getWorkingShiftFiltersFilter.Read<T>().ToList();
            int totalRecords = getWorkingShiftFiltersFilter.Read<int>().Single();
            double totalPage = getWorkingShiftFiltersFilter.Read<int>().Single();
            return new PagingResult<T>
            {
                CurrentPage = pageNumber,
                CurrentPageRecords = pageSize,
                TotalPage = Convert.ToInt32(totalPage),
                TotalRecord = totalRecords,
                Data = workingShiftFilters
            };
        }

        /// <summary>
        /// Hàm xóa 1 bản ghi theo id
        /// </summary>
        /// <param name="recordId">id</param>
        /// <returns>số bản ghi đã xóa</returns>
        /// Author : NCCONG(09/03/2023)
        public int DeleteOneRecord(Guid recordId)
        {
            string storedProcedureName = string.Format(ProcedureName.Delete, typeof(T).Name);

            var parameters = new DynamicParameters();
            string propertyName = typeof(T).GetProperties()[0].Name;
            parameters.Add($"p_{propertyName}", recordId);


            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DataContext.DataBaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
        }


        #endregion
    }
}

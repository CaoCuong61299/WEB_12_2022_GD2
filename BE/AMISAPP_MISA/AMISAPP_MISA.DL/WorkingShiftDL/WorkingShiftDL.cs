using AMISAPP_MISA.Common.Constants;
using AMISAPP_MISA.Common.Entities;
using AMISAPP_MISA.Common.Entities.DTO;
using AMISAPP_MISA.Common.Resources;
using AMISAPP_MISA.Common.ViewModel;
using AMISAPP_MISA.DL.BaseDL;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISAPP_MISA.DL.WorkingShiftDL
{
    public class WorkingShiftDL : BaseDL<WorkingShift>,IWorkingShiftDL
    {
        #region Method
        /// <summary>
        /// hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns>bản ghi</returns>
        /// Author: NCCONG(09/03/2023)
        public List<WorkingShift> GetAll()
        {
            string storedProcedureName = string.Format(ProcedureName.GetAll);
            List<WorkingShift> workingShift;
            using (var mySqlConnection = new MySqlConnection(DataContext.DataBaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                workingShift = result.Read<WorkingShift>().ToList();
            }
            return workingShift;
        }

        /// <summary>
        /// Kiểm tra trùng mã
        /// </summary>
        /// <param name="workingShiftCode">mã</param>
        /// <returns>workingShiftId</returns>
        /// Author: NCCONG(09/03/2023)
        public object GetCheckCode(string workingShiftCode)
        {
            using (var mySqlConnection = new MySqlConnection(DataContext.DataBaseContext.ConnectionString))
            {
                string storedProcedureName = String.Format(ProcedureName.GetCheckCode);

                var parameters = new DynamicParameters();
                string propertyName = typeof(WorkingShift).GetProperties()[1].Name;
                parameters.Add($"p_{propertyName}", workingShiftCode);

                var workingShift = mySqlConnection.QueryFirstOrDefault(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                if (workingShift != null)
                {
                    return workingShift.WorkingShiftId;
                }
                else
                {
                    return workingShift;
                }
            }
        }

        /// <summary>
        /// Hàm lấy bản ghi để xuất file excel
        /// </summary>
        /// <returns>số bản ghi</returns>
        /// Author: NCCONG(09/03/2023)
        public dynamic ExportExcel()
        {
            string storedProcedureName = string.Format(ProcedureName.GetAll);
            dynamic employees;
            using (var mySqlConnection = new MySqlConnection(DataContext.DataBaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                employees = result.Read<WorkingShift>().ToList();
            }

            return employees;
        }

        /// <summary>
        /// Hàm Paging
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="workingShiftFilter"></param>
        /// <param name="whereSql"></param>
        /// <returns>ListPaging</returns>
        /// Author: NCCONG(19/03/2022)
        public PagingResult<WorkingShift> Paging(int pageSize , int pageNumber, string workingShiftFilter,string whereSql)
        {
            string storedProcedureName = string.Format(ProcedureName.PagingAndFilterTest);

            var parameters = new DynamicParameters();
            parameters.Add("p_OffSet", pageNumber); // 
            parameters.Add("p_Limit", pageSize);
            parameters.Add("p_WorkingShiftFilter", workingShiftFilter);
            parameters.Add("p_Where", whereSql);

            var mySqlConnection = new MySqlConnection(DataContext.DataBaseContext.ConnectionString);
            mySqlConnection.Open();

            var getWorkingShiftFiltersFilter = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
           
            var workingShiftFilters = getWorkingShiftFiltersFilter.Read<WorkingShift>().ToList();
            int totalRecords = getWorkingShiftFiltersFilter.Read<int>().Single();
            double totalPage = getWorkingShiftFiltersFilter.Read<int>().Single();
            return new PagingResult<WorkingShift>
            {
                CurrentPage = pageNumber,
                CurrentPageRecords = pageSize,
                TotalPage = Convert.ToInt32(totalPage),
                TotalRecord = totalRecords,
                Data = workingShiftFilters
            };
        }


        #endregion

    }
}

using AMISAPP_MISA.Common.Constants;
using AMISAPP_MISA.Common.Entities;
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

namespace AMISAPP_MISA.DL.ColumnTableDL
{
    public class ColumTableDL : IColumnTableDL
    {
        public int DeleteColumnTable()
        {
            var storedProcedureName = Resource.Proc_ColumnTable_Delete;
            int numberOfAffectedRows = 0;
            using (var mySqlConnection = new MySqlConnection(DataBaseContext.ConnectionString))
            {
                numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName);
            }
            return numberOfAffectedRows;
        }

        public List<ColumnTable> GetAll()
        {
            var storedProcedureName = ProcedureName.GetAllColumnTable;
            List<ColumnTable> lstColumnTable;
            using (var mySqlConnection = new MySqlConnection(DataBaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                lstColumnTable = result.Read<ColumnTable>().ToList();
            }
            return lstColumnTable;
        }

        public int InsertColumnTable(List<ColumnTable> columns)
        {
            string sql = "INSERT INTO comlumntable VALUES ('{0}', '{1}', {2}, '{3}', '{4}', '{5}', '{6}', '{7}', {8}, {9})";
            int numberOfAffectedRows = 0;
            using (var mySqlConnection = new MySqlConnection(DataBaseContext.ConnectionString))
            {
                foreach (var column in columns)
                {
                    var Fixed = column.Fixed == true ? 1 : 0;
                    var IsCheck = column.IsCheck == true ? 1 : 0;
                    numberOfAffectedRows = mySqlConnection.Execute(string.Format(sql, column.Field, column.Caption, column.Width, Fixed, column.FixPosition, column.Alignemnt, column.Format, column.DataType, IsCheck, column.OrderColumn));
                }
            }
            return numberOfAffectedRows;

        }
    }
}

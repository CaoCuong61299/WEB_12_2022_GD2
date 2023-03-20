using AMISAPP_MISA.Common.Entities;
using AMISAPP_MISA.DL.ColumnTableDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISAPP_MISA.BL.ColumnTableBL
{
    public class ColumnTableBL : IColumnTableBL
    {
        #region Field

        private IColumnTableDL _columnTableDL;

        #endregion

        #region contructor

        public ColumnTableBL(IColumnTableDL columnTableDL)
        {
            _columnTableDL= columnTableDL;
        }
        #endregion
        public int DeleteColumnTable()
        {
           var result =  _columnTableDL.DeleteColumnTable();
            return result;
        }

        public List<ColumnTable> GetAll()
        {
            var result = _columnTableDL.GetAll();   
            return result;
        }

        public int InsertColumnTable(List<ColumnTable> columns)
        {
           var result = _columnTableDL.InsertColumnTable(columns);  
            return result;
        }
    }
}

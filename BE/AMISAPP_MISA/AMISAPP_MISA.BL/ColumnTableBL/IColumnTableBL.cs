using AMISAPP_MISA.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISAPP_MISA.BL.ColumnTableBL
{
    public interface IColumnTableBL
    {
        List<ColumnTable> GetAll();

        int InsertColumnTable(List<ColumnTable> columns);

        int DeleteColumnTable();
    }
}

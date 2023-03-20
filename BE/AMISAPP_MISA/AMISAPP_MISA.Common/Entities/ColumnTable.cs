using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISAPP_MISA.Common.Entities
{
    public class ColumnTable
    {
        public string? Field { get; set; }
        public string? Caption { get; set; }
        public int? Width { get; set; }
        public Boolean? Fixed { get; set; }
        public string? FixPosition { get; set; }
        public string? Alignemnt { get; set; }
        public string? Format { get; set; }
        public string? DataType { get; set; }

        public Boolean? IsCheck { get; set; }
        public int? OrderColumn { get; set; }

    }
}

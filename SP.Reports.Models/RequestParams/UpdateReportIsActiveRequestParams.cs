using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Reports.Models.RequestParams
{
    public class UpdateReportIsActiveRequestParams
    {
        public string Key { get; set; }
        public bool IsActive { get; set; }
    }
}

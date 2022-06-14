using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Reports.Models.RequestParams
{
    public class UpdateReportRolesRequestParams
    {
        public string Key { get; set; }
        public string[] Roles { get; set; }
    }
}

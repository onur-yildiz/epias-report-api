﻿using SP.Reports.Models;
using SP.Reports.Models.ReportListing;
using SP.Reports.Models.RequestParams;

namespace SP.Reports.Service
{
    public interface IReportsService
    {
        Task<T?> GetData<T, V>(object? r, string endpoint) where T : class where V : IResponseBase<T>;
        IEnumerable<Report>? GetReports();
        void UpdateRoles(string reportKey, UpdateReportRolesRequestParams r);
        void UpdateIsActive(string reportKey, UpdateReportIsActiveRequestParams r);
    }
}
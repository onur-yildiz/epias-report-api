
using System.Linq.Expressions;
using Moq;
using SP.Exceptions;
using SP.Reports.Service;
using SP.Reports.Models.ReportListing;
using SP.Reports.Models.RequestBody;
using SP.Reports.Service.Tests.Mocks;

namespace SP.Reports.Service.Tests
{
    public class Tests
    {
        static readonly Report _report = new(string.Empty, string.Empty, default, new HashSet<string>(), new HashSet<ReportName>());

        static (DependencyMocks, ReportsService) ReportsServiceSetup()
        {
            var mocks = new DependencyMocks();
            var service = new ReportsService(mocks.HttpClientFactory.Object, mocks.ReportRepository.Object);
            mocks.HttpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(mocks.HttpClient.Object);

            return (mocks, service);
        }

        // TODO GetData_QueryObjectAndEndpoint_ReturnsExtractedContentBody

        [Fact]
        public void GetReports_NoParams_ReturnsReports()
        {
            //Setup
            var reports = new List<Report>() { _report };
            var (mocks, service) = ReportsServiceSetup();
            mocks.ReportRepository.Setup(r => r.Get()).Returns(reports).Verifiable();

            //Act
            var result = service.GetReports();

            //Assert
            Assert.Equal(reports, result);
            mocks.ReportRepository.Verify();
        }

        [Fact]
        public void UpdateIsActive_ReportKeyAndActiveStatus_CallsUpdate()
        {
            //Setup
            var reportKey = string.Empty;
            var isActiveReqBody = new Mock<IUpdateReportIsActiveRequestBody>();
            var (mocks, service) = ReportsServiceSetup();
            mocks.ReportRepository.Setup(r => r.UpdateOne_Set(It.IsAny<Expression<Func<Report, bool>>>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(_report).Verifiable();

            //Act
            service.UpdateIsActive(reportKey, isActiveReqBody.Object);

            //Assert
            mocks.ReportRepository.Verify();
        }

        [Fact]
        public void UpdateIsActive_ReportKeyAndActiveStatus_UpdateReturnsNullAndThrows()
        {
            //Setup
            var reportKey = string.Empty;
            var isActiveReqBody = new Mock<IUpdateReportIsActiveRequestBody>();
            var (mocks, service) = ReportsServiceSetup();
            mocks.ReportRepository.Setup(r => r.UpdateOne_Set(It.IsAny<Expression<Func<Report, bool>>>(), It.IsAny<string>(), It.IsAny<bool>())).Returns((Report?)null).Verifiable();

            //Act
            void act() => service.UpdateIsActive(reportKey, isActiveReqBody.Object);

            //Assert
            Assert.Throws<HttpResponseException>(act);
        }

        [Fact]
        public void UpdateRoles_ReportKeyAndRoles_CallsUpdate()
        {
            //Setup
            var reportKey = string.Empty;
            var rolesReqBody = new Mock<IUpdateReportRolesRequestBody>();
            var (mocks, service) = ReportsServiceSetup();
            mocks.ReportRepository.Setup(r => r.UpdateOne_Set(It.IsAny<Expression<Func<Report, bool>>>(), It.IsAny<string>(), It.IsAny<string[]>())).Returns(_report).Verifiable();

            //Act
            service.UpdateRoles(reportKey, rolesReqBody.Object);

            //Assert
            mocks.ReportRepository.Verify();
        }

        [Fact]
        public void UpdateRoles_ReportKeyAndRoles_UpdateReturnsNullAndThrows()
        {
            //Setup
            var reportKey = string.Empty;
            var rolesReqBody = new Mock<IUpdateReportRolesRequestBody>();
            var (mocks, service) = ReportsServiceSetup();
            mocks.ReportRepository.Setup(r => r.UpdateOne_Set(It.IsAny<Expression<Func<Report, bool>>>(), It.IsAny<string>(), It.IsAny<string[]>())).Returns((Report?)null).Verifiable();

            //Act
            void act() => service.UpdateRoles(reportKey, rolesReqBody.Object);

            //Assert
            Assert.Throws<HttpResponseException>(act);
        }


    }
}

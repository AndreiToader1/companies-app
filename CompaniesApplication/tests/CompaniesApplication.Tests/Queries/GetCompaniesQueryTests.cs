using System.Runtime.InteropServices;
using CompaniesApplication.Application.Enums;
using CompaniesApplication.Application.Queries;
using CompaniesApplication.Application.Services.Contracts;
using CompaniesApplication.Domain;
using Moq;
using Xunit;

namespace CompaniesApplication.Tests.Queries;

public class GetCompaniesQueryTests
{
    private readonly Mock<ICompaniesData> m_companiesData;
    private readonly Mock<ISortingService> m_sortingService;

    public GetCompaniesQueryTests()
    {
        m_companiesData = new Mock<ICompaniesData>();
        m_sortingService = new Mock<ISortingService>();
    }
    
    [Fact]
    public async Task Handle_ShouldCallCompaniesData()
    {
        var handler = new GetCompaniesQueryRequestHandler(m_companiesData.Object, m_sortingService.Object);
        var request = new GetCompaniesQueryRequest(SortingField.CompanyName);
        await handler.Handle(request, CancellationToken.None);
        
        m_companiesData.Verify(m => m.GetCompanies(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallSortingService()
    {
        var handler = new GetCompaniesQueryRequestHandler(m_companiesData.Object, m_sortingService.Object);
        var request = new GetCompaniesQueryRequest(SortingField.CompanyName);
        var companies = new List<Company>
        {
            new Company(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(), 10, Guid.NewGuid().ToString())
        };
        m_companiesData.Setup(m => m.GetCompanies()).Returns(companies);
        await handler.Handle(request, CancellationToken.None);
        
        m_sortingService.Verify(m => m.SortCompanies(
                It.Is<IList<Company>>(c => c.First() == companies.First()),
                It.Is<SortingField>(field => field == SortingField.CompanyName)),
            Times.Once);
    }
}
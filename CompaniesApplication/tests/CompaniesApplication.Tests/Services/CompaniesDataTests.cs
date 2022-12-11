using CompaniesApplication.Application.Services;
using CompaniesApplication.Application.Services.Contracts;
using CompaniesApplication.Domain;
using FluentAssertions;
using Moq;
using Xunit;

namespace CompaniesApplication.Tests.Services;

public class CompaniesDataTests
{
    private readonly Mock<ICompaniesDataSource> m_commaSeparatedDataSource;
    private readonly Mock<ICompaniesDataSource> m_tagSeparatedDataSource;
    private readonly Mock<ICompaniesDataSource> m_hypenSeparatedDataSource;
    private readonly ICompaniesData m_companiesData;
    
    public CompaniesDataTests()
    {
        m_commaSeparatedDataSource = new Mock<ICompaniesDataSource>();
        m_tagSeparatedDataSource = new Mock<ICompaniesDataSource>();
        m_hypenSeparatedDataSource = new Mock<ICompaniesDataSource>();
        var dataSourcesList = new List<ICompaniesDataSource>
        {
            m_commaSeparatedDataSource.Object,
            m_tagSeparatedDataSource.Object,
            m_hypenSeparatedDataSource.Object
        };
        m_companiesData = new CompaniesData(dataSourcesList);
    }

    [Fact]
    public void GetCompanies_ShouldCallAllDataSources()
    {
        ArrangeTestData();
        m_companiesData.GetCompanies();
        m_commaSeparatedDataSource.Verify(m => m.GetCompanies(), Times.Once);
        m_tagSeparatedDataSource.Verify(m => m.GetCompanies(), Times.Once);
        m_hypenSeparatedDataSource.Verify(m => m.GetCompanies(), Times.Once);
    }

    [Fact]
    public void GetCompanies_ShouldGetAllCompanies()
    {
        var (mockCompany1, mockCompany2, mockCompany3) = ArrangeTestData();
        var companies = m_companiesData.GetCompanies();

        companies.Should().HaveCount(3);
        companies[0].Should().Be(mockCompany1);
        companies[1].Should().Be(mockCompany2);
        companies[2].Should().Be(mockCompany3);
    }

    private (Company, Company, Company) ArrangeTestData()
    {
        var mockCompany1 = GetMockCompany();
        var mockCompany2 = GetMockCompany();
        var mockCompany3 = GetMockCompany();

        m_commaSeparatedDataSource.Setup(m => m.GetCompanies()).Returns(
            new List<Company> { mockCompany1 });
        m_tagSeparatedDataSource.Setup(m => m.GetCompanies())
            .Returns(new List<Company> { mockCompany2 });
        m_hypenSeparatedDataSource.Setup(m => m.GetCompanies()).Returns(
            new List<Company>{mockCompany3});

        return (mockCompany1, mockCompany2, mockCompany3);
    }

    private Company GetMockCompany()
    {
        return new Company(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(), 10, Guid.NewGuid().ToString());
    }
}
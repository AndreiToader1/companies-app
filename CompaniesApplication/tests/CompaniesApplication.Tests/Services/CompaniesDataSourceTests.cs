using CompaniesApplication.Application.Services;
using CompaniesApplication.Application.Services.Contracts;
using CompaniesApplication.Domain;
using FluentAssertions;
using Moq;
using Xunit;

namespace CompaniesApplication.Tests.Services;

public class CompaniesDataSourceTests
{
    private readonly Mock<ICompaniesDataFormat> m_companiesDataFormat;
    private readonly Mock<IFileReader> m_mockFileReader;
    private readonly string m_filePath;
    private readonly CompaniesDataSource m_companiesDataSource;

    public CompaniesDataSourceTests()
    {
        m_companiesDataFormat = new Mock<ICompaniesDataFormat>();
        m_mockFileReader = new Mock<IFileReader>();
        m_filePath = Guid.NewGuid().ToString();
        m_companiesDataSource = new CompaniesDataSource(
            m_companiesDataFormat.Object, m_filePath, m_mockFileReader.Object);
    }

    [Fact]
    public void GetCompanies_ShouldCallFileReader()
    {
        var companiesLines = new List<string> { Guid.NewGuid().ToString()};
        m_mockFileReader.Setup(m => m.ReadLines(It.IsAny<string>())).Returns(
            companiesLines);
        m_companiesDataSource.GetCompanies();
        
        m_mockFileReader.Verify(m => m.ReadLines(It.Is<string>(s => s == m_filePath))
        , Times.Once);
    }

    [Fact]
    public void GetCompanies_ShouldConvertLinesToCompany()
    {
        var companiesLines = new List<string> { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
        m_mockFileReader.Setup(m => m.ReadLines(It.IsAny<string>())).Returns(
            companiesLines);

        m_companiesDataSource.GetCompanies();
        
        m_companiesDataFormat.Verify(m => m.ConvertToCompany(companiesLines.First()),
            Times.Once);
        m_companiesDataFormat.Verify(m => m.ConvertToCompany(companiesLines.Last()),
            Times.Once);
    }

    [Fact]
    public void GetCompanies_ShouldGetConvertedCompanies()
    {
        var companiesLines = new List<string> { Guid.NewGuid().ToString()};
        m_mockFileReader.Setup(m => m.ReadLines(It.IsAny<string>())).Returns(
            companiesLines);
        var mockCompany = new Company(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(), 10, Guid.NewGuid().ToString());
        m_companiesDataFormat.Setup(m => m.ConvertToCompany(It.IsAny<string>()))
            .Returns(mockCompany);

        var companies = m_companiesDataSource.GetCompanies();

        companies.Should().HaveCount(1);
        companies.First().Should().Be(mockCompany);
    }
}
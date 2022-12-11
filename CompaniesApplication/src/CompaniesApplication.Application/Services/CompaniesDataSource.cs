using CompaniesApplication.Application.Services.Contracts;
using CompaniesApplication.Domain;

namespace CompaniesApplication.Application.Services;

public class CompaniesDataSource : ICompaniesDataSource
{
    private readonly ICompaniesDataFormat m_companiesDataFormat;
    private readonly string m_filePath;
    private readonly IFileReader m_fileReader;
    
    public CompaniesDataSource(ICompaniesDataFormat companiesDataFormat, string filePath,
        IFileReader fileReader)
    {
        m_companiesDataFormat = companiesDataFormat;
        m_filePath = filePath;
        m_fileReader = fileReader;
    }
    
    public IList<Company> GetCompanies()
    {
        var companies = m_fileReader.ReadLines(m_filePath);

        return companies.Select(m_companiesDataFormat.ConvertToCompany).ToList();
    }
}
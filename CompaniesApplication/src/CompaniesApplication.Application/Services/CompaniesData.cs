using CompaniesApplication.Application.Services.Contracts;
using CompaniesApplication.Domain;

namespace CompaniesApplication.Application.Services;

public class CompaniesData : ICompaniesData
{
    private readonly IList<ICompaniesDataSource> m_companiesDataSources;
    
    public CompaniesData(IList<ICompaniesDataSource> companiesDataSources)
    {
        m_companiesDataSources = companiesDataSources;
    }
    
    public IList<Company> GetCompanies()
    {
        var companies = new List<Company>();

        foreach (var companiesDataSource in m_companiesDataSources)
        {
            companies.AddRange(companiesDataSource.GetCompanies());
        }

        return companies;
    }
}
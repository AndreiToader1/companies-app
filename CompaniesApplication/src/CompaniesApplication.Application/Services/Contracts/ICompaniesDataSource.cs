using CompaniesApplication.Domain;

namespace CompaniesApplication.Application.Services.Contracts;

public interface ICompaniesDataSource
{
    IList<Company> GetCompanies();
}
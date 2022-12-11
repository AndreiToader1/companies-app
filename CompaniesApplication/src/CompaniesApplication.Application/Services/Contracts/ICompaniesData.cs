using CompaniesApplication.Domain;

namespace CompaniesApplication.Application.Services.Contracts;

public interface ICompaniesData
{
    IList<Company> GetCompanies();
}
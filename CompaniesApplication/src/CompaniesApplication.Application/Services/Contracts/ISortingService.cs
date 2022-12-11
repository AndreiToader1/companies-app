using CompaniesApplication.Application.Enums;
using CompaniesApplication.Domain;

namespace CompaniesApplication.Application.Services.Contracts;

public interface ISortingService
{
    IList<Company> SortCompanies(IList<Company> companies,SortingField sortingField);
}
using CompaniesApplication.Application.Enums;
using CompaniesApplication.Application.Services.Contracts;
using CompaniesApplication.Application.Utilities.Contracts;
using CompaniesApplication.Domain;

namespace CompaniesApplication.Application.Services;

public class SortingService : ISortingService
{
    private readonly ISortingAlgorithm<Company, SortingField> m_sortingAlgorithm;

    public SortingService(ISortingAlgorithm<Company, SortingField> sortingAlgorithm)
    {
        m_sortingAlgorithm = sortingAlgorithm;
    }
    public IList<Company> SortCompanies(IList<Company> companies, SortingField sortingField)
    {
        companies = m_sortingAlgorithm.Sort(companies, ShouldSwap, sortingField);

        return companies;
    }

    private bool ShouldSwap(Company company1, Company company2, SortingField sortingField)
    {
        switch (sortingField)
        {
            case SortingField.None:
                return false;
            case SortingField.CompanyName:
                return String.CompareOrdinal(company1.Name, company2.Name) > 0;
            case SortingField.ContactName:
                return String.CompareOrdinal(company1.ContactName, company2.ContactName) > 0;
            case SortingField.YearsInBusinessThenCompanyName:
                return company1.YearsInBusiness > company2.YearsInBusiness || String.CompareOrdinal(
                    company1.Name, company2.Name) > 0;
            default:
                return false;
        }
    }
}
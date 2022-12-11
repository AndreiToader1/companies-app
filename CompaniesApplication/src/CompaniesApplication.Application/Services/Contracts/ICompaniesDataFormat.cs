using CompaniesApplication.Domain;

namespace CompaniesApplication.Application.Services.Contracts;

public interface ICompaniesDataFormat
{
    Company ConvertToCompany(string companyInfo);
}
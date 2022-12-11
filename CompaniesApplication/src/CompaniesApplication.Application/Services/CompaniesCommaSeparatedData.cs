using CompaniesApplication.Application.Services.Contracts;
using CompaniesApplication.Domain;

namespace CompaniesApplication.Application.Services;

public class CompaniesCommaSeparatedData : ICompaniesDataFormat
{
    public Company ConvertToCompany(string companyInfo)
    {
        var companyProperties = companyInfo.Split(',');

        return new Company(companyProperties[0].Trim(), companyProperties[1].Trim(),
            companyProperties[2].Trim(), Convert.ToInt32(companyProperties[3].Trim()), 
            companyProperties[4].Trim());
    }
}
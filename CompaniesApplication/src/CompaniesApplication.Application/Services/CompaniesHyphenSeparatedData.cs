using CompaniesApplication.Application.Services.Contracts;
using CompaniesApplication.Domain;

namespace CompaniesApplication.Application.Services;

public class CompaniesHyphenSeparatedData : ICompaniesDataFormat
{
    public Company ConvertToCompany(string companyInfo)
    {
        var companyProperties = companyInfo.Split('-');

        return new Company(companyProperties[0].Trim(), companyProperties[4].Trim() +" "+ companyProperties[5].Trim(),
            companyProperties[2].Trim(), DateTime.UtcNow.Year- Convert.ToInt32(companyProperties[1].Trim()), 
            companyProperties[3].Trim());
    }
}
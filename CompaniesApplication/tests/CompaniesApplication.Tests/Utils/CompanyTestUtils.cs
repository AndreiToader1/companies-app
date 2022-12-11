using CompaniesApplication.Domain;
using FluentAssertions;

namespace CompaniesApplication.Tests.Utils;

public static class CompanyTestUtils
{
    public static void AssertCompanyInformation(Company company, string expectedName, string expectedContactName,
        string expectedContactPhoneNumber, int expectedYearsInBusiness, string expectedContactEmail)
    {
        company.Name.Should().Be(expectedName);
        company.ContactName.Should().Be(expectedContactName);
        company.ContactPhoneNumber.Should().Be(expectedContactPhoneNumber);
        company.YearsInBusiness.Should().Be(expectedYearsInBusiness);
        company.ContactEmail.Should().Be(expectedContactEmail);
    }
}
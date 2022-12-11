using CompaniesApplication.Application.Services;
using CompaniesApplication.Application.Services.Contracts;
using CompaniesApplication.Application.Utilities;
using CompaniesApplication.Application.Utilities.Contracts;
using CompaniesApplication.Infrastructure;

namespace CompaniesApplication;

public static class ServicesExtensions
{
    public static IServiceCollection AddSortingAlgorithm<T1, T2>(this IServiceCollection services) where T1 : class where T2:Enum
    {
        services.AddTransient<ISortingAlgorithm<T1, T2>, BubbleSortAlgorithm<T1, T2>>();

        return services;
    }

    public static IServiceCollection AddCompaniesData(this IServiceCollection services)
    {
        services.AddTransient<ICompaniesData, CompaniesData>(sp =>
        {
            var commaSeparatedDataFormat = new CompaniesCommaSeparatedData();
            var tagSeparatedDataFormat = new CompaniesTagSeparatedData();
            var hyphenSeparatedDataFormat = new CompaniesHyphenSeparatedData();
            
            var fileReader = new FileReader();
            var commaSeparatedDataSource = new CompaniesDataSource(commaSeparatedDataFormat, "./DataFiles/comma.txt",
                fileReader);
            var tagSeparatedDataSource = new CompaniesDataSource(tagSeparatedDataFormat, "./DataFiles/hash.txt",
                fileReader);
            var hyphenSeparatedDataSource = new CompaniesDataSource(hyphenSeparatedDataFormat, "./DataFiles/hyphen.txt",
                fileReader);
            IList<ICompaniesDataSource> companiesDataSources = new List<ICompaniesDataSource>
            {
                commaSeparatedDataSource,
                tagSeparatedDataSource,
                hyphenSeparatedDataSource
            };

            return new CompaniesData(companiesDataSources);
        });

        return services;
    }
}
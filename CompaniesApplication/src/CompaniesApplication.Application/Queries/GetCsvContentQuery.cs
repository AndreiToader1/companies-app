using System.Text;
using CompaniesApplication.Domain;
using MediatR;

namespace CompaniesApplication.Application.Queries;

public class GetCsvContentQueryRequest: IRequest<string>
{
    public IList<Company> Companies { get; }

    public GetCsvContentQueryRequest(IList<Company> companies)
    {
        Companies = companies;
    }
}

public class GetCsvContentQueryHandler : IRequestHandler<GetCsvContentQueryRequest, string>
{
    public Task<string> Handle(GetCsvContentQueryRequest request, CancellationToken cancellationToken)
    {
        StringBuilder csvBuilder = new StringBuilder();
        AppendCsvHeader(csvBuilder);
        foreach (var company in request.Companies)
        {
            AppendCompanyInfo(csvBuilder, company);
        }
        return Task.FromResult(csvBuilder.ToString());
    }

    private void AppendCsvHeader(StringBuilder csvBuilder)
    {
        csvBuilder.Append("Name,ContactName,ContactPhoneNumber,YearsInBusiness,ContactEmail");
        csvBuilder.Append("\r\n");
    }

    private void AppendCompanyInfo(StringBuilder csvBuilder, Company company)
    {
        csvBuilder.Append(
            $"{company.Name},{company.ContactName},{company.ContactPhoneNumber},{company.YearsInBusiness},{company.ContactEmail}");
        csvBuilder.Append("\r\n");
    }
}
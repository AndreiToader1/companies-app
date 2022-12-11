using CompaniesApplication.Application.Enums;
using CompaniesApplication.Application.Services.Contracts;
using CompaniesApplication.Domain;
using MediatR;

namespace CompaniesApplication.Application.Queries;

public class GetCompaniesQueryRequest: IRequest<IList<Company>>
{
    public SortingField SortingField { get; }

    public GetCompaniesQueryRequest(SortingField sortingField)
    {
        SortingField = sortingField;
    }
}

public class GetCompaniesQueryRequestHandler : IRequestHandler<GetCompaniesQueryRequest, IList<Company>>
{
    private readonly ICompaniesData m_companiesData;
    private readonly ISortingService m_sortingService;
    
    public GetCompaniesQueryRequestHandler(ICompaniesData companiesData, ISortingService sortingService)
    {
        m_companiesData = companiesData;
        m_sortingService = sortingService;
    }
    
    public Task<IList<Company>> Handle(GetCompaniesQueryRequest request, CancellationToken cancellationToken)
    {
        var companiesData = m_companiesData.GetCompanies();
        companiesData = m_sortingService.SortCompanies(companiesData, request.SortingField);
        
        return Task.FromResult(companiesData);
    }
}
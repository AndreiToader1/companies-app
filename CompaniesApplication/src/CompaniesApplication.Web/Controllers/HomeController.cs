using System.Diagnostics;
using System.Text;
using CompaniesApplication.Application.Enums;
using CompaniesApplication.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using CompaniesApplication.Models;
using MediatR;

namespace CompaniesApplication.Controllers;

public class HomeController : Controller
{
    private readonly IMediator m_mediator;
    
    public HomeController(IMediator mediator)
    {
        m_mediator = mediator;
    }

    public async Task<IActionResult> Index([FromQuery]SortingField sortingField)
    {
        var companies = await m_mediator.Send(new GetCompaniesQueryRequest(sortingField));
        var companiesViewModels = companies.Select(c => new CompanyViewModel
        {
            Name = c.Name,
            ContactEmail = c.ContactEmail,
            ContactName = c.ContactName,
            ContactPhoneNumber = c.ContactPhoneNumber,
            YearsInBusiness = c.YearsInBusiness
        }).ToList();
        
        return View(companiesViewModels);
    }

    [HttpPost]
    public async Task<FileResult> ExportCompaniesCsv([FromQuery]SortingField sortingField)
    {
        var companies = await m_mediator.Send(new GetCompaniesQueryRequest(sortingField));
        var csvContent = await m_mediator.Send(new GetCsvContentQueryRequest(companies));
        
        return File(Encoding.UTF8.GetBytes(csvContent), "text/csv", "companies.csv");
    }
}
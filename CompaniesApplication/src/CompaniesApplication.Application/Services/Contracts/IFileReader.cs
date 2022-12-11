namespace CompaniesApplication.Application.Services.Contracts;

public interface IFileReader
{
    IList<string> ReadLines(string filePath);
}
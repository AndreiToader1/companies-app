using CompaniesApplication.Application.Services.Contracts;

namespace CompaniesApplication.Infrastructure;

public class FileReader : IFileReader
{
    public IList<string> ReadLines(string filePath)
    {
        return File.ReadAllLines(filePath);
    }
}
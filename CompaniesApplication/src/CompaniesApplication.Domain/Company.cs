namespace CompaniesApplication.Domain;

public class Company
{
    public Company(string name, string contactName, string contactPhoneNumber, 
        int yearsInBusiness, string contactEmail)
    {
        Name = name;
        ContactName = contactName;
        ContactPhoneNumber = contactPhoneNumber;
        YearsInBusiness = yearsInBusiness;
        ContactEmail = contactEmail;
    }
    
    public string Name { get; }
    
    public string ContactName { get; }
    
    public string ContactPhoneNumber { get; }
    
    public int YearsInBusiness { get;}
    
    public string ContactEmail { get;}
}
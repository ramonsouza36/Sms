namespace SchoolManagerSystem.Models;

public class Student
{
    #region Properties

    public Guid Id { get; set; }

    public string? FederalRegistration { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime BirthDate { get; set; } = DateTime.Now;

    #endregion Properties
}
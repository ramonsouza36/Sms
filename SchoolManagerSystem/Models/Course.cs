namespace SchoolManagerSystem.Models;

public class Course
{
    #region Properties

    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Requirement { get; set; }

    public int WorkLoad { get; set; } = 0;

    public double Price { get; set; } = 0;

    #endregion Properties
}
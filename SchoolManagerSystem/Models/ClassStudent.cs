namespace SchoolManagerSystem.Models;

public class ClassStudent
{
    #region Properties

    public Guid Id { get; set; }

    public Guid InstructorId { get; set; }

    public Guid CourseId { get; set; }

    public DateTime? StartDate { get; set; } = DateTime.Now;

    public DateTime? EndDate { get; set; } = DateTime.Now;

    public int WorkLoad {get; set; } = 0;

    #endregion Properties
}
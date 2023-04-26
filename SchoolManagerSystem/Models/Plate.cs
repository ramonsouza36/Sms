namespace SchoolManagerSystem.Models;

public class Plate
{
    #region Properties

    public Guid Id { get; set; }

    public Guid ClassStudentId { get; set; }

    public Guid StudentId { get; set; }

    public DateTime? PlateDate { get; set; } = DateTime.Now;

    #endregion Properties
}
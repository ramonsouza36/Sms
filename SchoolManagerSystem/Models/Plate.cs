using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagerSystem.Models;

public class Plate
{
    #region Properties

    public Guid Id { get; set; }

    public Guid ClassStudentId { get; set; }

    public Guid StudentId { get; set; }

    public DateTime PlateDate { get; set; } = DateTime.Now;

    [NotMapped]
    public string NameStudent { get; set; }

    [NotMapped]
    public string NameCourse { get; set; }

    #endregion Properties
}
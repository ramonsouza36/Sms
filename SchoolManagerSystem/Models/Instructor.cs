using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagerSystem.Models;

public class Instructor
{
    #region Properties

    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public int HourlyCost { get; set; } = 0;

    public string? Certications { get; set; }

    #endregion Properties
}
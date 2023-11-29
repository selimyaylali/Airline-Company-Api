using System;
using System.Collections.Generic;

namespace midterm_SE4458.Model;

public partial class Attendant
{
    public int AttendantId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Username { get; set; }

    public string? AttendantPassword { get; set; }

    public string? Token { get; set; }

    public virtual ICollection<AttendantFlight> AttendantFlights { get; } = new List<AttendantFlight>();
}

using System;
using System.Collections.Generic;

namespace midterm_SE4458.Model;

public partial class AttendantFlight
{
    public int Id { get; set; }

    public int? AttendantId { get; set; }

    public int? FlightId { get; set; }

    public virtual Attendant? Attendant { get; set; }

    public virtual Flight? Flight { get; set; }
}

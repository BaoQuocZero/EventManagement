using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo_02.Models;

public partial class Event
{
    public int EventsId { get; set; }

    public int EventtypesId { get; set; }

    public string EventName { get; set; }

    public DateTime? EventTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public int? RequiredParticipants { get; set; }

    public int? MaxParticipants { get; set; }

    public string DressCode { get; set; }

    public string EventGroupLink { get; set; }

    public string AttendanceListLink { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<Eventparticipation> Eventparticipations { get; set; } = new List<Eventparticipation>();

    public virtual Eventtype Eventtypes { get; set; }

    [NotMapped]
    public int TotalParticipants { get; set; }

    [NotMapped]
    public int TotalDonations { get; set; }

    [NotMapped]
    public int AbsentCount { get; set; }

    [NotMapped]
    public decimal ParticipationRate { get; set; }
}

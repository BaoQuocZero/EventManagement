using System;
using System.Collections.Generic;

namespace demo_02.Models;

public partial class Eventdonation
{
    public int EventdonationsId { get; set; }

    public int ParticipationId { get; set; }

    public int? Amount { get; set; }

    public DateTime? DonationDate { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Eventparticipation Participation { get; set; }
}

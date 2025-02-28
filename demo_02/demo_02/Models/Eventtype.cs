namespace demo_02.Models;

public partial class Eventtype
{
    public int EventtypesId { get; set; }

    public string EventtypesName { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}

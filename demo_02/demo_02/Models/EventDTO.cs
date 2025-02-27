namespace demo_02.Models;

public class EventDTO
{
    public int EventsId { get; set; }
    public string EventName { get; set; }
    public DateTime? EventTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public int? RequiredParticipants { get; set; }
    public int? MaxParticipants { get; set; }
    public string EventTypeName { get; set; } // Lấy tên loại sự kiện
}

namespace demo_02.Data
{
    public class EventParticipationDTO
    {
        public int ParticipationId { get; set; }
        public string EventName { get; set; }
        public string UserName { get; set; }
        public string ParticipationStatus { get; set; }
        public DateTime? ParticipationTime { get; set; }
        public int? EarnedPoints { get; set; }
    }

}

namespace SpeedBracketsFakeAPI.Models
{
	public class RealTimeEvent
	{
		public RealTimeEventPayload Payload { get; set; }
		public string Locale { get; set; }
		public Metadata Metadata { get; set; }
	}

	public class RealTimeEventPayload
	{
		public Game Game { get; set; }
		public Event Event { get; set; }
	}
}

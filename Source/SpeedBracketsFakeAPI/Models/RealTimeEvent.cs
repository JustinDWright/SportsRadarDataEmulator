using Newtonsoft.Json;

namespace SpeedBracketsFakeAPI.Models
{
	public class RealTimeEvent
	{
		public RealTimeEventPayload payload { get; set; }
		public string locale { get; set; }
		public Metadata metadata { get; set; }
	}

	public class RealTimeEventPayload
	{
		public Game game { get; set; }
		[JsonProperty(PropertyName = "event")]
		public Event Event { get; set; }
	}
}

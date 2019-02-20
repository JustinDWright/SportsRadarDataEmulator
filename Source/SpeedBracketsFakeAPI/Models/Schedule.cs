namespace SpeedBracketsFakeAPI.Models
{
	public class Schedule
	{
		public League league { get; set; }
		public Season season { get; set; }
		public Tournament[] tournaments { get; set; }
	}

	public class League
	{
		public string id { get; set; }
		public string name { get; set; }
		public string alias { get; set; }
	}
}

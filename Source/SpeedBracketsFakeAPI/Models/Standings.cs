namespace SpeedBracketsFakeAPI.Models
{
	public class Standings
	{
		public League league { get; set; }
		public Season season { get; set; }
		public Conference[] conferences { get; set; }
	}

	public class Conference
	{
		public string id { get; set; }
		public string name { get; set; }
		public string alias { get; set; }
		public Team[] teams { get; set; }
	}

	public class Games_Behind
	{
		public float conference { get; set; }
	}

	public class Streak
	{
		public string kind { get; set; }
		public int length { get; set; }
	}

	public class Record
	{
		public string record_type { get; set; }
		public int wins { get; set; }
		public int losses { get; set; }
		public float win_pct { get; set; }
	}
}

using System;

namespace SpeedBracketsFakeAPI.Models
{
	public class Tournament
	{
		public string id { get; set; }
		public string name { get; set; }
		public string location { get; set; }
		public string status { get; set; }
		public DateTime start_date { get; set; }
		public DateTime end_date { get; set; }
		public League league { get; set; }
		public Season season { get; set; }
		public Round[] rounds { get; set; }
	}

	public class Round
	{
		public string id { get; set; }
		public int sequence { get; set; }
		public string name { get; set; }
		public Game[] games { get; set; }
		public Bracketed[] bracketed { get; set; }
	}

	public class Broadcast
	{
		public string network { get; set; }
		public string satellite { get; set; }
	}	

	public class Bracketed
	{
		public Bracket bracket { get; set; }
		public Game[] games { get; set; }
	}

	public class Bracket
	{
		public string id { get; set; }
		public string name { get; set; }
		public string location { get; set; }
	}
}

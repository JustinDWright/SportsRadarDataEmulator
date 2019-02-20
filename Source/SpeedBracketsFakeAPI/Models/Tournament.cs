using System;

namespace SpeedBracketsFakeAPI.Models
{

	public class Tournament
	{
		public string id { get; set; }
		public string name { get; set; }
		public string location { get; set; }
		public string status { get; set; }
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
		public Game1[] games { get; set; }
	}

	public class Bracket
	{
		public string id { get; set; }
		public string name { get; set; }
		public string location { get; set; }
	}

	public class Game1
	{
		public string id { get; set; }
		public string status { get; set; }
		public string title { get; set; }
		public string coverage { get; set; }
		public DateTime scheduled { get; set; }
		public int home_points { get; set; }
		public int away_points { get; set; }
		public bool neutral_site { get; set; }
		public bool conference_game { get; set; }
		public bool track_on_court { get; set; }
		public Venue1 venue { get; set; }
		public Broadcast1 broadcast { get; set; }
		public Home1 home { get; set; }
		public Away1 away { get; set; }
	}

	public class Venue1
	{
		public string id { get; set; }
		public string name { get; set; }
		public int capacity { get; set; }
		public string address { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string zip { get; set; }
		public string country { get; set; }
	}

	public class Broadcast1
	{
		public string network { get; set; }
		public string satellite { get; set; }
	}

	public class Home1
	{
		public string name { get; set; }
		public string alias { get; set; }
		public string id { get; set; }
		public int seed { get; set; }
		public Source2 source { get; set; }
	}

	public class Source2
	{
		public string id { get; set; }
		public string title { get; set; }
		public string status { get; set; }
		public string coverage { get; set; }
		public string home_team { get; set; }
		public string away_team { get; set; }
		public DateTime scheduled { get; set; }
		public string outcome { get; set; }
	}

	public class Away1
	{
		public string name { get; set; }
		public string alias { get; set; }
		public string id { get; set; }
		public int seed { get; set; }
		public Source3 source { get; set; }
	}

	public class Source3
	{
		public string id { get; set; }
		public string title { get; set; }
		public string status { get; set; }
		public string coverage { get; set; }
		public string home_team { get; set; }
		public string away_team { get; set; }
		public DateTime scheduled { get; set; }
		public string outcome { get; set; }
	}
}

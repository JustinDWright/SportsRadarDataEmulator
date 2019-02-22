using System;

namespace SpeedBracketsFakeAPI.Models
{

	public class GameSummary
	{
		public string id { get; set; }
		public string title { get; set; }
		public string status { get; set; }
		public string coverage { get; set; }
		public bool neutral_site { get; set; }
		public DateTime scheduled { get; set; }
		public bool conference_game { get; set; }
		public int attendance { get; set; }
		public int lead_changes { get; set; }
		public int times_tied { get; set; }
		public string clock { get; set; }
		public int half { get; set; }
		public bool track_on_court { get; set; }
		public string entry_mode { get; set; }
		public string possession_arrow { get; set; }
		public Venue venue { get; set; }
		public Team home { get; set; }
		public Team away { get; set; }
		public object[] officials { get; set; }
	}
}

using System;
using System.Collections.Generic;

namespace SpeedBracketsFakeAPI.Models
{
	public class Game
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
		public Team home { get; set; }
		public Team away { get; set; }
		public List<Period> periods { get; set; }

		// NFL
		public int quarter { get; set; }
		public Summary summary { get; set; }

		public Game ToGameOnly()
		{
			return new Game
			{
				id = this.id,
				title = this.title,
				status = this.status,
				scheduled = this.scheduled,
				clock = this.clock,
				half = this.half,
				home = this.home,
				away = this.away
			};
		}
	}

	public class Metadata
	{
		public string league { get; set; }
		public string match { get; set; }
		public string status { get; set; }
		public string event_type { get; set; }
		public string event_category { get; set; }
		public string locale { get; set; }
		public string operation { get; set; }
		public string version { get; set; }
		public string team { get; set; }
	}

	// NFL
	public class Summary
	{
		public Season season { get; set; }
		public Week week { get; set; }
		public Venue venue { get; set; }
		public Team home { get; set; }
		public Team away { get; set; }
	}

	// NFL
	public class Season
	{
		public string id { get; set; }
		public int year { get; set; }
		public string type { get; set; }
		public string name { get; set; }
	}

	// NFL
	public class Week
	{
		public string id { get; set; }
		public int sequence { get; set; }
		public string title { get; set; }
	}

	//NFL
	public class Venue
	{
		public string id { get; set; }
		public string name { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string country { get; set; }
		public string zip { get; set; }
		public string address { get; set; }
		public int capacity { get; set; }
		public string surface { get; set; }
		public string roof_type { get; set; }
	}

	public class Period
	{
		public string type { get; set; }
		public string id { get; set; }
		public int number { get; set; }
		public int sequence { get; set; }
		public Scoring scoring { get; set; }		
		public List<Event> events { get; set; }

		// nfl
		public List<Pbp> pbp { get; set; }
	}

	// nfl
	public class Pbp
	{
		public string type { get; set; }
		public string id { get; set; }
		public float sequence { get; set; }
		public string reference { get; set; }
		public string clock { get; set; }
		public string event_type { get; set; }
		public string description { get; set; }
		public string alt_description { get; set; }
		public string start_reason { get; set; }
		public string end_reason { get; set; }
		public int play_count { get; set; }
		public string duration { get; set; }
		public int first_downs { get; set; }
		public int gain { get; set; }
		public int penalty_yards { get; set; }
		public IList<Event> events { get; set; }
		public bool inside_20 { get; set; }
		public bool scoring_drive { get; set; }
	}

	public class Scoring
	{
		public int times_tied { get; set; }
		public int lead_changes { get; set; }
		public Team home { get; set; }
		public Team away { get; set; }
	}

	public class Event
	{
		public string id { get; set; }
		public string clock { get; set; }
		public DateTime updated { get; set; }
		public string description { get; set; }
		public string event_type { get; set; }
		public Attribution attribution { get; set; }
		public Location location { get; set; }
		public Possession possession { get; set; }
		public On_Court on_court { get; set; }
		public List<Statistic> statistics { get; set; }
		public string turnover_type { get; set; }
		public int duration { get; set; }
		public string attempt { get; set; }

		// nfl
		public Score score { get; set; }
		public DateTime wall_clock { get; set; }
		public float sequence { get; set; }
	}

	// nfl
	public class Score
	{
		public int sequence { get; set; }
		public string clock { get; set; }
		public int points { get; set; }
		public int home_points { get; set; }
		public int away_points { get; set; }
		public PointsAfterPlay pointsafterplay { get; set; }
	}

	// nfl
	public class PointsAfterPlay
	{
		public string id { get; set; }
		public float sequence { get; set; }
		public string reference { get; set; }
		public string type { get; set; }
	}

	public class Attribution
	{
		public string name { get; set; }
		public string market { get; set; }
		public string id { get; set; }
		public string team_basket { get; set; }
	}

	public class Location
	{
		public int coord_x { get; set; }
		public int coord_y { get; set; }
	}

	public class Possession
	{
		public string name { get; set; }
		public string market { get; set; }
		public string id { get; set; }
	}

	public class On_Court
	{
		public Team home { get; set; }
		public Team away { get; set; }
	}

	public class Player
	{
		public string full_name { get; set; }
		public string jersey_number { get; set; }
		public string id { get; set; }
	}

	public class Statistic
	{
		public string type { get; set; }
		public Team team { get; set; }
		public Player player { get; set; }
		public bool made { get; set; }
		public string shot_type { get; set; }
		public int points { get; set; }
		public string rebound_type { get; set; }
		public bool three_point_shot { get; set; }
		public string shot_type_desc { get; set; }
		public string free_throw_type { get; set; }
	}

	public class Team
	{
		public string name { get; set; }
		public string market { get; set; }
		public string id { get; set; }
		public int points { get; set; }
		public int rank { get; set; }
		public bool double_bonus { get; set; }
		public int remaining_timeouts { get; set; }
		public List<Player> players { get; set; }
	}
}

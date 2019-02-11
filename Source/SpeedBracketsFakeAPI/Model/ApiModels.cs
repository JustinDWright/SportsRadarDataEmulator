using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedBracketsFakeAPI.Model
{


	public class Rootobject
	{
		public string id { get; set; }
		public string status { get; set; }
		public string reference { get; set; }
		public int number { get; set; }
		public DateTime scheduled { get; set; }
		public int attendance { get; set; }
		public int utc_offset { get; set; }
		public string entry_mode { get; set; }
		public string weather { get; set; }
		public string clock { get; set; }
		public int quarter { get; set; }
		public Summary summary { get; set; }
		public IList<Period> periods { get; set; }
		public string _comment { get; set; }
	}

	public class Summary
	{
		public Season season { get; set; }
		public Week week { get; set; }
		public Venue venue { get; set; }
		public Home home { get; set; }
		public Away away { get; set; }
	}

	public class Season
	{
		public string id { get; set; }
		public int year { get; set; }
		public string type { get; set; }
		public string name { get; set; }
	}

	public class Week
	{
		public string id { get; set; }
		public int sequence { get; set; }
		public string title { get; set; }
	}

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

	public class Home
	{
		public string id { get; set; }
		public string name { get; set; }
		public string market { get; set; }
		public string alias { get; set; }
		public string reference { get; set; }
		public int used_timeouts { get; set; }
		public int remaining_timeouts { get; set; }
		public int points { get; set; }
	}

	public class Away
	{
		public string id { get; set; }
		public string name { get; set; }
		public string market { get; set; }
		public string alias { get; set; }
		public string reference { get; set; }
		public int used_timeouts { get; set; }
		public int remaining_timeouts { get; set; }
		public int points { get; set; }
	}

	public class Period
	{
		public string period_type { get; set; }
		public string id { get; set; }
		public int number { get; set; }
		public int sequence { get; set; }
		public Scoring scoring { get; set; }
		public IList<Pbp> pbp { get; set; }
	}

	public class Scoring
	{
		public Home1 home { get; set; }
		public Away1 away { get; set; }
	}

	public class Home1
	{
		public string id { get; set; }
		public string name { get; set; }
		public string market { get; set; }
		public string alias { get; set; }
		public string reference { get; set; }
		public int points { get; set; }
	}

	public class Away1
	{
		public string id { get; set; }
		public string name { get; set; }
		public string market { get; set; }
		public string alias { get; set; }
		public string reference { get; set; }
		public int points { get; set; }
	}

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

	public class Event
	{
		public string type { get; set; }
		public string id { get; set; }
		public float sequence { get; set; }
		public string reference { get; set; }
		public string clock { get; set; }
		public int home_points { get; set; }
		public int away_points { get; set; }
		public string play_type { get; set; }
		public int play_clock { get; set; }
		public DateTime wall_clock { get; set; }
		public string description { get; set; }
		public string alt_description { get; set; }
		public Start_Situation start_situation { get; set; }
		public End_Situation end_situation { get; set; }
		public Statistic[] statistics { get; set; }
		public bool goaltogo { get; set; }
		public bool scoring_play { get; set; }
		public Score score { get; set; }
		public string event_type { get; set; }
	}

	public class Start_Situation
	{
		public string clock { get; set; }
		public int down { get; set; }
		public int yfd { get; set; }
		public Possession possession { get; set; }
		public Location location { get; set; }
	}

	public class Possession
	{
		public string id { get; set; }
		public string name { get; set; }
		public string market { get; set; }
		public string alias { get; set; }
		public string reference { get; set; }
	}

	public class Location
	{
		public string id { get; set; }
		public string name { get; set; }
		public string market { get; set; }
		public string alias { get; set; }
		public string reference { get; set; }
		public int yardline { get; set; }
	}

	public class End_Situation
	{
		public string clock { get; set; }
		public int down { get; set; }
		public int yfd { get; set; }
		public Possession1 possession { get; set; }
		public Location1 location { get; set; }
	}

	public class Possession1
	{
		public string id { get; set; }
		public string name { get; set; }
		public string market { get; set; }
		public string alias { get; set; }
		public string reference { get; set; }
	}

	public class Location1
	{
		public string id { get; set; }
		public string name { get; set; }
		public string market { get; set; }
		public string alias { get; set; }
		public string reference { get; set; }
		public int yardline { get; set; }
	}

	public class Score
	{
		public int sequence { get; set; }
		public string clock { get; set; }
		public int points { get; set; }
		public int home_points { get; set; }
		public int away_points { get; set; }
		public PointsAfterPlay pointsafterplay { get; set; }
	}

	public class PointsAfterPlay
	{
		public string id { get; set; }
		public float sequence { get; set; }
		public string reference { get; set; }
		public string type { get; set; }
	}

	public class Statistic
	{
		public string stat_type { get; set; }
		public int attempt { get; set; }
		public int yards { get; set; }
		public int gross_yards { get; set; }
		public int touchback { get; set; }
		public Player player { get; set; }
		public Team team { get; set; }
		public string category { get; set; }
		public int complete { get; set; }
		public int att_yards { get; set; }
		public int firstdown { get; set; }
		public int inside_20 { get; set; }
		public int goaltogo { get; set; }
		public int target { get; set; }
		public int pass_defended { get; set; }
		public int ast_tackle { get; set; }
		public int primary { get; set; }
		public int sack { get; set; }
		public float sack_yards { get; set; }
		public int tackle { get; set; }
		public int qb_hit { get; set; }
		public int tlost { get; set; }
		public int tlost_yards { get; set; }
		public int down { get; set; }
		public int fumble { get; set; }
		public int forced { get; set; }
		public int own_rec { get; set; }
		public int own_rec_yards { get; set; }
		public int _return { get; set; }
		public int penalty { get; set; }
		public int forced_fumble { get; set; }
		public int interception { get; set; }
		public int int_yards { get; set; }
		public bool nullified { get; set; }
		public int touchdown { get; set; }
		public int reception { get; set; }
		public int yards_after_catch { get; set; }
		public int endzone { get; set; }
		public int faircatch { get; set; }
		public int downed { get; set; }
		public int missed { get; set; }
		public int lost { get; set; }
		public int opp_rec { get; set; }
		public int opp_rec_yards { get; set; }
	}

	public class Player
	{
		public string id { get; set; }
		public string name { get; set; }
		public string jersey { get; set; }
		public string reference { get; set; }
		public string position { get; set; }
	}

	public class Team
	{
		public string id { get; set; }
		public string name { get; set; }
		public string market { get; set; }
		public string alias { get; set; }
		public string reference { get; set; }
	}


}
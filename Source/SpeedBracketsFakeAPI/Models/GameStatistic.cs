using System;

namespace SpeedBracketsFakeAPI.Models
{
	public class GameStatistic
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
		public Home home { get; set; }
		public Away away { get; set; }
		public object[] officials { get; set; }
	}

	public class Home
	{
		public string name { get; set; }
		public string alias { get; set; }
		public string market { get; set; }
		public string id { get; set; }
		public int points { get; set; }
		public int rank { get; set; }
		public bool double_bonus { get; set; }
		public int remaining_timeouts { get; set; }
		public StatScoring[] scoring { get; set; }
		public Statistics statistics { get; set; }
		public Coach[] coaches { get; set; }
		public StatPlayer[] players { get; set; }
	}

	public class Statistics
	{
		public string minutes { get; set; }
		public int field_goals_made { get; set; }
		public int field_goals_att { get; set; }
		public float field_goals_pct { get; set; }
		public int three_points_made { get; set; }
		public int three_points_att { get; set; }
		public float three_points_pct { get; set; }
		public int two_points_made { get; set; }
		public int two_points_att { get; set; }
		public float two_points_pct { get; set; }
		public int blocked_att { get; set; }
		public int free_throws_made { get; set; }
		public int free_throws_att { get; set; }
		public float free_throws_pct { get; set; }
		public int offensive_rebounds { get; set; }
		public int defensive_rebounds { get; set; }
		public int rebounds { get; set; }
		public int assists { get; set; }
		public int turnovers { get; set; }
		public int steals { get; set; }
		public int blocks { get; set; }
		public float assists_turnover_ratio { get; set; }
		public int personal_fouls { get; set; }
		public int ejections { get; set; }
		public int foulouts { get; set; }
		public int points { get; set; }
		public int fast_break_pts { get; set; }
		public int second_chance_pts { get; set; }
		public int team_turnovers { get; set; }
		public int points_off_turnovers { get; set; }
		public int team_rebounds { get; set; }
		public int flagrant_fouls { get; set; }
		public int player_tech_fouls { get; set; }
		public int team_tech_fouls { get; set; }
		public int coach_tech_fouls { get; set; }
		public int points_in_paint { get; set; }
		public int pls_min { get; set; }
		public float effective_fg_pct { get; set; }
		public int bench_points { get; set; }
		public int points_in_paint_att { get; set; }
		public int points_in_paint_made { get; set; }
		public float points_in_paint_pct { get; set; }
		public float true_shooting_att { get; set; }
		public float true_shooting_pct { get; set; }
		public int biggest_lead { get; set; }
		public int fouls_drawn { get; set; }
		public int offensive_fouls { get; set; }
		public int efficiency { get; set; }
		public float efficiency_game_score { get; set; }
		public Most_Unanswered most_unanswered { get; set; }
	}

	public class Most_Unanswered
	{
		public int points { get; set; }
		public int own_score { get; set; }
		public int opp_score { get; set; }
	}	

	public class StatScoring
	{
		public string type { get; set; }
		public int number { get; set; }
		public int sequence { get; set; }
		public int points { get; set; }
	}

	public class Coach
	{
		public string id { get; set; }
		public string full_name { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string position { get; set; }
	}

	public class StatPlayer
	{
		public string full_name { get; set; }
		public string jersey_number { get; set; }
		public string id { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string position { get; set; }
		public string primary_position { get; set; }
		public bool played { get; set; }
		public bool active { get; set; }
		public bool starter { get; set; }
		public Statistics1 statistics { get; set; }
	}

	public class Statistics1
	{
		public string minutes { get; set; }
		public int field_goals_made { get; set; }
		public int field_goals_att { get; set; }
		public float field_goals_pct { get; set; }
		public int three_points_made { get; set; }
		public int three_points_att { get; set; }
		public float three_points_pct { get; set; }
		public int two_points_made { get; set; }
		public int two_points_att { get; set; }
		public float two_points_pct { get; set; }
		public int blocked_att { get; set; }
		public int free_throws_made { get; set; }
		public int free_throws_att { get; set; }
		public float free_throws_pct { get; set; }
		public int offensive_rebounds { get; set; }
		public int defensive_rebounds { get; set; }
		public int rebounds { get; set; }
		public int assists { get; set; }
		public int turnovers { get; set; }
		public int steals { get; set; }
		public int blocks { get; set; }
		public float assists_turnover_ratio { get; set; }
		public int personal_fouls { get; set; }
		public int tech_fouls { get; set; }
		public int flagrant_fouls { get; set; }
		public int pls_min { get; set; }
		public int points { get; set; }
		public bool double_double { get; set; }
		public bool triple_double { get; set; }
		public float effective_fg_pct { get; set; }
		public int efficiency { get; set; }
		public float efficiency_game_score { get; set; }
		public int points_in_paint { get; set; }
		public int points_in_paint_att { get; set; }
		public int points_in_paint_made { get; set; }
		public float points_in_paint_pct { get; set; }
		public float true_shooting_att { get; set; }
		public float true_shooting_pct { get; set; }
		public int fouls_drawn { get; set; }
		public int offensive_fouls { get; set; }
		public int points_off_turnovers { get; set; }
		public int second_chance_pts { get; set; }
		public Period1[] periods { get; set; }
	}

	public class Period1
	{
		public string type { get; set; }
		public string id { get; set; }
		public int number { get; set; }
		public int sequence { get; set; }
		public string minutes { get; set; }
		public int field_goals_made { get; set; }
		public int field_goals_att { get; set; }
		public float field_goals_pct { get; set; }
		public int three_points_made { get; set; }
		public int three_points_att { get; set; }
		public float three_points_pct { get; set; }
		public int two_points_made { get; set; }
		public int two_points_att { get; set; }
		public float two_points_pct { get; set; }
		public int blocked_att { get; set; }
		public int free_throws_made { get; set; }
		public int free_throws_att { get; set; }
		public int free_throws_pct { get; set; }
		public int offensive_rebounds { get; set; }
		public int defensive_rebounds { get; set; }
		public int rebounds { get; set; }
		public int assists { get; set; }
		public int turnovers { get; set; }
		public int steals { get; set; }
		public int blocks { get; set; }
		public int assists_turnover_ratio { get; set; }
		public int personal_fouls { get; set; }
		public int offensive_fouls { get; set; }
		public int tech_fouls { get; set; }
		public int flagrant_fouls { get; set; }
		public int pls_min { get; set; }
		public int points { get; set; }
		public float effective_fg_pct { get; set; }
		public int efficiency { get; set; }
		public float efficiency_game_score { get; set; }
		public int points_in_paint { get; set; }
		public int points_in_paint_att { get; set; }
		public int points_in_paint_made { get; set; }
		public float points_in_paint_pct { get; set; }
		public float true_shooting_att { get; set; }
		public float true_shooting_pct { get; set; }
		public int fouls_drawn { get; set; }
		public int points_off_turnovers { get; set; }
		public int second_chance_pts { get; set; }
	}

	public class Away
	{
		public string name { get; set; }
		public string alias { get; set; }
		public string market { get; set; }
		public string id { get; set; }
		public int points { get; set; }
		public int rank { get; set; }
		public bool double_bonus { get; set; }
		public int remaining_timeouts { get; set; }
		public Scoring1[] scoring { get; set; }
		public Statistics2 statistics { get; set; }
		public Coach1[] coaches { get; set; }
		public Player1[] players { get; set; }
	}

	public class Statistics2
	{
		public string minutes { get; set; }
		public int field_goals_made { get; set; }
		public int field_goals_att { get; set; }
		public float field_goals_pct { get; set; }
		public int three_points_made { get; set; }
		public int three_points_att { get; set; }
		public int three_points_pct { get; set; }
		public int two_points_made { get; set; }
		public int two_points_att { get; set; }
		public float two_points_pct { get; set; }
		public int blocked_att { get; set; }
		public int free_throws_made { get; set; }
		public int free_throws_att { get; set; }
		public float free_throws_pct { get; set; }
		public int offensive_rebounds { get; set; }
		public int defensive_rebounds { get; set; }
		public int rebounds { get; set; }
		public int assists { get; set; }
		public int turnovers { get; set; }
		public int steals { get; set; }
		public int blocks { get; set; }
		public float assists_turnover_ratio { get; set; }
		public int personal_fouls { get; set; }
		public int ejections { get; set; }
		public int foulouts { get; set; }
		public int points { get; set; }
		public int fast_break_pts { get; set; }
		public int second_chance_pts { get; set; }
		public int team_turnovers { get; set; }
		public int points_off_turnovers { get; set; }
		public int team_rebounds { get; set; }
		public int flagrant_fouls { get; set; }
		public int player_tech_fouls { get; set; }
		public int team_tech_fouls { get; set; }
		public int coach_tech_fouls { get; set; }
		public int points_in_paint { get; set; }
		public int pls_min { get; set; }
		public int effective_fg_pct { get; set; }
		public int bench_points { get; set; }
		public int points_in_paint_att { get; set; }
		public int points_in_paint_made { get; set; }
		public float points_in_paint_pct { get; set; }
		public float true_shooting_att { get; set; }
		public int true_shooting_pct { get; set; }
		public int biggest_lead { get; set; }
		public int fouls_drawn { get; set; }
		public int offensive_fouls { get; set; }
		public int efficiency { get; set; }
		public float efficiency_game_score { get; set; }
		public Most_Unanswered1 most_unanswered { get; set; }
		public Period2[] periods { get; set; }
	}

	public class Most_Unanswered1
	{
		public int points { get; set; }
		public int own_score { get; set; }
		public int opp_score { get; set; }
	}

	public class Period2
	{
		public string type { get; set; }
		public string id { get; set; }
		public int number { get; set; }
		public int sequence { get; set; }
		public string minutes { get; set; }
		public int field_goals_made { get; set; }
		public int field_goals_att { get; set; }
		public float field_goals_pct { get; set; }
		public int three_points_made { get; set; }
		public int three_points_att { get; set; }
		public float three_points_pct { get; set; }
		public int two_points_made { get; set; }
		public int two_points_att { get; set; }
		public float two_points_pct { get; set; }
		public int blocked_att { get; set; }
		public int free_throws_made { get; set; }
		public int free_throws_att { get; set; }
		public float free_throws_pct { get; set; }
		public int offensive_rebounds { get; set; }
		public int defensive_rebounds { get; set; }
		public int rebounds { get; set; }
		public int assists { get; set; }
		public int turnovers { get; set; }
		public int steals { get; set; }
		public int blocks { get; set; }
		public float assists_turnover_ratio { get; set; }
		public int personal_fouls { get; set; }
		public int offensive_fouls { get; set; }
		public int points { get; set; }
		public int second_chance_pts { get; set; }
		public int team_turnovers { get; set; }
		public int points_off_turnovers { get; set; }
		public int team_rebounds { get; set; }
		public int flagrant_fouls { get; set; }
		public int player_tech_fouls { get; set; }
		public int team_tech_fouls { get; set; }
		public int coach_tech_fouls { get; set; }
		public int pls_min { get; set; }
		public float effective_fg_pct { get; set; }
		public int bench_points { get; set; }
		public int points_in_paint { get; set; }
		public int points_in_paint_att { get; set; }
		public int points_in_paint_made { get; set; }
		public float points_in_paint_pct { get; set; }
		public float true_shooting_att { get; set; }
		public float true_shooting_pct { get; set; }
		public int biggest_lead { get; set; }
		public int fouls_drawn { get; set; }
		public int efficiency { get; set; }
		public float efficiency_game_score { get; set; }
	}

	public class Scoring1
	{
		public string type { get; set; }
		public int number { get; set; }
		public int sequence { get; set; }
		public int points { get; set; }
	}

	public class Coach1
	{
		public string id { get; set; }
		public string full_name { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string position { get; set; }
	}

	public class Player1
	{
		public string full_name { get; set; }
		public string jersey_number { get; set; }
		public string id { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string position { get; set; }
		public string primary_position { get; set; }
		public bool played { get; set; }
		public bool active { get; set; }
		public Statistics3 statistics { get; set; }
		public bool starter { get; set; }
	}

	public class Statistics3
	{
		public string minutes { get; set; }
		public int field_goals_made { get; set; }
		public int field_goals_att { get; set; }
		public float field_goals_pct { get; set; }
		public int three_points_made { get; set; }
		public int three_points_att { get; set; }
		public float three_points_pct { get; set; }
		public int two_points_made { get; set; }
		public int two_points_att { get; set; }
		public float two_points_pct { get; set; }
		public int blocked_att { get; set; }
		public int free_throws_made { get; set; }
		public int free_throws_att { get; set; }
		public float free_throws_pct { get; set; }
		public int offensive_rebounds { get; set; }
		public int defensive_rebounds { get; set; }
		public int rebounds { get; set; }
		public int assists { get; set; }
		public int turnovers { get; set; }
		public int steals { get; set; }
		public int blocks { get; set; }
		public float assists_turnover_ratio { get; set; }
		public int personal_fouls { get; set; }
		public int tech_fouls { get; set; }
		public int flagrant_fouls { get; set; }
		public int pls_min { get; set; }
		public int points { get; set; }
		public bool double_double { get; set; }
		public bool triple_double { get; set; }
		public float effective_fg_pct { get; set; }
		public int efficiency { get; set; }
		public float efficiency_game_score { get; set; }
		public int points_in_paint { get; set; }
		public int points_in_paint_att { get; set; }
		public int points_in_paint_made { get; set; }
		public float points_in_paint_pct { get; set; }
		public float true_shooting_att { get; set; }
		public float true_shooting_pct { get; set; }
		public int fouls_drawn { get; set; }
		public int offensive_fouls { get; set; }
		public int points_off_turnovers { get; set; }
		public int second_chance_pts { get; set; }
		public Period3[] periods { get; set; }
	}

	public class Period3
	{
		public string type { get; set; }
		public string id { get; set; }
		public int number { get; set; }
		public int sequence { get; set; }
		public string minutes { get; set; }
		public int field_goals_made { get; set; }
		public int field_goals_att { get; set; }
		public float field_goals_pct { get; set; }
		public int three_points_made { get; set; }
		public int three_points_att { get; set; }
		public float three_points_pct { get; set; }
		public int two_points_made { get; set; }
		public int two_points_att { get; set; }
		public int two_points_pct { get; set; }
		public int blocked_att { get; set; }
		public int free_throws_made { get; set; }
		public int free_throws_att { get; set; }
		public int free_throws_pct { get; set; }
		public int offensive_rebounds { get; set; }
		public int defensive_rebounds { get; set; }
		public int rebounds { get; set; }
		public int assists { get; set; }
		public int turnovers { get; set; }
		public int steals { get; set; }
		public int blocks { get; set; }
		public float assists_turnover_ratio { get; set; }
		public int personal_fouls { get; set; }
		public int offensive_fouls { get; set; }
		public int tech_fouls { get; set; }
		public int flagrant_fouls { get; set; }
		public int pls_min { get; set; }
		public int points { get; set; }
		public float effective_fg_pct { get; set; }
		public int efficiency { get; set; }
		public float efficiency_game_score { get; set; }
		public int points_in_paint { get; set; }
		public int points_in_paint_att { get; set; }
		public int points_in_paint_made { get; set; }
		public float points_in_paint_pct { get; set; }
		public float true_shooting_att { get; set; }
		public float true_shooting_pct { get; set; }
		public int fouls_drawn { get; set; }
		public int points_off_turnovers { get; set; }
		public int second_chance_pts { get; set; }
	}
}

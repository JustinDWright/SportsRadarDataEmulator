using Microsoft.AspNetCore.Mvc;
using SpeedBracketsFakeAPI.Models;
using SpeedBracketsFakeAPI.Services;

namespace SpeedBracketsFakeAPI.Controllers.NCAA
{
	[Route("api/v1/ncaa/[controller]")]
	public class TournamentsController : Controller
    {
		private readonly TournamentService tournamentService;
		private readonly ScheduleService scheduleService;
		
		public TournamentsController(TournamentService tournamentService, ScheduleService scheduleService)
		{
			this.tournamentService = tournamentService;
			this.scheduleService = scheduleService;
		}

		[HttpGet("{year}/{seasonType}/schedule.json")]
		public Schedule GetTournamentSchedule(int year, string seasonType)
		{
			return scheduleService.GetSchedule(year, seasonType);
		}

		[HttpGet("{tournamentId}/schedule.json")]
        public Tournament GetScheduleByTournamentId(string tournamentId)
		{
			return tournamentService.GetTournament(tournamentId);
		}
    }
}

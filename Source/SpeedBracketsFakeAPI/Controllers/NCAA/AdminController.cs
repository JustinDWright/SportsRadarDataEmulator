using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpeedBrackets.Data.ActivityData.Models;
using SpeedBracketsFakeAPI.Models;
using SpeedBracketsFakeAPI.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpeedBracketsFakeAPI.Controllers.NCAA
{
	[Route("api/v1/[controller]")]
	public class AdminController : Controller
	{
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly TournamentService tournamentService;
		private readonly GameService gameService;

		public AdminController(IHttpContextAccessor httpContextAccessor, TournamentService tournamentService, GameService gameService)
		{
			this.httpContextAccessor = httpContextAccessor;
			this.tournamentService = tournamentService;
			this.gameService = gameService;
		}

		[HttpGet("LoadNcaaGames/{tournamentId}")]
		public async Task LoadGamesForTournament(string tournamentId)
		{
			var response = httpContextAccessor.HttpContext.Response;

			var tournament = tournamentService.GetTournament(tournamentId);

			var games = tournament.rounds.SelectMany(x => x.games).ToList();
			games.AddRange(tournament.rounds.SelectMany(x => x.bracketed.SelectMany(b => b.games)).ToList());

			using (var httpClient = new HttpClient())
			{
				foreach (var game in games)
				{
					try
					{
						if (!gameService.GameDataExists(game.id))
						{
							await response.WriteAsync($"Game data for {game.id} exists. Skipping. {Environment.NewLine}", Encoding.UTF8);
							response.Body.Flush();

							var uri = $"http://api.sportradar.us/ncaamb/trial/v4/en/games/{game.id}/pbp.json?api_key=spv7c7wtayj2f3vktempjyd3";
							var apiResponse = await httpClient.GetAsync(uri);
							var gameData = await apiResponse.Content.ReadAsStringAsync();

							var gameObject = JsonConvert.DeserializeObject<Game>(gameData);

							// update the game as if it hasn't take place yet.
							gameObject.status = GameStatus.Scheduled.ToString();							

							gameService.SaveGame(game.id, gameObject);							

							await response.WriteAsync($"Game data for {game.id} saved to disk. {Environment.NewLine}", Encoding.UTF8);
							response.Body.Flush();
						}

						if (!gameService.BoxScoreExists(game.id))
						{
							var uri = $"http://api.sportradar.us/ncaamb/trial/v4/en/games/{game.id}/boxscore.json?api_key=spv7c7wtayj2f3vktempjyd3";
							var apiResponse = await httpClient.GetAsync(uri);
							var boxScore = await apiResponse.Content.ReadAsStringAsync();

							gameService.SaveBoxScore(game.id, boxScore);

							await response.WriteAsync($"Boxscore data for {game.id} saved to disk. {Environment.NewLine}", Encoding.UTF8);
							response.Body.Flush();
						}

						if (!gameService.SummaryExists(game.id))
						{
							var uri = $"http://api.sportradar.us/ncaamb/trial/v4/en/games/{game.id}/summary.json?api_key=spv7c7wtayj2f3vktempjyd3";
							var apiResponse = await httpClient.GetAsync(uri);
							var summary = await apiResponse.Content.ReadAsStringAsync();

							gameService.SaveSummary(game.id, summary);

							await response.WriteAsync($"Summary data for {game.id} saved to disk. {Environment.NewLine}", Encoding.UTF8);
							response.Body.Flush();
						}
					}
					catch (Exception ex)
					{
						await response.WriteAsync($"Failed to load game {game.id} - {ex.Message} {Environment.NewLine}", Encoding.UTF8);
						response.Body.Flush();
					}
				}
			}

			await response.WriteAsync($"Complete. {Environment.NewLine}", Encoding.UTF8);
			response.Body.Flush();
		}
	}
}

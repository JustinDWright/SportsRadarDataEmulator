using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedBracketsFakeAPI.api.v1.Controllers
{
	public class BoxScoreController : Controller
	{
		// http://api.sportradar.us/nfl-ot1/games/1e636f91-ad6a-4981-9018-e4639a459a76/statistics.json
		// http://api.sportradar.us/nfl-ot1/games/1e636f91-ad6a-4981-9018-e4639a459a76/boxscore.json

		// BoxScore URI (in C#): $"/nfl-{access_level}{version}/games/{game_id}/boxscore{format}"
		// Stats URI (in C#): $"/nfl-{access_level}{version}/seasontd/{season_id}/{nfl_season}/teams/{team_id}/statistics:format"

		// GET: BoxScore
		public ActionResult Index()
		{
			return View();
		}
	}
}

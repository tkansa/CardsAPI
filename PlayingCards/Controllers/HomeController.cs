using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlayingCards.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlayingCards.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private const string CARD_API_BASE = "https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1";

		private HttpClient http = new HttpClient();

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> DrawFive()
		{

			var deckResponse = await http.GetAsync(CARD_API_BASE);
			Deck deck = await deckResponse.Content.ReadAsAsync<Deck>();
			string deckId = deck.deck_id;


			var handResponse = await http.GetAsync($"https://deckofcardsapi.com/api/deck/{deckId}/draw/?count=5");

			Deck hand = await handResponse.Content.ReadAsAsync<Deck>();

			return View(hand);
		}

		public async Task<IActionResult> DrawFiveMore(string deck_id)
		{
			var handResponse = await http.GetAsync($"https://deckofcardsapi.com/api/deck/{deck_id}/draw/?count=5");

			Deck hand = await handResponse.Content.ReadAsAsync<Deck>();

			return View("DrawFive", hand);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

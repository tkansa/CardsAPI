using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayingCards.Models
{
	public class Deck
	{
		public string deck_id { get; set; }
		public List<Card> cards { get; set; }
	}
}

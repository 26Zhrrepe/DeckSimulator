using System.Collections.Generic;
using System.Linq;

namespace DeckSimulator
{
    public class Deck
    {
        public List<Card> Cards { get; set; } = new();

        public void AddCard(Card card)
        {
            var existing = Cards.FirstOrDefault(c => c.Name == card.Name);
            if (existing != null)
            {
                existing.Count += card.Count;
            }
            else
            {
                Cards.Add(card);
            }
        }
    }

}

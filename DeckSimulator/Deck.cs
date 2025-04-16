using System.Collections.Generic;
using System.Linq;

namespace DeckSimulator
{
    public class Deck
    {
        public List<Card> Cards { get; set; } = new();

        public void AddCard(Card card, int count)
        {
            var existing = Cards.FirstOrDefault(c => c.Name == card.Name);
            if (existing != null)
            {
                existing.Count += count;
            }
            else
            {
                Cards.Add(new Card
                {
                    Name = card.Name,
                    Count = count,
                    CardType = card.CardType,
                    Attribute = card.Attribute,
                    Race = card.Race,
                    Categories = card.Categories?.ToList() ?? new List<string>()
                });
            }
        }
        public void RemoveCard(Card card)
        {
            Cards.Remove(card);
        }
    }

}

using AgilePoker.WebUI.Components.Card;
using System.Collections.Generic;

namespace AgilePoker.WebUI.Models
{
    public class GameModel
    {
        public IList<CardModel> Cards { get; set; } = new List<CardModel>
        {
            new CardModel ("1"),
            new CardModel ("2"),
            new CardModel ("3"),
            new CardModel ("5"),
            new CardModel ("8"),
            new CardModel ("13"),
            new CardModel ("20"),
            new CardModel ("40"),
            new CardModel ("100"),
            new CardModel ("∞"),
            new CardModel ("☕"),
        };

        public IList<PlayerModel> Players { get; set; } = new List<PlayerModel>();

        public PlayerModel CurrentPlayer { get; set; }

        public bool Revealed { get; set; }
    }
}

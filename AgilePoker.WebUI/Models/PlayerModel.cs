using AgilePoker.WebUI.Components.Card;
using System;
using System.ComponentModel.DataAnnotations;

namespace AgilePoker.WebUI.Models
{
    public class PlayerModel : IEquatable<PlayerModel>
    {
        public PlayerModel() { }

        public PlayerModel(int playerId, string name)
        {
            PlayerId = playerId;
            Name = name;
        }

        public PlayerModel(int playerId, string name, bool guest) 
            : this(playerId, name)
        {
            Guest = guest;
        }

        public PlayerModel(int playerId, string name, bool guest, CardModel selectedCard) 
            : this(playerId, name, guest)
        {
            SelectedCard = selectedCard;
        }

        public int PlayerId { get; set; } = new Random().Next();

        [Required]
        public string Name { get; set; }

        public bool Guest { get; set; }

        public CardModel SelectedCard { get; set; } = new CardModel();

        public bool Equals(PlayerModel player)
        {
            return PlayerId == player.PlayerId;
        }
    }
}

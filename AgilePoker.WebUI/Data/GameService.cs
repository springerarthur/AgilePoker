using AgilePoker.WebUI.Components.Card;
using AgilePoker.WebUI.Models;
using System.Collections.Generic;
using System.Linq;

namespace AgilePoker.WebUI.Data
{
    public class GameService : IGameService 
    {
        private static IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public PlayerModel GetPlayer(int playerId)
        {
            return _gameRepository.Players.FirstOrDefault(p => p.PlayerId == playerId);
        }

        public IList<PlayerModel> GetPlayers()
        {
            return _gameRepository.Players;
        }

        public IList<PlayerModel> JoinGame(PlayerModel player)
        {
            if (!_gameRepository.Players.Contains(player))
            {
                _gameRepository.Players.Add(player);
            }

            return _gameRepository.Players;
        }

        public IList<PlayerModel> KickPlayer(PlayerModel player)
        {
            return player != null
                ? KickPlayer(player.PlayerId)
                : _gameRepository.Players;
        }

        public IList<PlayerModel> KickPlayer(int playerId)
        {
            var player = _gameRepository.Players.FirstOrDefault(p => p.PlayerId == playerId);
            if (player != null)
            {
                _gameRepository.Players.Remove(player);
            }

            return _gameRepository.Players;
        }

        public IList<PlayerModel> RestartGame()
        {
            foreach (var player in _gameRepository.Players)
            {
                player.SelectedCard = new CardModel();
            }

            return _gameRepository.Players;
        }
    }
}

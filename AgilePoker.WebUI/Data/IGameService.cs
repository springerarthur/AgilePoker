using AgilePoker.WebUI.Models;
using System.Collections.Generic;

namespace AgilePoker.WebUI.Data
{
    public interface IGameService
    {
        PlayerModel GetPlayer(int playerId);
        IList<PlayerModel> GetPlayers();
        IList<PlayerModel> JoinGame(PlayerModel player);
        IList<PlayerModel> KickPlayer(PlayerModel player);
        IList<PlayerModel> KickPlayer(int playerId);
        IList<PlayerModel> RestartGame();
    }
}
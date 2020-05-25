using AgilePoker.WebUI.Data;
using AgilePoker.WebUI.Models;

namespace AgilePoker.WebUI.Tests.Data
{
    public static class FluentGameRepositoryGenerator
    {
        public static IGameRepository AddDeveloper(this IGameRepository gameRepository, int id, string name)
        {
            gameRepository.Players.Add(new PlayerModel(id, name));
            return gameRepository;
        }
        public static IGameRepository AddGuest(this IGameRepository gameRepository, int id, string name)
        {
            gameRepository.Players.Add(new PlayerModel(id, name, true));
            return gameRepository;
        }
    }
}

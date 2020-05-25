using AgilePoker.WebUI.Data;
using AgilePoker.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AgilePoker.WebUI.Tests.Data
{
    [TestClass]
    public class GameServiceTests
    {
        #region GetPlayer
        [DataTestMethod]
        [DataRow(123, "Developer 1")]
        [DataRow(789, "Developer 2")]
        [DataRow(321, "Guest")]
        public void GetPlayer_ShouldFindThePlayerById(int id, string name)
        {
            IGameRepository gameRepository = CreateDummyGame();
            var gameService = new GameService(gameRepository);

            var foundPlayer = gameService.GetPlayer(id);

            Assert.AreEqual(name, foundPlayer.Name);
        }

        [TestMethod]
        public void GetPlayer_ShouldReturnNull_IfUserNotExists()
        {
            IGameRepository gameRepository = CreateDummyGame();
            var gameService = new GameService(gameRepository);

            var foundPlayer = gameService.GetPlayer(123456);

            Assert.AreEqual(null, foundPlayer);
        }

        [TestMethod]
        public void GetPlayers_ShouldReturnEveryPlayer()
        {
            IGameRepository gameRepository = CreateDummyGame();
            var gameService = new GameService(gameRepository);

            var players = gameService.GetPlayers();

            Assert.AreEqual(3, players.Count);
            Assert.AreEqual(123, players[0].PlayerId);
            Assert.AreEqual(321, players[1].PlayerId);
            Assert.AreEqual(789, players[2].PlayerId);
        }

        #endregion

        #region Join game

        [TestMethod]
        public void JoindGame_AsDeveloper_ShouldJoinAnExistingGame()
        {
            IGameRepository gameRepository = CreateDummyGame();
            var gameService = new GameService(gameRepository);

            var newPlayer = new PlayerModel(123456, "New");
            var players = gameService.JoinGame(newPlayer);

            Assert.AreEqual(4, players.Count);
            Assert.AreEqual(123456, players[3].PlayerId);
        }

        [TestMethod]
        public void JoindGame_AsGuest_ShouldJoinAnExistingGame()
        {
            IGameRepository gameRepository = CreateDummyGame();
            var gameService = new GameService(gameRepository);

            var newPlayer = new PlayerModel(123456, "New Guest", true);
            var players = gameService.JoinGame(newPlayer);

            Assert.AreEqual(4, players.Count);
            Assert.AreEqual(123456, players[3].PlayerId);
        }

        [TestMethod]
        public void JoindGame_AsDeveloper_ShouldJoinAnEmptyGame()
        {
            IGameRepository gameRepository = CreateEmptyGame();
            var gameService = new GameService(gameRepository);

            var newPlayer = new PlayerModel(123456, "New");
            var players = gameService.JoinGame(newPlayer);

            Assert.AreEqual(1, players.Count);
            Assert.AreEqual(123456, players[0].PlayerId);
        }

        [TestMethod]
        public void JoindGame_AsGuest_ShouldJoinAnEmptyGame()
        {
            IGameRepository gameRepository = CreateEmptyGame();
            var gameService = new GameService(gameRepository);

            var newPlayer = new PlayerModel(123456, "New Guest", true);
            var players = gameService.JoinGame(newPlayer);

            Assert.AreEqual(1, players.Count);
            Assert.AreEqual(123456, players[0].PlayerId);
        }

        [TestMethod]
        public void JoindGame_ShouldNotJoin_IfHeAlreadyJoinedTheGame()
        {
            IGameRepository gameRepository = CreateDummyGame();
            var gameService = new GameService(gameRepository);

            var newPlayer = new PlayerModel(123, "New Name");
            var players = gameService.JoinGame(newPlayer);

            Assert.AreEqual(3, players.Count);
            Assert.AreEqual(123, players[0].PlayerId);
            Assert.AreEqual("Developer 1", players[0].Name);
        }

        #endregion

        #region Kick player

        [TestMethod]
        public void KickPlayer_ShouldKickAPlayer_IfThePlayerExistsInTheGame()
        {
            IGameRepository gameRepository = CreateDummyGame();
            var gameService = new GameService(gameRepository);

            var newPlayer = new PlayerModel(123, "New");
            var players = gameService.KickPlayer(newPlayer);

            Assert.AreEqual(2, players.Count);
            Assert.AreEqual(321, players[0].PlayerId);
            Assert.AreEqual(789, players[1].PlayerId);
        }

        [TestMethod]
        public void KickPlayer_ShouldKickAPlayerById_IfThePlayerExistsInTheGame()
        {
            IGameRepository gameRepository = CreateDummyGame();
            var gameService = new GameService(gameRepository);

            var players = gameService.KickPlayer(123);

            Assert.AreEqual(2, players.Count);
            Assert.AreEqual(321, players[0].PlayerId);
            Assert.AreEqual(789, players[1].PlayerId);
        }

        [TestMethod]
        public void KickPlayer_ShouldDoNothing_IfThePlayerDidNotExistsInTheGame()
        {
            IGameRepository gameRepository = CreateDummyGame();
            var gameService = new GameService(gameRepository);

            var players = gameService.KickPlayer(123456);

            Assert.AreEqual(3, players.Count);
            Assert.AreEqual(123, players[0].PlayerId);
            Assert.AreEqual(321, players[1].PlayerId);
            Assert.AreEqual(789, players[2].PlayerId);
        }

        [TestMethod]
        public void KickPlayer_ShouldDoNothing_IfThePlayerIsNull()
        {
            IGameRepository gameRepository = CreateDummyGame();
            var gameService = new GameService(gameRepository);

            var players = gameService.KickPlayer(null);

            Assert.AreEqual(3, players.Count);
            Assert.AreEqual(123, players[0].PlayerId);
            Assert.AreEqual(321, players[1].PlayerId);
            Assert.AreEqual(789, players[2].PlayerId);
        }

        #endregion

        #region Restart game

        [TestMethod]
        public void RestartGame_ShouldUnselectEveryPlayersCard()
        {
            IGameRepository gameRepository = CreateDummyGame();
            var gameService = new GameService(gameRepository);

            var players = gameService.RestartGame();

            Assert.AreEqual(3, players.Count);
            foreach (var player in players)
            {
                Assert.IsFalse(player.SelectedCard.Selected);
            }
        }

        [TestMethod]
        public void KickPlayer_ShouldDoNothing_IfTheGameIsEmpty()
        {
            IGameRepository gameRepository = CreateEmptyGame();
            var gameService = new GameService(gameRepository);

            var players = gameService.RestartGame();

            Assert.AreEqual(0, players.Count);
        }

        #endregion

        private static IGameRepository CreateEmptyGame() => new GameRepositoryMock();

        private static IGameRepository CreateDummyGame()
        {
            return new GameRepositoryMock()
                .AddDeveloper(123, "Developer 1")
                .AddGuest(321, "Guest")
                .AddDeveloper(789, "Developer 2");
        }
    }
}
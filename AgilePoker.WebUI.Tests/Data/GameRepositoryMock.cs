using AgilePoker.WebUI.Data;
using AgilePoker.WebUI.Models;
using System.Collections.Generic;

namespace AgilePoker.WebUI.Tests.Data
{
    public class GameRepositoryMock : IGameRepository
    {
        public IList<PlayerModel> Players { get; set; } = new List<PlayerModel>();
    }
}

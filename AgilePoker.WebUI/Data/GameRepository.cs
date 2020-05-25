using AgilePoker.WebUI.Models;
using System.Collections.Generic;

namespace AgilePoker.WebUI.Data
{
    public class GameRepository : IGameRepository
    {
        public IList<PlayerModel> Players { get; set; } = new List<PlayerModel>();
    }
}

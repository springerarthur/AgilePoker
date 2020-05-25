using AgilePoker.WebUI.Models;
using System.Collections.Generic;

namespace AgilePoker.WebUI.Data
{
    public interface IGameRepository
    {
        IList<PlayerModel> Players { get; set; }
    }
}
using AgilePoker.WebUI.Components.Card;
using AgilePoker.WebUI.Data;
using AgilePoker.WebUI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgilePoker.WebUI.Pages.Game
{
    public class GameBase : ComponentBase, IAsyncDisposable
    {
        private HubConnection _hubConnection;

        [Inject]
        protected IHubContext<GameHub> HubContext { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IGameService GameService { get; set; }

        /// <summary>
        /// The Id of the current player.
        /// </summary>
        [Parameter]
        public int PlayerId { get; set; }

        protected GameModel Game { get; set; } = new GameModel();

        protected override async Task OnInitializedAsync()
        {
            await InitUpdateHub();
            PlayerModel currentPlayer = GetPlayerOrNavigateToStart();

            Game.CurrentPlayer = currentPlayer;
            SelectCard(currentPlayer?.SelectedCard);
            Game.Players = GameService.GetPlayers();

            await HubContext.Clients.All.SendAsync("updatePlayers");
        }

        protected bool EveryoneHasChosen()
        {
            foreach (var player in Game.Players.Where(p => !p.Guest))
            {
                if(!player.SelectedCard.Selected)
                {
                    return false;
                }
            }
            return true;
        }


        protected async Task RestartGameClickedAsync()
        {
            await HubContext.Clients.All.SendAsync("restartGame");
        }


        protected async Task CardSelectedAsync(CardModel selectedCard)
        {
            ResetSelection();

            selectedCard.Selected = true;

            Game.CurrentPlayer.SelectedCard = selectedCard;

            await HubContext.Clients.All.SendAsync("updatePlayers");
        }

        protected async Task KickPlayerClickedAsync(PlayerModel player)
        {
            GameService.KickPlayer(player);

            await HubContext.Clients.All.SendAsync("kickPlayer");
        }

        public async ValueTask DisposeAsync()
        {
            await _hubConnection.DisposeAsync();
        }

        private void SelectCard(CardModel selectedCard)
        {
            if (selectedCard != null)
            {
                var card = Game.Cards.FirstOrDefault(c => c.Value.Equals(selectedCard.Value, StringComparison.OrdinalIgnoreCase));
                if (card != null)
                {
                    card.Selected = true;
                }
            }
        }

        private async Task InitUpdateHub()
        {
            _hubConnection = new HubConnectionBuilder()
                            .WithUrl(NavigationManager.ToAbsoluteUri("gameHub"))
                            .Build();

            await _hubConnection.StartAsync();

            _hubConnection.On("updatePlayers", UpdatePlayersAsync);
            _hubConnection.On("revealCards", RevealCards);
            _hubConnection.On("restartGame", RestartGame);
            _hubConnection.On("kickPlayer", UpdatePlayersAsync);
        }

        private async Task UpdatePlayersAsync()
        {
            Game.Players = GameService.GetPlayers();
            
            GetPlayerOrNavigateToStart();
            
            if (Game.Players.Count(p => !p.Guest && !p.SelectedCard.Selected) == 0)
            {
                await HubContext.Clients.All.SendAsync("revealCards");
            }

            StateHasChanged();
        }

        private void RestartGame()
        {
            ResetSelection();
            Game.Revealed = false;
            GameService.RestartGame();
            StateHasChanged();
        }

        private void RevealCards()
        {
            Game.Revealed = true;
            StateHasChanged();
        }


        private void ResetSelection()
        {
            foreach (var card in Game.Cards)
            {
                card.Selected = false;
            }
        }

        private PlayerModel GetPlayerOrNavigateToStart()
        {
            var currentPlayer = GameService.GetPlayer(PlayerId);
            if (currentPlayer == null)
            {
                NavigationManager.NavigateTo("/");
            }

            return currentPlayer;
        }
    }
}

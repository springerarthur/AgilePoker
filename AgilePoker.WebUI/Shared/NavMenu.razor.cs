using AgilePoker.WebUI.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace AgilePoker.WebUI.Shared
{
    public class NavMenuBase : ComponentBase
    {
        [Inject]
        protected IHubContext<GameHub> HubContext { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IGameService GameService { get; set; }

        protected async Task OnLeaveClickedAsync()
        {
            var currentUri = NavigationManager.Uri;
            
            if (int.TryParse(currentUri.Split("/").Last(), out int playerId))
            {
                GameService.KickPlayer(playerId);
            }

            await HubContext.Clients.All.SendAsync("updatePlayers");

            NavigationManager.NavigateTo("/");
        }

        protected override void OnInitialized()
        {
            NavigationManager.LocationChanged += LocationChanged;
            base.OnInitialized();
        }

        private void LocationChanged(object sender, LocationChangedEventArgs e)
        {
            StateHasChanged();
        }
    }
}
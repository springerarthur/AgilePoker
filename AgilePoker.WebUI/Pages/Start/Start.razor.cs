using AgilePoker.WebUI.Data;
using AgilePoker.WebUI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace AgilePoker.WebUI.Pages
{
    public class StartBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IGameService GameService { get; set; }
                
        protected PlayerModel Player { get; set; } = new PlayerModel();

        protected void JoinGame()
        {
            NavigationManager.NavigateTo($"/play/{Player.PlayerId}");
            GameService.JoinGame(Player);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("jsFunctions.focusElement", "name");
            }
        }
    }
}

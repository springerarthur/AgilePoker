using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AgilePoker.WebUI.Components.Card
{
    public class CardBase : ComponentBase
    {
        [Parameter]
        public CardModel Card { get; set; }

        [Parameter]
        public bool FacedDown { get; set; }

        [Parameter]
        public bool Locked { get; set; }

        [Parameter]
        public EventCallback<CardModel> OnSelected { get; set; }

        public async Task SelectAsync()
        {
            if(!Locked) { 
                await OnSelected.InvokeAsync(Card);
            }
        }
    }
}

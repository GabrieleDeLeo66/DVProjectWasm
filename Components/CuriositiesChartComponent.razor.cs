using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DVProject_Wasm.Components
{
    public partial class CuriositiesChartComponent : ComponentBase
    {
        [Parameter]
        public string SelectedCountry { get; set; } = "Italy";
        [Parameter]
        public bool Loading { get; set; }

        public bool RenderCooldown = false;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!RenderCooldown)
            {
                if (firstRender)
                {
                    await base.OnAfterRenderAsync(firstRender);
                    await Task.Delay(1000);
                    await JS.InvokeVoidAsync("CreateChart", SelectedCountry);
                }
                else
                {
                    await Task.Delay(1000);
                    await JS.InvokeVoidAsync("UpdateChart", SelectedCountry);
                }
                Loading = false;
                RenderCooldown = true;
                StateHasChanged();
            }
            else
            {
                RenderCooldown = false;
            }
        }
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DVProject_Wasm.Components
{
    public partial class MatchingChartComponent : ComponentBase
    {
        [Parameter]
        public string SelectedCountry { get; set; } = "Italy";
        [Parameter]
        public string SelectedBar { get; set; } = "new_cases";
        [Parameter]
        public string SelectedLine { get; set; } = "total_vaccinations";
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
                    await JS.InvokeVoidAsync("CreateChart", SelectedCountry, SelectedBar, SelectedLine);
                }
                else
                {
                    await Task.Delay(1000);
                    await JS.InvokeVoidAsync("UpdateChart", SelectedCountry, SelectedBar, SelectedLine);
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

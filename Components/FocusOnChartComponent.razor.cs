using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DVProject_Wasm.Components
{
    public partial class FocusOnChartComponent : ComponentBase
    {
        [Parameter]
        public string SelectedField { get; set; } = "total_cases";

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
                    await JS.InvokeVoidAsync("CreateChart", SelectedField);
                }
                else
                {
                    await Task.Delay(1000);
                    await JS.InvokeVoidAsync("UpdateChart", SelectedField);
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

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DVProject_Wasm.Components
{
    public partial class FocusOnChartComponent : ComponentBase
    {
        [Parameter]
        public string SelectedField { get; set; } = "total_cases";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await Task.Delay(1000);
                await JS.InvokeVoidAsync("CreateChart", SelectedField);
            }
            else
            {
                await Task.Delay(1000);
                await JS.InvokeVoidAsync("UpdateChart", SelectedField);
            }
        }
    }
}

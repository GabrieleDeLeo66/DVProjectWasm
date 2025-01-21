using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DVProject_Wasm.Components
{
    public partial class CuriositiesChartComponent : ComponentBase
    {
        [Parameter]
        public string SelectedCountry { get; set; } = "Italy";
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await Task.Delay(1000);
                await JS.InvokeVoidAsync("CreateChart", SelectedCountry);
            }
            else
            {
                await Task.Delay(1000);
                await JS.InvokeVoidAsync("UpdateChart", SelectedCountry);
            }
        }
    }
}

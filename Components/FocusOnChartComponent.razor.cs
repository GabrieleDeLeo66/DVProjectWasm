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

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
        }

        private void NavigationManager_LocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            Dispose();
        }
        public void Dispose()
        {
            NavigationManager.LocationChanged -= NavigationManager_LocationChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!RenderCooldown)
            {
                if (firstRender)
                {
                    await base.OnAfterRenderAsync(firstRender);
                    await Task.Delay(2000);
                    await JS.InvokeVoidAsync("CreateChart", SelectedField);
                    await Task.Delay(2000);
                }
                else
                {
                    await JS.InvokeVoidAsync("UpdateChart", SelectedField);
                    await Task.Delay(2000);
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

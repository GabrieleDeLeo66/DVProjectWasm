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
                    await Task.Delay(1000);
                    await JS.InvokeVoidAsync("CreateChart2", SelectedCountry, SelectedBar, SelectedLine);
                }
                else
                {
                    await JS.InvokeVoidAsync("UpdateChart2", SelectedCountry, SelectedBar, SelectedLine);
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

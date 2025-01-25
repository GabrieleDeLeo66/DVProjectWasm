using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DVProject_Wasm.Components
{
    public partial class EuropeChartComponent : ComponentBase, IDisposable
    {
        private DateTime? _date = DateTime.Parse("2021-01-10");
        [Parameter]
        public DateTime? SelectedDate { get; set; }
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
                    await Task.Delay(4000);
                    await JS.InvokeVoidAsync("MapEurope", SelectedDate?.ToString("yyyy-MM-dd"));
                }
                else
                {
                    await Task.Delay(2000);
                    await JS.InvokeVoidAsync("UpdateEurope", SelectedDate?.ToString("yyyy-MM-dd"));
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

        //public DateTime? Date
        //{
        //    get { return _date; }
        //    set 
        //    { 
        //        _date = value;
        //        var valueString = value?.ToString("yyyy-MM-dd");
        //        JS.InvokeVoidAsync("UpdateEurope", valueString);
        //    }
        //}


        public async Task OnDateChanged(DateTime? value)
        {
            Loading = true;
            RenderCooldown = true;
            StateHasChanged();
            _date = value;
            var valueString = value?.ToString("yyyy-MM-dd");
            await Task.Delay(5000);
            await JS.InvokeVoidAsync("UpdateEurope", valueString);
            StateHasChanged();
        }

    }
}

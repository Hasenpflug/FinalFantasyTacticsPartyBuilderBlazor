using FinalFantasyTacticsPartyBuilderBlazor.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinalFantasyTacticsPartyBuilderBlazor.Services
{
    public class JavascriptService
    {
        private readonly IJSRuntime _js;

        public JavascriptService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task InitializeJobSelectionChartJavascriptAsync()
        {
            await Task.WhenAll(_js.InvokeVoidAsync("JobOverview.initializeJobModifierChart").AsTask(),
                _js.InvokeVoidAsync("JobOverview.initializeJobGrowthChart").AsTask(),
                _js.InvokeVoidAsync("JobOverview.initializeJobMoveChart").AsTask(),
                _js.InvokeVoidAsync("JobOverview.initializeJobEvasionChart").AsTask());
        }

        public async Task InitializeJobSelectionWheelJavascriptAsync()
        {
            await _js.InvokeVoidAsync("JobOverview.initializeJobWheel").AsTask();
        }

        public async Task UpdateSelectedJobChartDataAsync(JobOverviewViewModel jobViewModel)
        {
            string jobJson = JsonSerializer.Serialize(jobViewModel);
            await _js.InvokeVoidAsync("JobOverview.updateChartData", jobJson);
        }

        public async Task SetInitialUnitDataAsync()
        {
            List<UnitDetailsViewModel> units = new List<UnitDetailsViewModel>();
            await SetUnitDataAsync(units);
        }

        public async Task SetUnitDataAsync(List<UnitDetailsViewModel> units)
        {
            string json = JsonSerializer.Serialize(units);
            await _js.InvokeVoidAsync($"localStorage.setItem", "unitData", json);
        }

        public async Task<List<UnitDetailsViewModel>> GetUnitDataAsync()
        {
            try
            {
                string serializedJson = await _js.InvokeAsync<string>("localStorage.getItem", "unitData");
                return JsonSerializer.Deserialize<List<UnitDetailsViewModel>>(serializedJson);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<UnitOverviewPanelViewModel>> GetUnitPanelDataAsync()
        {
            List<UnitDetailsViewModel> units = await GetUnitDataAsync();
            List<UnitOverviewPanelViewModel> unitPanels;

            if (units == null)
            {
                return null;
            }

            unitPanels = units.Select(m => new UnitOverviewPanelViewModel
            {
                Gender = m.Unit.GenderName,
                MaxHP = m.Unit.MaxHP,
                MaxMP = m.Unit.MaxMP,
                JobID = m.Unit.JobID,
                Position = m.Unit.Position                
            }).ToList();

            return unitPanels;
        }

        public async Task AddUnitDataAsync(UnitDetailsViewModel unit)
        {
            List<UnitDetailsViewModel> units = await GetUnitDataAsync();
            units.Add(unit);
            await SetUnitDataAsync(units);
        }

        public async Task RemoveUnitDataAsync(int unitPosition)
        {
            List<UnitDetailsViewModel> units = await GetUnitDataAsync();
            units.RemoveAt(unitPosition);
            await SetUnitDataAsync(units);
        }
    }
}

// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace BlazorApplication.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\kaizenplus\backend\BlazorApplication\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\kaizenplus\backend\BlazorApplication\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\kaizenplus\backend\BlazorApplication\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\kaizenplus\backend\BlazorApplication\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\kaizenplus\backend\BlazorApplication\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\kaizenplus\backend\BlazorApplication\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\kaizenplus\backend\BlazorApplication\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\kaizenplus\backend\BlazorApplication\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\kaizenplus\backend\BlazorApplication\_Imports.razor"
using BlazorApplication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\kaizenplus\backend\BlazorApplication\_Imports.razor"
using BlazorApplication.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\kaizenplus\backend\BlazorApplication\Pages\WareHouse.razor"
using BlazorApplication.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\kaizenplus\backend\BlazorApplication\Pages\WareHouse.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\kaizenplus\backend\BlazorApplication\Pages\WareHouse.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\kaizenplus\backend\BlazorApplication\Pages\WareHouse.razor"
using Blazored.LocalStorage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\kaizenplus\backend\BlazorApplication\Pages\WareHouse.razor"
using System.Net.Http.Headers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\kaizenplus\backend\BlazorApplication\Pages\WareHouse.razor"
           [Authorize]

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/WareHouse")]
    public partial class WareHouse : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 67 "D:\kaizenplus\backend\BlazorApplication\Pages\WareHouse.razor"
       
    private Warehouses WarehousesList;
    private int currentPage = 1;
    private bool showInsertPopup = false;
    private Warehouse newWarehouse = new Warehouse(); // New warehouse object for inserting

    protected override async Task OnInitializedAsync()
    {
        await GetData("");
    }

    private async Task SearchChanged(ChangeEventArgs e)
    {
        currentPage = 1; // Reset to first page when performing a new search
        await GetData(e.Value.ToString());
    }

    private async Task GetData(string Search)
    {
        var requestContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Search", Search),
            new KeyValuePair<string, string>("PageSize", "10"),
            new KeyValuePair<string, string>("PageNumber", currentPage.ToString())
    });

        var token = await localStorage.GetItemAsync<LoginRoot>("user");
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.data.token);

        var response = await httpClient.PostAsync("http://localhost:5000/api/Warehouse/search", requestContent);
        if (response.IsSuccessStatusCode)
        {
            WarehousesList = await response.Content.ReadFromJsonAsync<Warehouses>();
        }
    }

    private async Task DeleteWarehouse(int id)
    {
        var token = await localStorage.GetItemAsync<LoginRoot>("user");
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.data.token);

        var response = await httpClient.DeleteAsync($"http://localhost:5000/api/Warehouse/{id}");
        if (response.IsSuccessStatusCode)
        {
            await GetData(""); // Refresh data after deletion
        }
    }

    private void ShowInsertPopup()
    {
        showInsertPopup = true;
    }

    private async Task InsertWarehouse()
    {
        var token = await localStorage.GetItemAsync<LoginRoot>("user");
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.data.token);

        var requestContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Name", newWarehouse.name),
            new KeyValuePair<string, string>("Address", newWarehouse.address),
            new KeyValuePair<string, string>("City", newWarehouse.city),
             new KeyValuePair<string, string>("CountryId", newWarehouse.countryId.ToString())

    });
        var response = await httpClient.PostAsync("http://localhost:5000/api/Warehouse", requestContent);
        if (response.IsSuccessStatusCode)
        {
            newWarehouse = new Warehouse(); // Clear the new warehouse object
            showInsertPopup = false; // Close the popup
            await GetData(""); // Refresh data after insertion
        }
    }


    public class Warehouse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int countryId { get; set; }
        public string countryName { get; set; }
        
    }

    public class Data
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int countryId { get; set; }
        public string countryName { get; set; }
        public string warehouseItemsCount { get; set; }
    }

    public class Rows
    {
        public IList<Data> data { get; set; }
        public int totalRows { get; set; }
    }

    public class Warehouses
    {
        public int errorCode { get; set; }
        public string errorMessage { get; set; }
        public bool success { get; set; }
        public Rows data { get; set; }
    }
    


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ILocalStorageService localStorage { get; set; }
    }
}
#pragma warning restore 1591

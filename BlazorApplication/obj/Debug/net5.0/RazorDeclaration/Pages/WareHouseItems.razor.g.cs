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
#line 2 "D:\kaizenplus\backend\BlazorApplication\Pages\WareHouseItems.razor"
using BlazorApplication.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\kaizenplus\backend\BlazorApplication\Pages\WareHouseItems.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\kaizenplus\backend\BlazorApplication\Pages\WareHouseItems.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\kaizenplus\backend\BlazorApplication\Pages\WareHouseItems.razor"
using Blazored.LocalStorage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\kaizenplus\backend\BlazorApplication\Pages\WareHouseItems.razor"
using System.Net.Http.Headers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\kaizenplus\backend\BlazorApplication\Pages\WareHouseItems.razor"
using static BlazorApplication.Pages.WareHouse;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\kaizenplus\backend\BlazorApplication\Pages\WareHouseItems.razor"
           [Authorize]

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/warehouseitems")]
    public partial class WareHouseItems : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 80 "D:\kaizenplus\backend\BlazorApplication\Pages\WareHouseItems.razor"
       
    private WarehouseItems WarehouseItemsList;
    private Warehouses WarehousesList;
    private int currentPage = 1;
    private bool showInsertPopup = false;
    private WarehouseItemData newWarehouseItem = new WarehouseItemData(); // New warehouse item object for inserting

    protected override async Task OnInitializedAsync()
    {
        await GetData("");
        await GetWarehouses(); // Get warehouses for dropdown list
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

        var response = await httpClient.PostAsync("http://localhost:5000/api/WarehouseItems/search", requestContent);
        if (response.IsSuccessStatusCode)
        {
            WarehouseItemsList = await response.Content.ReadFromJsonAsync<WarehouseItems>();
        }
    }
    private void ShowInsertPopup()
    {
        showInsertPopup = true;
    }
    private async Task GetWarehouses()
    {
        var token = await localStorage.GetItemAsync<LoginRoot>("user");
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.data.token);

        var requestContent = new FormUrlEncodedContent(new[]
       {
            new KeyValuePair<string, string>("Search", ""),
            new KeyValuePair<string, string>("PageSize", "999999"),
            new KeyValuePair<string, string>("PageNumber","1")
    });
        var response = await httpClient.PostAsync("http://localhost:5000/api/Warehouse/search", requestContent);
        if (response.IsSuccessStatusCode)
        {
            WarehousesList = await response.Content.ReadFromJsonAsync<Warehouses>();
        }
    }

    private async Task DeleteWarehouseItem(int id)
    {
        var token = await localStorage.GetItemAsync<LoginRoot>("user");
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.data.token);

        var response = await httpClient.DeleteAsync($"http://localhost:5000/api/WarehouseItems/{id}");
        if (response.IsSuccessStatusCode)
        {
            await GetData(""); // Refresh data after deletion
        }
    }
    private async Task InsertWarehouseItem()
    {
        var token = await localStorage.GetItemAsync<LoginRoot>("user");
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.data.token);

        var requestContent = new FormUrlEncodedContent(new[]
        {
        new KeyValuePair<string, string>("Name", newWarehouseItem.name),
        new KeyValuePair<string, string>("SKU", newWarehouseItem.sku),
        new KeyValuePair<string, string>("Quantity", newWarehouseItem.quantity.ToString()),
        new KeyValuePair<string, string>("CostPrice", newWarehouseItem.costPrice.ToString()),
        new KeyValuePair<string, string>("MSRPPrice", newWarehouseItem.msrpPrice.ToString()),
        new KeyValuePair<string, string>("WarehouseId", newWarehouseItem.warehouseId.ToString())
    });

        var response = await httpClient.PostAsync("http://localhost:5000/api/WarehouseItems", requestContent);
        if (response.IsSuccessStatusCode)
        {
            newWarehouseItem = new WarehouseItemData(); // Clear the new warehouse item object
            showInsertPopup = false; // Close the popup
            await GetData(""); // Refresh data after insertion
        }
    }
    public class WarehouseItemData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string sku { get; set; }
        public int quantity { get; set; }
        public double costPrice { get; set; }
        public double msrpPrice { get; set; }
        public int warehouseId { get; set; }
        public string warehouseName { get; set; }
    }

    public class WarehouseItemRows
    {
        public IList<WarehouseItemData> data { get; set; }
        public int totalRows { get; set; }
    }

    public class WarehouseItems
    {
        public int errorCode { get; set; }
        public string errorMessage { get; set; }
        public bool success { get; set; }
        public WarehouseItemRows data { get; set; }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ILocalStorageService localStorage { get; set; }
    }
}
#pragma warning restore 1591
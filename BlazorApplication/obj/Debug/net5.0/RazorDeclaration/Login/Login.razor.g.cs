// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace BlazorApplication.Login
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
#line 11 "D:\kaizenplus\backend\BlazorApplication\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\kaizenplus\backend\BlazorApplication\Login\Login.razor"
using BlazorApplication.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\kaizenplus\backend\BlazorApplication\Login\Login.razor"
using Blazored.LocalStorage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\kaizenplus\backend\BlazorApplication\Login\Login.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\kaizenplus\backend\BlazorApplication\Login\Login.razor"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\kaizenplus\backend\BlazorApplication\Login\Login.razor"
           [AllowAnonymous]

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/login")]
    public partial class Login : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 31 "D:\kaizenplus\backend\BlazorApplication\Login\Login.razor"
       
    private LoginModel loginModel = new LoginModel();
    private string errorMessage;

    private async Task HandleLogin()
    {
        errorMessage = null;

        var requestContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Username", loginModel.Username),
            new KeyValuePair<string, string>("Password", loginModel.Password)
    });
        try
        {
            var response = await Http.PostAsync("http://localhost:5000/api/Account/authenticate", requestContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginRoot>();
                var token = result.data.token;

                await localStorage.SetItemAsync("user", result);
                Console.WriteLine(token);

                 await AuthenticationStateProvider.MarkUserAsAuthenticated();
                // Redirect to the home page or another protected page
                Navigation.NavigateTo("/");
            }
            else
            {
                errorMessage = "Invalid login attempt.";
            }
        }
    catch (Exception ex)
    {
        errorMessage = $"An error occurred: {ex.Message}";
        Console.WriteLine($"Error: {ex}");
    }
    }

    private class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    private class TokenResponse
    {
        public string Token { get; set; }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private MyAuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ILocalStorageService localStorage { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager Navigation { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591
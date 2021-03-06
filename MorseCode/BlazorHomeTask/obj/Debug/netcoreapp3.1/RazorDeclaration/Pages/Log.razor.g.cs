// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace BlazorHomeTask.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Noam Keren Zvi\Desktop\HomeTask\MorseCode\BlazorHomeTask\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Noam Keren Zvi\Desktop\HomeTask\MorseCode\BlazorHomeTask\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Noam Keren Zvi\Desktop\HomeTask\MorseCode\BlazorHomeTask\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Noam Keren Zvi\Desktop\HomeTask\MorseCode\BlazorHomeTask\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Noam Keren Zvi\Desktop\HomeTask\MorseCode\BlazorHomeTask\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Noam Keren Zvi\Desktop\HomeTask\MorseCode\BlazorHomeTask\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Noam Keren Zvi\Desktop\HomeTask\MorseCode\BlazorHomeTask\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Noam Keren Zvi\Desktop\HomeTask\MorseCode\BlazorHomeTask\_Imports.razor"
using BlazorHomeTask;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Noam Keren Zvi\Desktop\HomeTask\MorseCode\BlazorHomeTask\_Imports.razor"
using BlazorHomeTask.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Noam Keren Zvi\Desktop\HomeTask\MorseCode\BlazorHomeTask\Pages\Log.razor"
using Microsoft.AspNetCore.WebUtilities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Noam Keren Zvi\Desktop\HomeTask\MorseCode\BlazorHomeTask\Pages\Log.razor"
using BlazorHomeTask.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Noam Keren Zvi\Desktop\HomeTask\MorseCode\BlazorHomeTask\Pages\Log.razor"
using BlazorHomeTask.Data.Models;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/log")]
    public partial class Log : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 32 "C:\Users\Noam Keren Zvi\Desktop\HomeTask\MorseCode\BlazorHomeTask\Pages\Log.razor"
       

    private int indexNumber = 0;
    private Message message = null;

    protected override void OnInitialized()
    {
        GetQueryInput();

        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && indexNumber != 0)
        {
            await Task.Run(() =>
            {
                message = MorsePlayer.GetMessagesFromLog(indexNumber);
            });

            StateHasChanged();
        }
    }

    private void GetQueryInput()
    {
        // Get the msg from string query
        var query = new Uri(NavigationManager.Uri).Query;

        if (QueryHelpers.ParseQuery(query).TryGetValue("n", out var value))
        {
            indexNumber = Convert.ToInt32(value);
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IMorsePlayer MorsePlayer { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IMorseCodeData MorseData { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
    }
}
#pragma warning restore 1591

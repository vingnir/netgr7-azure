using Microsoft.JSInterop;

namespace BlazorApp.JSInterop
{
    public interface IJavascriptInvoker
    {
        Task GenerateQRCodeAsync(string guid);
    }

    public class JavascriptInvoker : IJavascriptInvoker
    {
        private readonly IJSRuntime _jSRuntime;

        public JavascriptInvoker(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public async Task GenerateQRCodeAsync(string guid)
        {
            await _jSRuntime.InvokeVoidAsync("GenerateQRCode", guid);
        }
    }
}

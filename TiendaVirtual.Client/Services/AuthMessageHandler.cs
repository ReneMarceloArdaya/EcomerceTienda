using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace TiendaVirtual.Client.Services
{
    public class AuthMessageHandler : DelegatingHandler
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigation;

        public AuthMessageHandler(IJSRuntime jsRuntime, NavigationManager navigation)
        {
            _jsRuntime = jsRuntime;
            _navigation = navigation;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                // Enviar la solicitud
                var response = await base.SendAsync(request, cancellationToken);

                // Si recibimos 401 Unauthorized, el token probablemente expiró
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Token expirado o inválido, realizando logout...");

                    // Limpiar datos de usuario del localStorage
                    await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "usuario_actual");

                    // Redirigir a la página de login en el hilo principal
                    await _jsRuntime.InvokeVoidAsync("eval", $"location.href='{_navigation.BaseUri}acceso'");
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en AuthMessageHandler: {ex.Message}");
                throw;
            }
        }
    }
}
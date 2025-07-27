using Microsoft.AspNetCore.Components;
using TiendaVirtual.Client.Services;

namespace TiendaVirtual.Client.Components
{
    public class ProtectedComponentBase : ComponentBase
    {
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        protected override void OnInitialized()
        {
            // Para operaciones asíncronas en OnInitialized, usa esto:
            _ = CheckAuthAndRedirect();
        }

        private async Task CheckAuthAndRedirect()
        {
            if (!AuthService.EstaLogueado)
            {
                Navigation.NavigateTo("/acceso");
                return;
            }

            // Verificar el estado de autenticación
            var isValid = await AuthService.CheckAuthStatusAsync();
            if (!isValid)
            {
                Navigation.NavigateTo("/acceso");
            }

        }

        
    }
}
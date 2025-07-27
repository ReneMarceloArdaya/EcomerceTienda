using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.JSInterop;
using TiendaVirtual.Client.Models;

namespace TiendaVirtual.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _jsRuntime;
        private const string USER_STORAGE_KEY = "usuario_actual";
        private const string TOKEN_STORAGE_KEY = "auth_token";

        public Usuario? UsuarioActual { get; private set; }
        public string? Token { get; private set; }
        public event Action? OnAuthStateChanged;

        public AuthService(HttpClient http, IJSRuntime jsRuntime)
        {
            _http = http;
            _jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync()
        {
            try
            {
                // Recuperar token
                var storedToken = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", TOKEN_STORAGE_KEY);
                if (!string.IsNullOrEmpty(storedToken))
                {
                    Token = storedToken;
                    // Configurar el header de autorización
                    _http.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
                }

                // Recuperar usuario
                var storedUserJson = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", USER_STORAGE_KEY);
                if (!string.IsNullOrEmpty(storedUserJson))
                {
                    UsuarioActual = JsonSerializer.Deserialize<Usuario>(storedUserJson);
                    OnAuthStateChanged?.Invoke();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inicializando AuthService: {ex.Message}");
                await LogoutAsync();
            }
        }

        public async Task<bool> LoginAsync(string correo, string pass)
        {
            try
            {
                var dto = new LoginModel { Correo = correo, Contrasena = pass };
                var response = await _http.PostAsJsonAsync("api/Auth/login", dto);

                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Login fallido: {response.StatusCode}");
                    return false;
                }

                // Deserializar la respuesta correctamente
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                if (loginResponse?.Usuario != null)
                {
                    UsuarioActual = loginResponse.Usuario;
                    Token = loginResponse.Token;

                    // Guardar en localStorage
                    var userJson = JsonSerializer.Serialize(UsuarioActual);
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", USER_STORAGE_KEY, userJson);

                    if (!string.IsNullOrEmpty(Token))
                    {
                        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TOKEN_STORAGE_KEY, Token);
                        // Configurar el header de autorización
                        _http.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
                    }

                    OnAuthStateChanged?.Invoke();
                    Console.WriteLine("Login exitoso");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en LoginAsync: {ex.Message}");
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            Console.WriteLine("Realizando logout...");
            UsuarioActual = null;
            Token = null;

            try
            {
                // Limpiar localStorage
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", USER_STORAGE_KEY);
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TOKEN_STORAGE_KEY);

                // Limpiar header de autorización
                _http.DefaultRequestHeaders.Authorization = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error limpiando localStorage: {ex.Message}");
            }

            OnAuthStateChanged?.Invoke();
        }

        public async Task<bool> CheckAuthStatusAsync()
        {
            if (!EstaLogueado || string.IsNullOrEmpty(Token)) return false;

            try
            {
                // Usar un endpoint protegido para verificar el token
                var response = await _http.GetAsync("api/Usuarios"); // O cualquier endpoint protegido

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await LogoutAsync();
                    return false;
                }

                return response.IsSuccessStatusCode;
            }
            catch
            {
                // Si hay error de conexión, asumir que el usuario sigue logueado
                return true;
            }
        }

        public bool EstaLogueado => UsuarioActual != null && !string.IsNullOrEmpty(Token);
    }
}
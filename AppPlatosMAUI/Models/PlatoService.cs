using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AppPlatosMAUI.Models
{
    public class PlatoService
    {
        private const string ApiUrl = "https://localhost:7106/api/plato";  // Cambia a la dirección de tu API si es necesario
        private readonly HttpClient _httpClient;

        public PlatoService()
        {
            _httpClient = new HttpClient();
        }

        // Obtener todos los platos (GET)
        public async Task<List<Plato>> GetPlatosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Plato>>(ApiUrl);
        }

        // Crear un nuevo plato (POST)
        public async Task CreatePlatoAsync(Plato plato)
        {
            await _httpClient.PostAsJsonAsync(ApiUrl, plato);
        }

        // Actualizar un plato (PUT)
        public async Task UpdatePlatoAsync(Plato plato)
        {
            await _httpClient.PutAsJsonAsync($"{ApiUrl}/{plato.Id}", plato);
        }

        // Eliminar un plato (DELETE)
        public async Task DeletePlatoAsync(int id)
        {
            await _httpClient.DeleteAsync($"{ApiUrl}/{id}");
        }
    }
}
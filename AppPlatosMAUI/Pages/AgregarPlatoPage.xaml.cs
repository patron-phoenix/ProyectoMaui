using System.Text.Json;
using System.Text;
using AppPlatosMAUI.Models;
using System.Collections.ObjectModel;

namespace AppPlatosMAUI.Pages;

public partial class AgregarPlatoPage : ContentPage
{
    private PlatoService _platoService;
    private ObservableCollection<Plato> _platos; // Colección de platos para actualizar
    public AgregarPlatoPage(ObservableCollection<Plato> platos) // Constructor que recibe la colección
    {
        InitializeComponent();
        _platoService = new PlatoService(); // Inicializa tu servicio
        _platos = platos; // Asigna la colección de platos
    }


    // Método para guardar el plato
    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        var nuevoPlato = new Plato
        {
            Nombre = NombreEntry.Text,
            Costo = double.Parse(CostoEntry.Text),
            Ingredientes = IngredientesEntry.Text
        };

        try
        {
            // Verificar si el plato ya existe
            var platosExistentes = await _platoService.GetPlatosAsync();
            var platoExistente = platosExistentes.FirstOrDefault(p => p.Nombre.Equals(nuevoPlato.Nombre, StringComparison.OrdinalIgnoreCase));

            if (platoExistente != null)
            {
                // Si ya existe, mostrar un mensaje de error
                await DisplayAlert("Error", "Ya existe un plato con ese nombre. Por favor, elige otro nombre.", "OK");
                return; // Mantener en la misma página
            }

            // Crear un nuevo plato si no existe
            await _platoService.CreatePlatoAsync(nuevoPlato);
            _platos.Add(nuevoPlato); // Agregar el nuevo plato a la colección
            await DisplayAlert("Éxito", "Plato guardado correctamente", "OK");
            await Navigation.PopAsync(); // Regresar a la página anterior
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al guardar el plato: {ex.Message}", "OK");
        }
    }

    // Método para cancelar la acción
    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Cancelar", "¿Estás seguro de que quieres cancelar?", "Sí", "No");
        if (confirm)
        {
            await Navigation.PopAsync(); // Regresar a la página anterior
        }
    }
}
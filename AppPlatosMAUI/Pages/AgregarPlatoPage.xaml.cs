using System.Text.Json;
using System.Text;
using AppPlatosMAUI.Models;
using System.Collections.ObjectModel;

namespace AppPlatosMAUI.Pages;

public partial class AgregarPlatoPage : ContentPage
{
    private PlatoService _platoService;
    private ObservableCollection<Plato> _platos; // Colecci�n de platos para actualizar
    public AgregarPlatoPage(ObservableCollection<Plato> platos) // Constructor que recibe la colecci�n
    {
        InitializeComponent();
        _platoService = new PlatoService(); // Inicializa tu servicio
        _platos = platos; // Asigna la colecci�n de platos
    }


    // M�todo para guardar el plato
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
                return; // Mantener en la misma p�gina
            }

            // Crear un nuevo plato si no existe
            await _platoService.CreatePlatoAsync(nuevoPlato);
            _platos.Add(nuevoPlato); // Agregar el nuevo plato a la colecci�n
            await DisplayAlert("�xito", "Plato guardado correctamente", "OK");
            await Navigation.PopAsync(); // Regresar a la p�gina anterior
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al guardar el plato: {ex.Message}", "OK");
        }
    }

    // M�todo para cancelar la acci�n
    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Cancelar", "�Est�s seguro de que quieres cancelar?", "S�", "No");
        if (confirm)
        {
            await Navigation.PopAsync(); // Regresar a la p�gina anterior
        }
    }
}
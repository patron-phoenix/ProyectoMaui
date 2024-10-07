using AppPlatosMAUI.Models;
using System.Collections.ObjectModel;

namespace AppPlatosMAUI.Pages;

public partial class ActualizarPlatoPage : ContentPage
{
    private PlatoService _platoService;
    private Plato _plato; // Plato que se va a actualizar
    private ObservableCollection<Plato> _platos; // Colección de platos
    public ActualizarPlatoPage(Plato plato, ObservableCollection<Plato> platos)
	{
		InitializeComponent();
        _platoService = new PlatoService(); // Inicializa tu servicio
        _plato = plato; // Almacena el plato a actualizar
        _platos = platos; // Almacena la colección de platos

        // Mostrar datos del plato en los campos
        
        NombreEntry.Text = plato.Nombre;
        CostoEntry.Text = plato.Costo.ToString();
        IngredientesEntry.Text = plato.Ingredientes;
    }

    // Método para guardar los cambios
    private async void OnGuardarClicked(object sender, EventArgs e)
    {

        var platoActualizado = new Plato
        {
            Nombre = NombreEntry.Text,
            Costo = double.Parse(CostoEntry.Text),
            Ingredientes = IngredientesEntry.Text,
            Id = _plato.Id // Mantener el mismo ID del plato existente
        };

        try
        {
            // Actualizar el plato existente
            await _platoService.UpdatePlatoAsync(platoActualizado);

            // Mostrar mensaje de éxito
            await DisplayAlert("Éxito", "Plato actualizado correctamente", "OK");

            // Actualizar la colección de platos
            var index = _platos.IndexOf(_plato);
            if (index >= 0)
            {
                // Actualiza los valores directamente en el plato existente en la colección
                _platos[index].Nombre = platoActualizado.Nombre;
                _platos[index].Costo = platoActualizado.Costo;
                _platos[index].Ingredientes = platoActualizado.Ingredientes;
            }

            // Navegar a PlatoListPage
            await Navigation.PopToRootAsync(); // Regresa a la página raíz (PlatoListPage)
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al actualizar el plato: {ex.Message}", "OK");
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
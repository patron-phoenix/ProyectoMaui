using AppPlatosMAUI.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppPlatosMAUI.Pages;

public partial class PlatoDetailPage : ContentPage
{
    private PlatoService _platoService = new PlatoService();
    private ObservableCollection<Plato> _platos; // Colección de platos
    private Plato _plato; // Plato seleccionado
    public PlatoDetailPage(Plato plato, ObservableCollection<Plato> platos)
	{
		InitializeComponent();

        BindingContext = plato; // Establecer el contexto de datos
        _plato = plato; // Almacenar el plato actual
        _platos = platos; // Almacenar la colección de platos
    }

    // Método para manejar la acción de actualizar
    private async void OnActualizarClicked(object sender, EventArgs e)
    {
        // Navegar a la página de actualizar pasando el plato actual
        await Navigation.PushAsync(new ActualizarPlatoPage(_plato, _platos));
    }

    // Método para manejar la acción de eliminar
    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirmar", "¿Estás seguro de que quieres eliminar este plato?", "Sí", "No");
        if (confirm)
        {
            await _platoService.DeletePlatoAsync(_plato.Id); // Eliminar el plato
            _platos.Remove(_plato); // Remover el plato de la colección
            await Navigation.PopAsync(); // Regresar a la página anterior
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
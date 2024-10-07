using AppPlatosMAUI.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppPlatosMAUI.Pages;

public partial class PlatoDetailPage : ContentPage
{
    private PlatoService _platoService = new PlatoService();
    private ObservableCollection<Plato> _platos; // Colecci�n de platos
    private Plato _plato; // Plato seleccionado
    public PlatoDetailPage(Plato plato, ObservableCollection<Plato> platos)
	{
		InitializeComponent();

        BindingContext = plato; // Establecer el contexto de datos
        _plato = plato; // Almacenar el plato actual
        _platos = platos; // Almacenar la colecci�n de platos
    }

    // M�todo para manejar la acci�n de actualizar
    private async void OnActualizarClicked(object sender, EventArgs e)
    {
        // Navegar a la p�gina de actualizar pasando el plato actual
        await Navigation.PushAsync(new ActualizarPlatoPage(_plato, _platos));
    }

    // M�todo para manejar la acci�n de eliminar
    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirmar", "�Est�s seguro de que quieres eliminar este plato?", "S�", "No");
        if (confirm)
        {
            await _platoService.DeletePlatoAsync(_plato.Id); // Eliminar el plato
            _platos.Remove(_plato); // Remover el plato de la colecci�n
            await Navigation.PopAsync(); // Regresar a la p�gina anterior
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
using AppPlatosMAUI.Models;
using System.Collections.ObjectModel;

namespace AppPlatosMAUI.Pages;

public partial class PlatoListPage : ContentPage
{
    private PlatoService _platoService = new PlatoService();
    public ObservableCollection<Plato> Platos { get; set; } = new ObservableCollection<Plato>();

    public PlatoListPage()
    {
        InitializeComponent();
        BindingContext = this;
        LoadPlatos();
    }

    // Cargar platos desde el servidor
    private async void LoadPlatos()
    {

        try
        {
            var platos = await _platoService.GetPlatosAsync();

            // Barajar la lista de platos de forma aleatoria
            var random = new Random();
            var platosAleatorios = platos.OrderBy(x => random.Next()).ToList();

            Platos.Clear(); // Limpiar la colección existente
            foreach (var plato in platosAleatorios)
            {
                Platos.Add(plato);  // Agregar los platos en orden aleatorio a la colección observable
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al cargar los platos: {ex.Message}", "OK");
        }
    }

    // Navegar a la página de agregar plato
    public async void OnAgregarPlatoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AgregarPlatoPage(Platos));
    }

    // Manejar el evento de clic en el nombre del plato
    private async void OnPlatoTapped(object sender, EventArgs e)
    {
        var label = (Label)sender;
        var plato = (Plato)label.BindingContext;


        // Asegúrate de que el objeto plato no sea nulo
        if (plato != null)
        {
            // Navegar a la página de detalles del plato
            await Navigation.PushAsync(new PlatoDetailPage(plato,Platos));
        }
        else
        {
            await DisplayAlert("Error", "El plato no se encontró.", "OK");
        }
    }
}
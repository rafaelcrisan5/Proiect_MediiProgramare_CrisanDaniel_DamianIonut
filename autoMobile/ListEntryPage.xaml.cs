namespace autoMobile;

public partial class ListEntryPage : ContentPage
{
    public ListEntryPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    async void OnSearchClicked(object sender, EventArgs e)
    {
        string plate = plateEntry.Text;
        if (!string.IsNullOrWhiteSpace(plate))
        {
            try
            {
                var rezultate = await App.Database.GetProgramariByPlateAsync(plate);
                listView.ItemsSource = rezultate;

                if (rezultate == null || rezultate.Count == 0)
                {
                    await DisplayAlert("Info", "Nu s-au gasit programari pentru acest numar.", "OK");
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Eroare Critica", ex.Message, "OK");
            }
        }
    }

    async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AppointmentPage
        {
            BindingContext = new Models.Programare
            {
                data = DateTime.Now,
                status = "In Asteptare"
            }
        });
    }
    async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
           
            await Navigation.PushAsync(new ListDetailsPage
            {
                BindingContext = e.SelectedItem as Models.Programare
            });
        }
    }
}
namespace autoMobile;

public partial class AppointmentPage : ContentPage
{
    public AppointmentPage()
    {
        InitializeComponent();
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        var programare = (Models.Programare)BindingContext;
        if (programare == null) return;

        try
        {
            
            bool esteNoua = (programare.id == 0);

            await App.Database.SaveProgramareAsync(programare, esteNoua);

            await DisplayAlert("Succes", esteNoua ? "Programare creata!" : "Modificari salvate!", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Eroare", "Eroare la salvare: " + ex.Message, "OK");
        }
    }
}
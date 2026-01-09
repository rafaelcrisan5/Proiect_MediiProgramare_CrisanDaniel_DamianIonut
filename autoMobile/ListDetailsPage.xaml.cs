namespace autoMobile;

public partial class ListDetailsPage : ContentPage
{
    public ListDetailsPage()
    {
        InitializeComponent();
    }

    async void OnEditClicked(object sender, EventArgs e)
    {
        
        var programareCurenta = (Models.Programare)BindingContext;

       
        await Navigation.PushAsync(new AppointmentPage
        {
            BindingContext = programareCurenta
        });
    }


    async void OnDeleteClicked(object sender, EventArgs e)
    {
        
        var programare = (Models.Programare)BindingContext;

        if (programare == null) return;

        bool confirmare = await DisplayAlert("Confirmare",
            $"Sigur doresti sa stergi programarea pentru {programare.numarInmatriculare}?", "Da", "Nu");

        if (confirmare)
        {
            try
            {
               
                await App.Database.DeleteProgramareAsync(programare.id);

                await DisplayAlert("Succes", "Programarea a fost stearsa.", "OK");

                
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", "Nu s-a putut sterge: " + ex.Message, "OK");
            }
        }
    }
}
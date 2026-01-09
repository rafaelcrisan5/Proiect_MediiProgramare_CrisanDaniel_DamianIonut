namespace autoMobile;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    async void OnLoginClicked(object sender, EventArgs e)
    {
        
        if (string.IsNullOrEmpty(emailEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text))
        {
            await DisplayAlert("Atentie", "Te rugam sa introduci email-ul si parola.", "OK");
            return;
        }

       
        loadingIndicator.IsRunning = true;

        try
        {
            
            bool isSuccess = await App.Database.LoginAsync(emailEntry.Text, passwordEntry.Text);

            if (isSuccess)
            {
                
                Application.Current.MainPage = new NavigationPage(new ListEntryPage());
            }
            else
            {
                await DisplayAlert("Eroare", "Email sau parola incorecta.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Eroare de retea", "Nu s-a putut contacta serverul: " + ex.Message, "OK");
        }
        finally
        {
           
            loadingIndicator.IsRunning = false;
        }
    }
}
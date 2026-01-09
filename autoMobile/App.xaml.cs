namespace autoMobile;

public partial class App : Application
{
    public static RestService Database { get; private set; }

    public App()
    {
        InitializeComponent();
        Database = new RestService();
        MainPage = new LoginPage();
    }
}

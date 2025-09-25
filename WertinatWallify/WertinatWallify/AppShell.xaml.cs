using WertinatWallify.Views;

namespace WertinatWallify;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // DetailPage için rota (navigation) kaydını yapıyoruz.
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
    }
}
using WertinatWallify.ViewModels;

namespace WertinatWallify.Views;

public partial class DetailPage : ContentPage
{
    public DetailPage(DetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.SetBinding(TitleProperty, new Binding("SelectedWallpaper.Name"));
    }
    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
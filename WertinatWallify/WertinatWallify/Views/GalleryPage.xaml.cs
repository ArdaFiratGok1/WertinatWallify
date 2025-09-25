using WertinatWallify.ViewModels;

namespace WertinatWallify.Views;

public partial class GalleryPage : ContentPage
{
    public GalleryPage(GalleryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
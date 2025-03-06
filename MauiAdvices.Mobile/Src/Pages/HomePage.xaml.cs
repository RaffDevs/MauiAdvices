using MauiAdvices.Mobile.Pages;

namespace MauiAdvices.Mobile.Src.Pages;

public partial class HomePage : ContentPage
{
    private readonly MainViewModel _viewModel;
    public HomePage(MainViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.ExecuteGetRandomAdviceCommand.Execute(null);
    }
}
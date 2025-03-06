namespace MauiAdvices.Mobile.Pages;

public partial class FavoritesPage : ContentPage
{
    private readonly MainViewModel _viewModel;
    
    
    public FavoritesPage(MainViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.ExecuteGetFavoriteAdvicesCommand.Execute(null);
    }

    private async void HandleDeleteAdvice(object? sender, EventArgs e)
    {
        if (sender is ImageButton imageButton)
        {
            var paramter = imageButton.CommandParameter;

            if (paramter is int id)
            {
                HandleConfirmDelete(() =>
                {
                    _viewModel.ExecuteDeleteAdviceCommand.Execute(id);
                });
            }
        }

        
    }

    private async void HandleConfirmDelete(Action callback)
    {
        var result = await this.DisplayAlert("Delete", "Are you sure?", "Yes", "No");
                
        if (result)
        {
            callback();
        }
        else
        {
            Console.WriteLine("Nope");
        }
    }
}
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MauiAdvices.Interactors.Models;
using MauiAdvices.Interactors.Usecases;

namespace MauiAdvices.Mobile.Pages;

public class MainViewModel : INotifyPropertyChanged
{
    
    
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly AdviceUsecase _adviceUsecase;
    
    public MainViewModel() {}
    
    public MainViewModel(AdviceUsecase adviceUsecase)
    {
        _adviceUsecase = adviceUsecase;
        ExecuteGetRandomAdviceCommand = new Command(async () => await GetRandomAdvice(), () => !IsProcessing);
        ExecuteSaveAdviceCommand = new Command(async () => await SaveAdvice(), () => !IsProcessing);
        ExecuteGetFavoriteAdvicesCommand = new Command(async () => await GetFavoriteAdvices(), () => !IsProcessing);
        ExecuteDeleteAdviceCommand = new Command<int>(async (id) => await DeleteAdvice(id), (_) => !IsProcessing);
    }
    

    #region properties

    private bool _isProcessing;

    public bool IsProcessing
    {
        get => _isProcessing;
        set
        {
            _isProcessing = value;
            OnPropertyChanged(nameof(IsProcessing));
            ((Command)ExecuteGetRandomAdviceCommand).ChangeCanExecute();
        }
    }
    

    private RandomAdviceDTO _currentRandomAdvice = new();
    public RandomAdviceDTO CurrentRandomAdvice
    {
        get => _currentRandomAdvice;
        set
        {
            _currentRandomAdvice = value;
            OnPropertyChanged(nameof(CurrentRandomAdvice));
        }
    }

    private ObservableCollection<AdviceDTO> _favoriteAdvices = [];

    public ObservableCollection<AdviceDTO> FavoriteAdvices
    {
        get => _favoriteAdvices;
        set
        {
            _favoriteAdvices = value;
            OnPropertyChanged(nameof(FavoriteAdvices));
        }
    }
    
    #endregion

    #region commands

    public ICommand ExecuteGetRandomAdviceCommand { get; private set; }
    public ICommand ExecuteSaveAdviceCommand { get; private set; }
    public ICommand ExecuteGetFavoriteAdvicesCommand { get; private set; }
    public ICommand ExecuteDeleteAdviceCommand { get; private set; }
    
    #endregion

    #region methods

    private async Task GetRandomAdvice()
    {
        ToggleIsProcessing();
        var advice = await _adviceUsecase.GetRandomAdvice();
        CurrentRandomAdvice = advice;
        ToggleIsProcessing();
    }

    private async Task SaveAdvice()
    {
        ToggleIsProcessing();
        await _adviceUsecase.SaveAdvice(CurrentRandomAdvice);
        await GetRandomAdvice();
        ToggleIsProcessing();
    }

    private async Task GetFavoriteAdvices()
    {
        ToggleIsProcessing();
        var advices = await _adviceUsecase.GetAllAdvices();
        FavoriteAdvices = new ObservableCollection<AdviceDTO>(advices);
        ToggleIsProcessing();
    }

    private async Task DeleteAdvice(int id)
    {
        ToggleIsProcessing();
        await _adviceUsecase.DeleteAdvice(id);
        OnPropertyChanged(nameof(FavoriteAdvices));
        await GetFavoriteAdvices();
        ToggleIsProcessing();
    }

    private void ToggleIsProcessing()
    {
        IsProcessing = !IsProcessing;
    }

    #endregion
    
    #region Others

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    #endregion

    
}
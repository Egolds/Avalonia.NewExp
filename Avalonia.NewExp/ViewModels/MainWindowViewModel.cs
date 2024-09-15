using Avalonia.NewExp.Core.Abstractions;
using Avalonia.NewExp.Core.Services;
using Avalonia.NewExp.ViewModels.Dialogs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace Avalonia.NewExp.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, ICloseable
    {
        #region Fields

        private readonly INavigationService navigationService;
        private readonly IWindowManager windowManager;
        private readonly IViewModelLocator viewModelFactory;

        #endregion

        #region Props

        [ObservableProperty] private bool allowCloseWindow = true;
        [ObservableProperty] private string dialogResultText;
        public string HelloMessage => "Welcome!";
        public ObservableObject CurrentViewModel => navigationService.CurrentViewModel;

        #endregion

        #region IWindowCloseable

        public event EventHandler? Close;
        public bool CanClose() => AllowCloseWindow;

        #endregion

        public MainWindowViewModel() { }
        public MainWindowViewModel(INavigationService navigationService, IWindowManager windowManager, IViewModelLocator viewModelFactory)
        {
            this.navigationService = navigationService;
            this.windowManager = windowManager;
            this.viewModelFactory = viewModelFactory;

            navigationService.CurrentViewChanged += (s, e) => OnPropertyChanged(nameof(CurrentViewModel));
            navigationService.NavigateTo<HomeViewModel>(this);
        }

        [RelayCommand]
        private async Task OpenNewWindow(object obj)
        {
            var modal = bool.TryParse(obj.ToString(), out var arg) ? arg : false;

            var dialogViewModel = viewModelFactory.Locate<TestModalWindowViewModel>(this);

            if (!modal)
            {
                windowManager.Show(this, dialogViewModel);
            }
            else
            {
                await windowManager.ShowDialog(this, dialogViewModel);
                DialogResultText = $"Result: {dialogViewModel.DialogResult}";
            }
        }

        [RelayCommand(CanExecute = nameof(CanCloseMe))]
        private void CloseMe()
        {
            Close?.Invoke(this, EventArgs.Empty);
        }

        [RelayCommand] private void GoHomeView() => navigationService.NavigateTo<HomeViewModel>(this);
        [RelayCommand] private void GoSecondView() => navigationService.NavigateTo<SecondViewModel>(this);

        private bool CanCloseMe()
        {
            // Тут можно реализовать логику доступности кнопки
            return true;
        }
    }
}

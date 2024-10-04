using Avalonia.NewExp.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Avalonia.NewExp.ViewModels
{
    public partial class HomeViewModel : ViewModelBase
    {
        private IViewModelLocator viewModelFactory;
        public string Greeting => "Welcome to Home view!";

        public ObservableCollection<ActionTabViewModel> Tabs { get; } = new();
        [ObservableProperty] private bool isTargetBtnFocused;

        public HomeViewModel() { }
        public HomeViewModel(IViewModelLocator viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
            Tabs = new ObservableCollection<ActionTabViewModel>()
            {
                new ActionTabViewModel(),
                new ActionTabViewModel(){ Title = "Поиск", Content =  viewModelFactory.Locate<SecondViewModel>(this) }
            };
        }

        [RelayCommand]
        private void LostFocus()
        {
            IsTargetBtnFocused = false;
            IsTargetBtnFocused = true;
        }
    }
}

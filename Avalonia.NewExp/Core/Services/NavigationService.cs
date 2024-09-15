using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Avalonia.NewExp.Core.Services
{
    public interface INavigationService
    {
        ObservableObject CurrentViewModel { get; }
        event EventHandler CurrentViewChanged;
        void NavigateTo<TViewModel>(ObservableObject? ownerWindowViewModel = null) where TViewModel : ObservableObject;
    }

    public class NavigationService : INavigationService
    {
        private readonly IViewModelLocator viewModelFactory;
        private ObservableObject? currentView;

        public ObservableObject CurrentViewModel
        {
            get => currentView!;
            private set
            {
                currentView = value;
                CurrentViewChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler? CurrentViewChanged;

        public NavigationService(IViewModelLocator viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>(ObservableObject? ownerWindowViewModel = null) where TViewModel : ObservableObject
        {
            CurrentViewModel = viewModelFactory.Locate<TViewModel>(ownerWindowViewModel);
        }
    }
}

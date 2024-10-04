using Avalonia.NewExp.Core.Abstractions;
using Avalonia.NewExp.Core.Services;
using Avalonia.NewExp.ViewModels.Dialogs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Avalonia.NewExp.ViewModels
{
    public partial class SecondViewModel : ViewModelBase, IOwnerable
    {
        private readonly IWindowManager windowManager;
        private readonly IViewModelLocator viewModelLocator;

        public ObservableObject OwnerWindowViewModel { get; set; }
        public string Greeting => "Welcome to Second view!";
        public ObservableCollection<string> TestItems { get; }

        public SecondViewModel() { }
        public SecondViewModel(IWindowManager windowManager, IViewModelLocator viewModelLocator)
        {
            this.windowManager = windowManager;
            this.viewModelLocator = viewModelLocator;

            TestItems = new ObservableCollection<string>();
            for (int i = 0; i < 500; i++)
            {
                TestItems.Add("Item " + i);
            }
        }

        [RelayCommand]
        private void OpenNewWindow()
        {
            var vm = viewModelLocator.Locate<TestModalWindowViewModel>(OwnerWindowViewModel);
            windowManager.Show(OwnerWindowViewModel, vm);
        }
    }
}

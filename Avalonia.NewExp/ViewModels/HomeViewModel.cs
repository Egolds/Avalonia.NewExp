using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Avalonia.NewExp.ViewModels
{
    public partial class HomeViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Home view!";

        [ObservableProperty] private bool isTargetBtnFocused;

        [RelayCommand]
        private void LostFocus()
        {
            IsTargetBtnFocused = false;
            IsTargetBtnFocused = true;
        }
    }
}

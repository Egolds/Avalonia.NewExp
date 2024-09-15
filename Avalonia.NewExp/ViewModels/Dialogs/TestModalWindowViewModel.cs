using Avalonia.NewExp.Core.Abstractions;
using CommunityToolkit.Mvvm.Input;
using System;

namespace Avalonia.NewExp.ViewModels.Dialogs
{
    public partial class TestModalWindowViewModel : ViewModelBase, ICloseable, IModalDialog<bool>
    {
        public bool DialogResult { get; private set; }

        public event EventHandler? Close;
        public bool CanClose() => true;

        [RelayCommand]
        private void Yes()
        {
            DialogResult = true;
            Close?.Invoke(this, EventArgs.Empty);
        }

        [RelayCommand]
        private void No()
        {
            DialogResult = false;
            Close?.Invoke(this, EventArgs.Empty);
        }
    }
}

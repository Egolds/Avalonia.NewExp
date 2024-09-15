using System.ComponentModel;

namespace Avalonia.NewExp.Core.Abstractions
{
    public interface IModalDialog : INotifyPropertyChanged
    {
        bool? DialogResult { get; }
    }

    public interface IModalDialog<TDialogResult> : INotifyPropertyChanged
    {
        TDialogResult? DialogResult { get; }
    }
}

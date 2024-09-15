using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace Avalonia.NewExp.Core.Abstractions
{
    public interface IOwnerable : INotifyPropertyChanged
    {
        ObservableObject OwnerWindowViewModel { get; set; }
    }
}

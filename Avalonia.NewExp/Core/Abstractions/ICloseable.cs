using System;
using System.ComponentModel;

namespace Avalonia.NewExp.Core.Abstractions
{
    public interface ICloseable : INotifyPropertyChanged
    {
        event EventHandler? Close;
        bool CanClose();
    }
}

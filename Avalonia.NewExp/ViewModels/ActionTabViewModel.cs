using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.NewExp.ViewModels
{
    public partial class ActionTabViewModel : ViewModelBase
    {
        [ObservableProperty] private string title = "Новая вкладка";
        [ObservableProperty] private ObservableObject? content;

        [RelayCommand] private void Close()
        {

        }
    }
}

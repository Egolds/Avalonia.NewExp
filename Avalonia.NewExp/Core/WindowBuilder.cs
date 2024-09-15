using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Avalonia.NewExp.Core
{
    internal static class WindowBuilder
    {
        public static Window CreateWindow(ObservableObject viewModel)
        {
            if (viewModel is null)
                return NotFoundWindow("NULL");

            var name = viewModel.GetType().FullName!
                .Replace("WindowViewModel", "Window", StringComparison.Ordinal)
                .Replace("ViewModel", "View", StringComparison.Ordinal);
            var type = Type.GetType(name);

            if (type != null)
            {
                var window = (Window)Activator.CreateInstance(type)!;
                window.DataContext = viewModel;
                return window;
            }

            return NotFoundWindow(name);
        }

        private static Window NotFoundWindow(string objectName) => new Window { Content = "Not Found: " + objectName };
    }
}

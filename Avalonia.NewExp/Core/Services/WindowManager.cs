using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.NewExp.Core.Abstractions;
using Avalonia.NewExp.Core.Behaviors;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avalonia.NewExp.Core.Services
{
    public class HandleWindowEventArgs : EventArgs
    {
        public Window Window { get; }
        public HandleWindowEventArgs(Window window) => Window = window;
    }

    public interface IWindowManager
    {
        event Action<HandleWindowEventArgs> CustomHandleWindow;

        Window FindWindowByViewModel(ObservableObject viewModel);

        void Show<TOwnerViewModel, TViewModel>(TOwnerViewModel owner, TViewModel viewModel, Action? callback = null)
            where TOwnerViewModel : ObservableObject
            where TViewModel : ObservableObject;
        Task ShowDialog<TOwnerViewModel, TViewModel>(TOwnerViewModel owner, TViewModel viewModel, Action? callback = null)
            where TOwnerViewModel : ObservableObject
            where TViewModel : ObservableObject;
    }

    public sealed class WindowManager : IWindowManager
    {
        private static IEnumerable<Window> Windows => (Application.Current!.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)!.Windows;

        public event Action<HandleWindowEventArgs>? CustomHandleWindow;

        public Window FindWindowByViewModel(ObservableObject viewModel) =>
           Windows.FirstOrDefault(x => ReferenceEquals(viewModel, x.DataContext))!;

        public void Show<TOwnerViewModel, TViewModel>(TOwnerViewModel owner, TViewModel viewModel, Action? callback = null)
            where TViewModel : ObservableObject
            where TOwnerViewModel : ObservableObject
        {
            var ownerWindow = LocateOwnerWindow(owner);
            var window = WindowBuilder.CreateWindow(viewModel);
            HandleWindowCloseCallback(window, callback!);
            HandleWindow(window);
            window.Show(ownerWindow);
        }

        public async Task ShowDialog<TOwnerViewModel, TViewModel>(TOwnerViewModel owner, TViewModel viewModel, Action? callback = null)
            where TViewModel : ObservableObject
            where TOwnerViewModel : ObservableObject
        {
            var ownerWindow = LocateOwnerWindow(owner);
            var window = WindowBuilder.CreateWindow(viewModel);
            HandleWindowCloseCallback(window, callback!);
            HandleWindow(window);
            await window.ShowDialog(ownerWindow);
        }

        private void HandleWindow(Window window)
        {
            if (window.DataContext != null)
            {
                if (window.DataContext is ICloseable vm)
                {
                    bool enabledWindowClosing = (bool)window.GetValue(WindowBehaviors.EnableWindowClosingProperty);
                    if (!enabledWindowClosing)
                    {
                        vm.Close += (s, e) => window.Close();
                        window.Closing += (s, e) => e.Cancel = !vm.CanClose();
                    }
                }

                // Тут можно добавить обработку интерфейсов например
                // ...

                CustomHandleWindow?.Invoke(new HandleWindowEventArgs(window));
            }
        }

        private void HandleWindowCloseCallback(Window window, Action callback)
        {
            if (window == null || callback == null)
                return;

            EventHandler? closeEvent = null;
            closeEvent = (s, e) =>
            {
                callback();
                window.Closed -= closeEvent;
            };
            window.Closed += closeEvent;
        }

        private Window LocateOwnerWindow<TOwnerViewModel>(TOwnerViewModel owner) where TOwnerViewModel : ObservableObject
        {
            var ownerWindow = FindWindowByViewModel(owner);
            if (ownerWindow == null)
                throw new NullReferenceException("Owner window not found");

            return ownerWindow;
        }
    }
}

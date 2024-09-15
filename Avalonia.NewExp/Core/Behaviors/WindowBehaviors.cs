using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.NewExp.Core.Abstractions;

namespace Avalonia.NewExp.Core.Behaviors
{
    public class WindowBehaviors : AvaloniaObject
    {
        static WindowBehaviors()
        {
            EnableWindowClosingProperty.Changed.AddClassHandler<Interactive>(HandleEnableWindowClosingChanged);
        }

        #region EnableWindowClosing

        // Это свойство можно использовать в любом Window, чтобы оно работало ViewModel должен реализовать интерфейс ICloseable

        /// <summary>
        /// EnableWindowClosing AttachedProperty definition
        /// indicates ....
        /// </summary>
        public static readonly AttachedProperty<bool> EnableWindowClosingProperty =
            AvaloniaProperty.RegisterAttached<WindowBehaviors, StyledElement, bool>("EnableWindowClosing", false);

        /// <summary>
        /// Accessor for Attached property <see cref="EnableWindowClosingProperty"/>.
        /// </summary>
        /// <param name="element">Target element</param>
        /// <param name="value">The value to set <see cref="EnableWindowClosingProperty"/>.</param>
        public static void SetEnableWindowClosing(StyledElement element, bool value) =>
            element.SetValue(EnableWindowClosingProperty, value);

        /// <summary>
        /// Accessor for Attached property <see cref="EnableWindowClosingProperty"/>.
        /// </summary>
        /// <param name="element">Target element</param>
        public static bool GetEnableWindowClosing(StyledElement element) =>
            element.GetValue(EnableWindowClosingProperty);

        private static void HandleEnableWindowClosingChanged(Interactive interactive, AvaloniaPropertyChangedEventArgs args)
        {
            //if (interactive is Window window && args.GetNewValue<bool>())
            //{
            //    interactive.AddHandler(Window.LoadedEvent, (s, e) =>
            //    {
            //        if (window.DataContext is ICloseable vm)
            //        {
            //            vm.Close += (sender, _e) => window.Close();
            //            window.Closing += (sender, _e) => _e.Cancel = !vm.CanClose();
            //        }
            //    });
            //}

            if (interactive is Window window && args.GetNewValue<bool>())
            {
                window.Loaded += (s, re) =>
                {
                    if (window.DataContext is ICloseable vm)
                    {
                        vm.Close += (sender, _e) => window.Close();
                        window.Closing += (sender, _e) => _e.Cancel = !vm.CanClose();
                    }
                };
            }
        }

        #endregion
    }
}

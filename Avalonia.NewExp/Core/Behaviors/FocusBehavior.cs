﻿using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia.NewExp.Core.Behaviors
{
    public static class FocusBehavior
    {
        static FocusBehavior()
        {
            IsFocusedProperty.Changed.AddClassHandler<Interactive>(HandleIsFocusedChanged);
        }

        public static readonly AttachedProperty<bool> IsFocusedProperty =
            AvaloniaProperty.RegisterAttached<StyledElement, bool>("IsFocused", typeof(FocusBehavior), false);

        public static void SetIsFocused(StyledElement element, bool value) =>
            element.SetValue(IsFocusedProperty, value);

        public static bool GetIsFocused(StyledElement element) =>
            element.GetValue(IsFocusedProperty);

        private static void HandleIsFocusedChanged(Interactive interactive, AvaloniaPropertyChangedEventArgs args)
        {
            if (interactive is Control control && (bool)args.NewValue!)
            {
                if (control.Focusable && !control.IsFocused)
                {
                    control.Focus();
                }
            }
        }
    }
}

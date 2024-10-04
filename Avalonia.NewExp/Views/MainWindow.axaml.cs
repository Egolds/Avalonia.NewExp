using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Rendering;

namespace Avalonia.NewExp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RendererDiagnostics.DebugOverlays = RendererDebugOverlays.Fps;
        }

        private void btnTestOverlay_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
        }
    }
}
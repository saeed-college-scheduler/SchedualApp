using System.Windows;
using System.Windows.Forms.Integration;

namespace SchedualApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var coursControl = new CoursManagementControl();
            winFormsHost.Child = coursControl;
        }
    }
}

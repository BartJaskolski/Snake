using System.Windows;
using SnakeMVVM.ViewModels;

namespace SnakeMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new SnakeViewModel();
        }
    }
}

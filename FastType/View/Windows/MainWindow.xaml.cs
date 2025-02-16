using FastType.View.Pages;
using System.Windows;

namespace FastType
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Navigate(new AuthorizationPage());
        }

        private void TypingTutorBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TypingTutorPage());
        }
    }
}

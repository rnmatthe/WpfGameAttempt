using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfGameAttempt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, UsernameTextBox);
        }

        public void login_click(object sender, RoutedEventArgs e)
        {
            if (verifyLogin())
            {
                MainMenu nextPage = new MainMenu();
                nextPage.Show();
                this.Close();
            }
        }

        public void new_user_click(object sender, RoutedEventArgs e)
        {

        }

        public bool verifyLogin()
        {
            string username = UsernameTextBox.Text;
            string password = MyPasswordBox.Password;
            return true;
        }
    }
}

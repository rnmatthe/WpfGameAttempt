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
using System.Windows.Shapes;

namespace WpfGameAttempt
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {

        private MainMenuHandler myHandler;

        public MainMenu()
        {
            InitializeComponent();
            myHandler = new MainMenuHandler();
            FocusManager.SetFocusedElement(this, InputTextBox);
        }

        public void key_down(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                myHandler.acceptInput(InputTextBox.Text);
                updateText();
            }
        }

        private void updateText()
        {
            MyTextBlock.Text = myHandler.getText();
            InputTextBox.Text = "";
        }
    }
}

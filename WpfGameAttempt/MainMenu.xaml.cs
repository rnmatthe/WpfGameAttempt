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
using System.Media;

namespace WpfGameAttempt
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {

        private MainMenuHandler myHandler;
        private MediaPlayer player;
        //private string musicPath = "C:\\Users\\Rachel\\Music\\Lovers_Song.mp3";
        private string musicPath = "Lovers_Song.mp3";

        public MainMenu()
        {
            InitializeComponent();
            myHandler = new MainMenuHandler();
            FocusManager.SetFocusedElement(this, InputTextBox);
            //SoundPlayer player = new SoundPlayer("C:\\Users\\Rachel\\Music\\Lovers_Song.mp3");
            //player.PlayLooping();

            player = new MediaPlayer();
            player.Open(new Uri(musicPath, UriKind.Relative));
            player.MediaEnded += new EventHandler(Media_Ended);
            player.Play();
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

        private void Media_Ended(object sender, EventArgs e)
        {
            player.Open(new Uri(musicPath, UriKind.Relative));
            player.Play();
        }
    }
}

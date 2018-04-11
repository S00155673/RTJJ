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
using WebAPIAuthenticationClient;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //public MainViewModel mvm { get; set; }
        public MainWindow()
        {
            PlayerAuthentication.baseWebAddress = "http://ppapigameserver.azurewebsites.net/";
            InitializeComponent();
            //mvm = new MainViewModel();
            //this.DataContext = mvm;
            //this.listBox.ItemsSource = mvm.list.Select(e => e.GamerTag + " " + e.score.ToString()).ToList();

        }


    }

    public class MainViewModel
    {
        public PlayerProfile currentPlayer { get; set; }
        public List<GameScoreObject> list { get; set; }
        public MainViewModel()
        {
            PlayerAuthentication.baseWebAddress = "http://ppapigameserver.azurewebsites.net/";// "http://localhost:50574/";

            if (PlayerAuthentication.login( "powell.paul@itsligo.ie", "itsPaul$1"))
            {
                currentPlayer = PlayerAuthentication.getPlayerProfile();
                
            }
            list = PlayerAuthentication.getScores(4, "Battle Call");


        }
    }
}

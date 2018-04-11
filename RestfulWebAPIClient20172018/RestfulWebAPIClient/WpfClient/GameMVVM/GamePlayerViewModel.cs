using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIAuthenticationClient;

namespace WpfClient.GameMVVM
{
    public class GamePlayerViewModel: INotifyPropertyChanged
    {
        
        public ObservableCollection<GameScoreObject> ScoreList { get; set; }

        public GamePlayerViewModel()
        {
            if(DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;
            PlayerAuthentication.baseWebAddress = "http://ppapigameserver.azurewebsites.net/";
            ////"http://localhost:50574/";
            ////http://ppapigameserver.azurewebsites.net/

            ScoreList = new ObservableCollection<GameScoreObject>( PlayerAuthentication.getScores(4, "Battle Call"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

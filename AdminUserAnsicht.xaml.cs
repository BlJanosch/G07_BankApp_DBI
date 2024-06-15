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

namespace BankingSystem
{
    /// <summary>
    /// Interaction logic for AdminUserAnsicht.xaml
    /// </summary>
    public partial class AdminUserAnsicht : Window
    {
        List<AdminEintrag> adminEintragList = AdminEintrag.GetAdminEinträge();
        public int Index;
        public AdminUserAnsicht(int index)
        {
            InitializeComponent();
            this.Index = index;
        }

        private void StatistikButton_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = User.GetUsers();
            foreach (User user in users)
            {
                if (user.Name == adminEintragList[Index].Name)
                {
                    StatisticWindow statisticWindow = new StatisticWindow(user);
                    statisticWindow.GetGridElements();
                    statisticWindow.ShowDialog();
                    break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NameLabel.Content = adminEintragList[Index].Name;
            StandortLabel.Content = adminEintragList[Index].Standort;
            KontostandLabel.Content = $"{adminEintragList[Index].Kontostand}€";
            DatumLabel.Content = adminEintragList[Index].Datum;
            BetragLabel.Content = $"{adminEintragList[Index].Betrag}€";
            BeschreibungLabel.Content = adminEintragList[Index].Beschreibung;

            StatistikButton.Content = $"Alle Ausgaben von {adminEintragList[Index].Name} anzeigen";
        }
    }
}

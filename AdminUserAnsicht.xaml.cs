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
            FixNameLabel.Style = Styles.GetFontStyle(20);
            FixStandortLabel.Style = Styles.GetFontStyle(20);
            FixKontostandLabel.Style = Styles.GetFontStyle(20);
            FixDatumLabel.Style = Styles.GetFontStyle(20);
            FixBetragLabel.Style = Styles.GetFontStyle(20);
            FixBeschreibungLabel.Style = Styles.GetFontStyle(20);
            NameLabel.Content = adminEintragList[Index].Name;
            NameLabel.Style = Styles.GetFontStyle(20);
            StandortLabel.Content = adminEintragList[Index].Standort;
            StandortLabel.Style = Styles.GetFontStyle(20);
            KontostandLabel.Content = $"{adminEintragList[Index].Kontostand}€";
            KontostandLabel.Style = Styles.GetFontStyle(20);
            DatumLabel.Content = adminEintragList[Index].Datum;
            DatumLabel.Style = Styles.GetFontStyle(20);
            BetragLabel.Content = $"{adminEintragList[Index].Betrag}€";
            BetragLabel.Style = Styles.GetFontStyle(20);
            BeschreibungLabel.Content = adminEintragList[Index].Beschreibung;
            BeschreibungLabel.Style = Styles.GetFontStyle(20);

            StatistikButton.Content = $"Alle Ausgaben von {adminEintragList[Index].Name} anzeigen";
        }
    }
}

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
    /// Interaction logic for StatisticWindow.xaml
    /// </summary>
    public partial class StatisticWindow : Window
    {
        public User user;
        public int MaxHeigt = 250;
        public StatisticWindow(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        public void DrawStatistic()
        {
            double Ausgaben = 0;
            double Einkünfte = 0;
            List<Eintrag> einträge = Eintrag.GetEinträge(user);
            foreach (Eintrag eintrag in einträge)
            {
                if (eintrag.Betrag < 0)
                {
                    Ausgaben += eintrag.Betrag;
                }
                else
                {
                    Einkünfte += eintrag.Betrag;
                }
            }

            if (Einkünfte > Ausgaben)
            {
                Wert1Rect.Height = (Ausgaben * (-1) / Einkünfte) * MaxHeigt;
                Wert2Rect.Height = MaxHeigt;
            }
            else
            {
                Wert2Rect.Height = (Einkünfte / Ausgaben * (-1)) * MaxHeigt;
                Wert1Rect.Height = MaxHeigt;
            }
        }
    }
}

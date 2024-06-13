using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
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
        protected List<double> Ausgaben;
        protected List<double> Einkünfte;
        protected double AusgabenGesamt;
        protected double EinkünfteGesamt;
        protected List<Eintrag> einträge;
        public StatisticWindow(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        public void GetGridElements()
        {
            AusgabenGesamt = 0;
            EinkünfteGesamt = 0;
            Ausgaben = new List<double>();
            Einkünfte = new List<double>();
            einträge = Eintrag.GetEinträge(user);
            foreach (Eintrag eintrag in einträge)
            {
                if (eintrag.Betrag < 0)
                {
                    Ausgaben.Add(eintrag.Betrag);
                    AusgabenGesamt += eintrag.Betrag;
                }
                else
                {
                    Einkünfte.Add(eintrag.Betrag);
                    EinkünfteGesamt += eintrag.Betrag;
                }
            }
            MakeRows();
        }

        public void MakeRows()
        {
            if (EinkünfteGesamt > AusgabenGesamt)
            {
                AusgabenGrid.Height = (AusgabenGesamt * (-1) / EinkünfteGesamt) * MaxHeigt;
                EinkünfteGrid.Height = MaxHeigt;
            }
            else
            {
                EinkünfteGrid.Height = (EinkünfteGesamt / AusgabenGesamt * (-1)) * MaxHeigt;
                AusgabenGrid.Height = MaxHeigt;
            }
            int counter = 0;
            foreach (double ausgabe in Ausgaben)
            {
                RowDefinition row = new RowDefinition()
                {
                    Height = new GridLength((ausgabe / AusgabenGesamt) * AusgabenGrid.Height),
                };
                Border border = new Border()
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(2),
                };
                Rectangle rectangle = new Rectangle()
                {
                    ToolTip = $"{ausgabe * (-1)}€",
                    Fill = Brushes.Red,
                };
                border.Child = rectangle;
                Grid.SetRow(border, counter);
                AusgabenGrid.RowDefinitions.Add(row);
                AusgabenGrid.Children.Add(border);
                counter++;
            }
            counter = 0;
            foreach (double einkunft in Einkünfte)
            {
                RowDefinition row = new RowDefinition()
                {
                    Height = new GridLength((einkunft / EinkünfteGesamt) * EinkünfteGrid.Height),
                };
                Border border = new Border()
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(2),
                };
                Rectangle rectangle = new Rectangle()
                {
                    ToolTip = $"{einkunft}€",
                    Fill = Brushes.Orange,
                };
                border.Child = rectangle;
                Grid.SetRow(border, counter);
                EinkünfteGrid.RowDefinitions.Add(row);
                EinkünfteGrid.Children.Add(border);
                counter++;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for WindowGluecksspiel.xaml
    /// </summary>
    public partial class WindowGluecksspiel : Window
    {
        public bool Gluecksspielausgang = false;
        public double Endergebnis = 0;
        public WindowGluecksspiel()
        {
            InitializeComponent();
            LabelInfo.Content = "leicht: niedriges Verlustrisiko (20%) - kleine Gewinne (100%)\nmittel: mittleres Risiko (50%) - mittlere Gewinne (200%)\nschwer: hohes Risiko (90%) - hohe Gewinne (500%)";
            LabelInfo.HorizontalContentAlignment = HorizontalAlignment.Center;
            LabelInfo.VerticalContentAlignment = VerticalAlignment.Center;
            LabelInfo.FontSize = 12;
            LabelInfo.FontWeight = FontWeights.Bold;
        }

        private void ButtonOK_Click_1(object sender, RoutedEventArgs e)
        {
            if (TBEinsatz.Text != "")
            {
                try
                {
                    Endergebnis = Convert.ToDouble(TBEinsatz.Text);
                    Random rnd = new Random();
                    if (CBUser.Text == "leicht")
                    {
                        int erg = rnd.Next(1, 6);
                        if (erg <= 4)
                        {
                            this.Gluecksspielausgang = true;
                            MessageBox.Show($"GEWONNEN!\n Dein Gewinn liegt bei {Endergebnis * 1} €");
                        }
                        else
                        {
                            MessageBox.Show($"VERLOREN!\n Deine Verluste betragen {Endergebnis} €");
                        }
                    }
                    else if (CBUser.Text == "mittel")
                    {
                        int erg = rnd.Next(1, 3);
                        if (erg == 1)
                        {
                            this.Gluecksspielausgang = true;
                            MessageBox.Show($"GEWONNEN!\n Dein Gewinn liegt bei {Endergebnis * 2} €");
                            Endergebnis *= 2;
                        }
                        else
                        {
                            MessageBox.Show($"VERLOREN!\n Deine Verluste betragen {Endergebnis} €");
                        }
                    }
                    else if (CBUser.Text == "schwer")
                    {
                        int erg = rnd.Next(1, 11);
                        if (erg == 1)
                        {
                            this.Gluecksspielausgang = true;
                            MessageBox.Show($"GEWONNEN!\n Dein Gewinn liegt bei {Endergebnis * 5} €");
                            Endergebnis *= 5;
                        }
                        else
                        {
                            MessageBox.Show($"VERLOREN!\n Deine Verluste betragen {Endergebnis} €");
                        }
                    }
                    this.DialogResult = true;

                }
                catch
                {
                    MessageBox.Show("Bitte nur Zahlen eingeben", "Fehler", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Bitte alle Felder ausfüllen", "Fehler", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

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
    /// Interaction logic for WindowGeldAbheben.xaml
    /// </summary>
    public partial class WindowGeldAbheben : Window
    {
        public double Geldmenge = 0;
        public User user;
        public WindowGeldAbheben(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Geldmenge = Convert.ToDouble(TBGeldmenge.Text);
                if (Geldmenge >= 0 && user.Kontostand >= Geldmenge)
                {
                    DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Achtung! Nur positive Zahlen erlaubt und Kontostand überprüfen!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            catch
            {
                MessageBox.Show("Achtung! Nur Zahlen eingeben", "Fehler", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GeldLabel.Style = Styles.GetFontStyle(20);
            TBGeldmenge.Style = Styles.GetTextBoxStyle();
        }
    }
}

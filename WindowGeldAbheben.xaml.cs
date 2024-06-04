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

        public WindowGeldAbheben()
        {
            InitializeComponent();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Geldmenge = Convert.ToDouble(TBGeldmenge.Text);
                DialogResult = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Achtung! Nur Zahlen eingeben");
            }
        }
    }
}

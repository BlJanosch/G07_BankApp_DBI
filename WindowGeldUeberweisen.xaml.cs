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
using Microsoft.Data.Sqlite;

namespace BankingSystem
{
    /// <summary>
    /// Interaction logic for WindowGeldUeberweisen.xaml
    /// </summary>
    public partial class WindowGeldUeberweisen : Window
    {
        public double Geldmenge;
        public string Username;
        public WindowGeldUeberweisen()
        {
            InitializeComponent();
            foreach (string item in getUsernames())
            {
                TBUser.Items.Add(item);
            }
        }

        public List<string> getUsernames()
        {
            List<string> strings = new List<string>();
            using (SqliteConnection connection =
                new SqliteConnection("Data Source=assets/bank.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText =
                    $"SELECT name from tblUser";



                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        strings.Add(reader.GetString(0));
                    }
                }
                return strings;
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TBUser.SelectedIndex != -1)
                {
                    try
                    {
                        Geldmenge = Convert.ToDouble(TBGeldmenge.Text);
                        Username = TBUser.Text;
                        if (Geldmenge >= 0)
                        {
                            DialogResult = true;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Achtung! Nur positive Zahlen erlaubt!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Stop);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Achtung! Nur Zahlen eingeben", "Fehler", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                }
                else
                {
                    MessageBox.Show("Achtung! User muss eingegeben werden", "Fehler", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            catch
            {

            }
            
        }
    }
}

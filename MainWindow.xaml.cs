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
using Microsoft.Data.Sqlite;



namespace BankingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User currentUser = new User(1, "Noah Fedele", "Bartholomäberg", 7000, "testPW");
        public MainWindow()
        {
            InitializeComponent();
            initMain();
        }

        public void initMain()
        {
            this.LabelKontostand.Content = KontoStandHolen(currentUser.ID);
        }

        private void ButtonGluecksspiel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAbheben_Click(object sender, RoutedEventArgs e)
        {
            WindowGeldAbheben windowGeldAbheben = new WindowGeldAbheben();
            windowGeldAbheben.ShowDialog();
            if (windowGeldAbheben.DialogResult == true)
            {
                KontostandVeraendern(KontoStandHolen(currentUser.ID), windowGeldAbheben.Geldmenge*(-1), currentUser.ID);
            }
            initMain();

        }

        private void ButtonÜberweisen_Click(object sender, RoutedEventArgs e)
        {
            WindowGeldUeberweisen windowGeldUeberweisen = new WindowGeldUeberweisen();
            windowGeldUeberweisen.ShowDialog();
            if (windowGeldUeberweisen.DialogResult == true)
            {
                KontostandVeraendern(KontoStandHolen(UsernameTOUserID(windowGeldUeberweisen.Username)), windowGeldUeberweisen.Geldmenge, UsernameTOUserID(windowGeldUeberweisen.Username));
                KontostandVeraendern(KontoStandHolen(currentUser.ID), windowGeldUeberweisen.Geldmenge *(-1), currentUser.ID);
                initMain();
            }
        }


        public double KontoStandHolen(int UserID)
        {
            using (SqliteConnection connection =
                new SqliteConnection("Data Source=assets/bank.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText =
                    $"SELECT kontostand FROM tblUser WHERE id = {UserID}";



                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader.GetDouble(0);
                    }
                }
                return 0;
            }
        }

        public int UsernameTOUserID(string Username)
        {
            using (SqliteConnection connection =
                new SqliteConnection("Data Source=assets/bank.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText =
                    $"SELECT id FROM tblUser WHERE name = '{Username}'";



                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
                return -1;
            }
        }

        public void KontostandVeraendern(double aktuellerKontostand, double veraenderung, int UserID)
        {
            using (SqliteConnection connection =
                new SqliteConnection("Data Source=assets/bank.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText =
                    $"UPDATE tblUser SET kontostand = {aktuellerKontostand+veraenderung} WHERE id = {UserID}";

                command.ExecuteNonQuery();
            }
        }


        private void ButtonKontoLöschen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonKontoAbmelden_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowAnmelden windowAnmelden = new WindowAnmelden();
            windowAnmelden.ShowDialog();
        }
    }
}

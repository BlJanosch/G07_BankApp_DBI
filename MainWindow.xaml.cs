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
        public User MainUser;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        public void initMain()
        {
            this.LabelKontostand.Content = $"{MainUser.Kontostand} €";
            EintragListe.ItemsSource = Eintrag.GetEinträge(MainUser);
        }




        private void ButtonGluecksspiel_Click(object sender, RoutedEventArgs e)
        {
            WindowGluecksspiel windowGluecksspiel = new WindowGluecksspiel();
            windowGluecksspiel.ShowDialog();
            if (windowGluecksspiel.DialogResult == true)
            {
                if (windowGluecksspiel.Gluecksspielausgang == true)
                {
                    KontostandVeraendern(KontoStandHolen(MainUser.ID), Convert.ToDouble(windowGluecksspiel.Endergebnis), MainUser.ID);
                    EintragErstellen(MainUser.ID, DateTime.Now, Convert.ToDouble(windowGluecksspiel.Endergebnis), $"Geld gewonnen bei Glücksspiel");

                }
                else
                {
                    KontostandVeraendern(KontoStandHolen(MainUser.ID), Convert.ToDouble(windowGluecksspiel.Endergebnis) * (-1), MainUser.ID);
                    EintragErstellen(MainUser.ID, DateTime.Now, Convert.ToDouble(windowGluecksspiel.Endergebnis) * (-1), $"Geld verloren bei Glücksspiel");
                }
                initMain();
            }
        }

        private void ButtonAbheben_Click(object sender, RoutedEventArgs e)
        {
            WindowGeldAbheben windowGeldAbheben = new WindowGeldAbheben();
            windowGeldAbheben.ShowDialog();
            if (windowGeldAbheben.DialogResult == true)
            {
                KontostandVeraendern(KontoStandHolen(MainUser.ID), windowGeldAbheben.Geldmenge*(-1), MainUser.ID);
                EintragErstellen(MainUser.ID, DateTime.Now, windowGeldAbheben.Geldmenge * (-1), $"Geld abgehoben");
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
                KontostandVeraendern(KontoStandHolen(MainUser.ID), windowGeldUeberweisen.Geldmenge *(-1), MainUser.ID);
                
                EintragErstellen(MainUser.ID, DateTime.Now, windowGeldUeberweisen.Geldmenge * (-1), $"Geld überwiesen an {windowGeldUeberweisen.Username}");
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
            MainUser.DeleteUser();
            DrawAnmelden();
        }

        private void ButtonKontoAbmelden_Click(object sender, RoutedEventArgs e)
        {
            DrawAnmelden();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DrawAnmelden();
            // initMain();
        }

        public void DrawAnmelden()
        {
            LabelKontostand.Content = "";
            LabelName.Content = "";
            WindowAnmelden windowAnmelden = new WindowAnmelden();
            windowAnmelden.Owner = this;
            windowAnmelden.ShowDialog();
            if (windowAnmelden.DialogResult == true)
            {
                MainUser = windowAnmelden.MainUser;
                LabelName.Content = MainUser.Name;
                LabelKontostand.Content = $"{MainUser.Kontostand} €";
                EintragListe.ItemsSource = Eintrag.GetEinträge(MainUser);
                initMain();
            }
            else
            {
                this.Close();
            }
        }

        public void EintragErstellen(int fkUserID, DateTime date, double Betrag, string Beschreibung)
        {
            using (SqliteConnection connection =
                new SqliteConnection("Data Source=assets/bank.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText =
                    $"insert into tblEintrag (fkUserID, datum, betrag, beschreibung) values ({fkUserID}, '{date}', {Betrag}, '{Beschreibung}')";

                command.ExecuteNonQuery();
            }
        }
    }
}

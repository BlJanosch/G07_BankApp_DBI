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
            WindowGluecksspiel windowGluecksspiel = new WindowGluecksspiel(MainUser);
            windowGluecksspiel.ShowDialog();
            if (windowGluecksspiel.DialogResult == true)
            {
                if (windowGluecksspiel.Gluecksspielausgang == true)
                {
                    MainUser.Kontostand += Convert.ToDouble(windowGluecksspiel.Endergebnis);
                    KontostandVeraendern(MainUser, MainUser.ID);
                    EintragErstellen(MainUser.ID, DateTime.Now, Convert.ToDouble(windowGluecksspiel.Endergebnis), $"Geld gewonnen bei Glücksspiel");

                }
                else
                {
                    MainUser.Kontostand -= Convert.ToDouble(windowGluecksspiel.Endergebnis);
                    KontostandVeraendern(MainUser, MainUser.ID);
                    EintragErstellen(MainUser.ID, DateTime.Now, Convert.ToDouble(windowGluecksspiel.Endergebnis) * (-1), $"Geld verloren bei Glücksspiel");
                }
                initMain();
            }
        }

        private void ButtonAbheben_Click(object sender, RoutedEventArgs e)
        {
            WindowGeldAbheben windowGeldAbheben = new WindowGeldAbheben(MainUser);
            windowGeldAbheben.ShowDialog();
            if (windowGeldAbheben.DialogResult == true)
            {
                MainUser.Kontostand -= windowGeldAbheben.Geldmenge;
                KontostandVeraendern(MainUser, MainUser.ID);
                EintragErstellen(MainUser.ID, DateTime.Now, windowGeldAbheben.Geldmenge * (-1), $"Geld abgehoben");
            }
            initMain();

        }

        private void ButtonÜberweisen_Click(object sender, RoutedEventArgs e)
        {
            WindowGeldUeberweisen windowGeldUeberweisen = new WindowGeldUeberweisen(MainUser);
            windowGeldUeberweisen.ShowDialog();
            if (windowGeldUeberweisen.DialogResult == true)
            {
                User otherUser = GetUser(UsernameTOUserID(windowGeldUeberweisen.Username));
                otherUser.Kontostand += windowGeldUeberweisen.Geldmenge;
                KontostandVeraendern(otherUser, UsernameTOUserID(windowGeldUeberweisen.Username));
                MainUser.Kontostand -= windowGeldUeberweisen.Geldmenge;
                KontostandVeraendern(MainUser, MainUser.ID);
                
                EintragErstellen(MainUser.ID, DateTime.Now, windowGeldUeberweisen.Geldmenge * (-1), $"Geld überwiesen an {windowGeldUeberweisen.Username}");
                EintragErstellen(otherUser.ID, DateTime.Now, windowGeldUeberweisen.Geldmenge, $"Geld bekommen von {MainUser.Name}");

                initMain();
            }
            
        }


        public User GetUser(int UserID)
        {
            User user;
            using (SqliteConnection connection =
                new SqliteConnection("Data Source=assets/bank.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText =
                    $"SELECT * FROM tblUser WHERE id = {UserID}";



                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDouble(3), reader.GetString(4));
                        return user;
                    }
                }
            }
            return null;
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

        public void KontostandVeraendern(User user, int UserID)
        {
            using (SqliteConnection connection =
                new SqliteConnection("Data Source=assets/bank.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText =
                    $"UPDATE tblUser SET kontostand = {user.Kontostand} WHERE id = {UserID}";

                command.ExecuteNonQuery();
            }
        }


        private void ButtonKontoLöschen_Click(object sender, RoutedEventArgs e)
        {
            int AnzUsers = User.GetUsers().Count - 1;  
            MainUser.DeleteUser();
            Ladefenster ladefenster = new Ladefenster();
            ladefenster.ShowDialog();

            if (AnzUsers <= 0)
            {
                DrawAnmelden(false);
            }
            else
            {
                DrawAnmelden(true);
            }
            
        }

        private void ButtonKontoAbmelden_Click(object sender, RoutedEventArgs e)
        {
            DrawAnmelden(true);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DrawAnmelden(true);
            // initMain();
        }

        public void DrawAnmelden(bool ButtonEnabled)
        {
            LabelKontostand.Content = "";
            LabelName.Content = "";
            WindowAnmelden windowAnmelden = new WindowAnmelden();
            windowAnmelden.ButtonAnmelden.IsEnabled = ButtonEnabled;
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

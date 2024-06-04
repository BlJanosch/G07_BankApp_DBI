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
        
        public MainWindow()
        {
            InitializeComponent();
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

            }
        }

        private void ButtonÜberweisen_Click(object sender, RoutedEventArgs e)
        {

        }


        public double KontoStandHolen()
        {
            using (SqliteConnection connection =
                new SqliteConnection("Data Source=Assets/GartenPlaner.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText =
                    @"SELECT kontostand FROM tblUser WHERE id = 1";



                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader.GetDouble(0);
                    }
                }


            }
            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLitePCL;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace BankingSystem
{
    public class User
    {
        public int ID;
        public string Name;
        public string Standort;
        public double Kontostand;
        public string Passwort;

        public User(int id, string name, string standort, double kontostand, string passwort)
        {
            ID = id;
            Name = name;
            Standort = standort;
            Kontostand = kontostand;
            Passwort = passwort;
        }

        public static List<User> GetUsers()
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=Assets/bank.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = @"SELECT * FROM tblUser";

                List<User> Users = new List<User>();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User(reader.GetInt16(1), reader.GetString(2), reader.GetString(3), reader.GetDouble(4), reader.GetString(5));
                        Users.Add(user);
                    }
                }
                return Users;
            }
        }
    }
}

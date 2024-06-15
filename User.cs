using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLitePCL;
using System.Threading.Tasks;
using System.Security.RightsManagement;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography;

namespace BankingSystem
{
    public class User
    {
        public int ID;
        public string Name;
        public string Standort;
        public double Kontostand;
        public string Passwort;
        public static User Admin = new User(0, "Admin", "Default", 0, PasswordToHash("Admin"));

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
            using (SqliteConnection connection = new SqliteConnection("Data Source=assets/bank.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = @"SELECT * FROM tblUser";

                List<User> Users = new List<User>();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User(reader.GetInt16(0), reader.GetString(1), reader.GetString(2), reader.GetDouble(3), reader.GetString(4));
                        Users.Add(user);
                    }
                }
                return Users;
            }
        }

        public static string PasswordToHash(string Password)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(Password);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                // Gib den Hash-Wert aus
                string hashString = builder.ToString();
                return hashString;
            }
        }

        public void SaveUser()
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=assets/bank.db"))
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = $"INSERT INTO tblUser(id, name, standort, kontostand, passwort) VALUES({ID}, '{Name}', '{Standort}', {Kontostand}, '{Passwort}');";

                int tmp = command.ExecuteNonQuery();
            }
        }

        public void DeleteUser()
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=assets/bank.db"))
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = $"DELETE FROM tblUser WHERE id = {ID};";

                int tmp = command.ExecuteNonQuery();
            }

            using (SqliteConnection connection = new SqliteConnection("Data Source=assets/bank.db"))
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = $"DELETE FROM tblEintrag WHERE fkUserID = {ID};";

                int tmp = command.ExecuteNonQuery();
            }
        }

        public static void CreatAdminUser()
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=assets/bank.db"))
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = $"INSERT INTO tblUser(id, name, standort, kontostand, passwort) VALUES({Admin.ID}, '{Admin.Name}', '{Admin.Standort}', {Admin.Kontostand}, '{Admin.Passwort}');";

                int tmp = command.ExecuteNonQuery();
            }
        }
    }
}

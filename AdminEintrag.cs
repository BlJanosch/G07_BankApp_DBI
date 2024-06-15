using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    public class AdminEintrag
    {
        public string Name;
        public string Standort;
        public double Kontostand;
        public DateTime Datum;
        public double Betrag;
        public string Beschreibung;

        public AdminEintrag(string name, string standort, double kontostand, DateTime datum, double betrag, string beschreibung)
        {
            Name = name; 
            Standort = standort; 
            Kontostand = kontostand;
            Datum = datum; 
            Betrag = betrag; 
            Beschreibung = beschreibung;
        }

        public static List<AdminEintrag> GetAdminEinträge()
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=Assets/bank.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = @"Select name, standort, kontostand, datum, betrag, beschreibung from tblUser join tblEintrag on tblUser.id = tblEintrag.fkUserID;";

                List<AdminEintrag> Einträge = new List<AdminEintrag>();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime date = Convert.ToDateTime(reader.GetString(3));
                        AdminEintrag adminEintrag = new AdminEintrag(reader.GetString(0), reader.GetString(1), reader.GetDouble(2), date, reader.GetDouble(4), reader.GetString(5));
                        Einträge.Add(adminEintrag);
                    }
                }
                return Einträge;
            }
        }

        public override string ToString()
        {
            return $"Benutzer: {Name} - Beschreibung: {Beschreibung}";
        }
    }
}

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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BankingSystem
{
    /// <summary>
    /// Interaction logic for WindowAnmelden.xaml
    /// </summary>
    public partial class WindowAnmelden : Window
    {
        public TextBox NameInput;
        public TextBox PasswortInput;
        public TextBox StandortInput;
        public User MainUser;
        public List<User> users;

        public WindowAnmelden()
        {
            InitializeComponent();
        }

        private void ButtonRegistrieren_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            for (int x = 0; x < 4; x++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                MainGrid.RowDefinitions.Add(row);
            }
            ColumnDefinition ColumnLabel = new ColumnDefinition();
            ColumnLabel.Width = new GridLength(1, GridUnitType.Star); 
            MainGrid.ColumnDefinitions.Add(ColumnLabel);
            ColumnDefinition ColumnInput = new ColumnDefinition();
            ColumnInput.Width = new GridLength(3, GridUnitType.Star);
            MainGrid.ColumnDefinitions.Add(ColumnInput);

            Label Name = new Label()
            {
                Content = "Name:",
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Label Passwort = new Label()
            {
                Content = "Passwort:",
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Label Standort = new Label()
            {
                Content = "Standort:",
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            MainGrid.Children.Add(Name);
            Grid.SetRow(Passwort, 1);
            MainGrid.Children.Add(Passwort);
            Grid.SetRow(Standort, 2);
            MainGrid.Children.Add(Standort);

            NameInput = new TextBox()
            {
                TextAlignment = TextAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            PasswortInput = new TextBox()
            {
                TextAlignment = TextAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            StandortInput = new TextBox()
            {
                TextAlignment = TextAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            Grid.SetColumn(NameInput, 1);
            Grid.SetColumn(PasswortInput, 1);
            Grid.SetColumn(StandortInput, 1);
            Grid.SetRow(PasswortInput, 1);
            Grid.SetRow(StandortInput, 2);
            MainGrid.Children.Add(NameInput);
            MainGrid.Children.Add(PasswortInput);
            MainGrid.Children.Add(StandortInput);

            Button ButtonAbbrechen = new Button()
            {
                Content = "Abbrechen",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20, 0, 0, 0),
                Width = 150,
                Height = 50
            };
            ButtonAbbrechen.Click += ButtonAbbrechen_Click;
            Grid.SetColumn(ButtonAbbrechen, 1);
            Grid.SetRow(ButtonAbbrechen, 3);
            MainGrid.Children.Add(ButtonAbbrechen);

            Button ButtonOK = new Button()
            {
                Content = "OK",
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 20, 0),
                Width = 150,
                Height = 50
            };
            ButtonOK.Click += ButtonOK_Click;
            Grid.SetColumn(ButtonOK, 1);
            Grid.SetRow(ButtonOK, 3);
            MainGrid.Children.Add(ButtonOK);

        }

        private void ButtonAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.RowDefinitions.Clear();
            MainGrid.ColumnDefinitions.Clear();

            Button ButtonAnmeldenNew = new Button
            {
                Height = 80,
                Width = 400,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = "Anmelden",
                Margin = new Thickness(0, 0, 0, 100),
            };
            ButtonAnmeldenNew.Click += ButtonAnmelden_Click;

            Button ButtonRegestrierenNew = new Button
            {
                Height = 80,
                Width = 400,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = "Regestrieren",
                Margin = new Thickness(0, 100, 0, 0),
            };
            ButtonRegestrierenNew.Click += ButtonRegistrieren_Click;
            MainGrid.Children.Add(ButtonAnmeldenNew);
            MainGrid.Children.Add(ButtonRegestrierenNew);
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            users = User.GetUsers();
            bool NameVergeben = false;
            foreach (User user in users)
            {
                if (user.Name == NameInput.Text)
                {
                    MessageBox.Show("Dieser Name ist bereits vergeben!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    NameVergeben = true;
                    break;
                }
            }
            if (!NameVergeben)
            {
                int id;
                if (users.Count == 0)
                {
                    id = 0;
                }
                else
                {
                    id = users[users.Count - 1].ID + 1;
                }
                MainUser = new User(id, NameInput.Text, StandortInput.Text, 50, User.PasswordToHash(PasswortInput.Text));
                MainUser.SaveUser();
                DialogResult = true;
            }
        }

        private void ButtonAnmelden_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            for (int x = 0; x < 3; x++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                MainGrid.RowDefinitions.Add(row);
            }
            ColumnDefinition ColumnLabel = new ColumnDefinition();
            ColumnLabel.Width = new GridLength(1, GridUnitType.Star);
            MainGrid.ColumnDefinitions.Add(ColumnLabel);
            ColumnDefinition ColumnInput = new ColumnDefinition();
            ColumnInput.Width = new GridLength(3, GridUnitType.Star);
            MainGrid.ColumnDefinitions.Add(ColumnInput);

            Label Name = new Label()
            {
                Content = "Name:",
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Label Passwort = new Label()
            {
                Content = "Passwort:",
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };


            MainGrid.Children.Add(Name);
            Grid.SetRow(Passwort, 1);
            MainGrid.Children.Add(Passwort);

            NameInput = new TextBox()
            {
                TextAlignment = TextAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            PasswortInput = new TextBox()
            {
                TextAlignment = TextAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };

            Grid.SetColumn(NameInput, 1);
            Grid.SetColumn(PasswortInput, 1);
            Grid.SetRow(PasswortInput, 1);
            MainGrid.Children.Add(NameInput);
            MainGrid.Children.Add(PasswortInput);

            Button ButtonAbbrechen = new Button()
            {
                Content = "Abbrechen",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20, 0, 0, 0),
                Width = 150,
                Height = 50
            };
            ButtonAbbrechen.Click += ButtonAbbrechen_Click;
            Grid.SetColumn(ButtonAbbrechen, 1);
            Grid.SetRow(ButtonAbbrechen, 3);
            MainGrid.Children.Add(ButtonAbbrechen);

            Button ButtonOK = new Button()
            {
                Content = "OK",
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 20, 0),
                Width = 150,
                Height = 50
            };
            ButtonOK.Click += ButtonOK_Click1; ;
            Grid.SetColumn(ButtonOK, 1);
            Grid.SetRow(ButtonOK, 3);
            MainGrid.Children.Add(ButtonOK);
        }

        private void ButtonOK_Click1(object sender, RoutedEventArgs e)
        {
            users = User.GetUsers();
            int counter = 0;
            foreach (User user in users)
            {
                if (user.Name == NameInput.Text && user.Passwort == User.PasswordToHash(PasswortInput.Text))
                {
                    MainUser = new User(users[counter].ID, NameInput.Text, users[counter].Standort, users[counter].Kontostand, users[counter].Passwort);
                    DialogResult = true;
                }
                counter++;
            }
        }
    }
}

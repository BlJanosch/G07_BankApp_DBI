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
using System.Windows.Threading;

namespace BankingSystem
{
    /// <summary>
    /// Interaction logic for Ladefenster.xaml
    /// </summary>
    public partial class Ladefenster : Window
    {
        public Ladefenster()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick;
            timer.Start();
            DispatcherTimer timerProgessBar = new DispatcherTimer();
            timerProgessBar.Interval = TimeSpan.FromSeconds(0.5);
            timerProgessBar.Tick += TimerProgessBar_Tick;
            timerProgessBar.Start();
        }

        private void TimerProgessBar_Tick(object? sender, EventArgs e)
        {
            ProgressBar.Value += 25;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DeleteLabel.Style = Styles.GetFontStyle(20);
            LoadingLabel.Style = Styles.GetFontStyle(20);
        }
    }
}

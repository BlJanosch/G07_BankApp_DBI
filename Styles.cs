using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BankingSystem
{
    public class Styles
    {
        public static Style GetButtonStyle()
        {
            Style buttonStyle = new Style(typeof(Button));

            buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Color.FromRgb(38, 80, 38))));
            buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.White));
            buttonStyle.Setters.Add(new Setter(Button.FontSizeProperty, 15.0));
            buttonStyle.Setters.Add(new Setter(Button.FontFamilyProperty, new FontFamily("Aharoni")));
            buttonStyle.Setters.Add(new Setter(Button.WidthProperty, 200.0));
            buttonStyle.Setters.Add(new Setter(Button.HeightProperty, 30.0));
            buttonStyle.Setters.Add(new Setter(Button.VerticalAlignmentProperty, VerticalAlignment.Center));
            buttonStyle.Setters.Add(new Setter(Button.MarginProperty, new Thickness(0, 20, 0, 0)));
            buttonStyle.Setters.Add(new Setter(Button.FontWeightProperty, FontWeights.Bold));

            Trigger trigger = new Trigger();
            trigger.Property = Button.IsMouseOverProperty;
            trigger.Value = true;
            trigger.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.DarkCyan));
            trigger.Setters.Add(new Setter(Button.CursorProperty, Cursors.Hand)); // Fügt den Cursor-Wechsel hinzu
            buttonStyle.Triggers.Add(trigger);

            return buttonStyle;
        }

        public static Style GetFontStyle(double FontSize)
        {
            Style labelStyle = new Style();
            labelStyle.Setters.Add(new Setter(Label.FontSizeProperty, FontSize));
            labelStyle.Setters.Add(new Setter(Label.FontFamilyProperty, new FontFamily("Aharoni")));
            labelStyle.Setters.Add(new Setter(Label.FontWeightProperty, FontWeights.Bold));
            labelStyle.Setters.Add(new Setter(Label.ForegroundProperty, Brushes.White));

            return labelStyle;
        }

        public static Style GetTextBoxStyle()
        {
            Style TextBox = new Style();
            TextBox.Setters.Add(new Setter(System.Windows.Controls.TextBox.BackgroundProperty, Brushes.Transparent));
            TextBox.Setters.Add(new Setter(System.Windows.Controls.TextBox.FontFamilyProperty, new FontFamily("Aharoni")));
            TextBox.Setters.Add(new Setter(System.Windows.Controls.TextBox.FontWeightProperty, FontWeights.Bold));
            TextBox.Setters.Add(new Setter(System.Windows.Controls.TextBox.ForegroundProperty, Brushes.White));
            TextBox.Setters.Add(new Setter(System.Windows.Controls.TextBox.ForegroundProperty, Brushes.White));

            return TextBox;
        }

        public static Style GetComboBoxStyle()
        {
            Style ComboBox = new Style();
            ComboBox.Setters.Add(new Setter(ComboBoxItem.FontFamilyProperty, new FontFamily("Aharoni")));
            ComboBox.Setters.Add(new Setter(ComboBoxItem.FontWeightProperty, FontWeights.Bold));
            ComboBox.Setters.Add(new Setter(ComboBoxItem.ForegroundProperty, Brushes.Black));

            return ComboBox;
        }
    }
}

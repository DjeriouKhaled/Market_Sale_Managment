using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Market.Views
{
    public class CurrencyTextBox : TextBox
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(decimal?),
            typeof(CurrencyTextBox),
            new FrameworkPropertyMetadata(new decimal?(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                ValuePropertyChanged));

        public CurrencyTextBox()
        {
            CurrencySymbol = "دج ";
            CurrencyDecimalPlaces = 2;
            DecimalSeparator = ",";
            ThousandSeparator = ".";
            Culture = "ar";
        }

        public string CurrencySymbol { get; set; }
        private int CurrencyDecimalPlaces { get; }
        public string DecimalSeparator { get; set; }
        public string ThousandSeparator { get; set; }
        public string Culture { get; set; }

        public decimal? Value
        {
            get { return (decimal?) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private bool IsValidKey(int k)
        {
            return k >= 34 && k <= 43 //digits 0 to 9
                   || k >= 74 && k <= 83 //numeric keypad 0 to 9
                   || k == 2 //back space
                   || k == 32 //delete
                ;
        }

        private string Format(string text)
        {
            var unformatedString = text == string.Empty ? "0,00" : text; //Initial state is always string.empty
            unformatedString = unformatedString.Replace(CurrencySymbol, ""); //Remove currency symbol from text
            unformatedString = unformatedString.Replace(DecimalSeparator, ""); //Remove separators (decimal)
            unformatedString = unformatedString.Replace(ThousandSeparator, ""); //Remove separators (thousands)
            var number =
                decimal.Parse(unformatedString) /
                (decimal) Math.Pow(10,
                    CurrencyDecimalPlaces); //The value will have 'x' decimal places, so divide it by 10^x
            unformatedString = number.ToString("C", CultureInfo.CreateSpecificCulture(Culture));
            return unformatedString;
        }

        private decimal FormatBack(string text)
        {
            var unformatedString = text == string.Empty ? "0.00" : text;
            unformatedString = unformatedString.Replace(CurrencySymbol, ""); //Remove currency symbol from text
            unformatedString = unformatedString.Replace(ThousandSeparator, ""); //Remove separators (thousands);
            var current =
                Thread.CurrentThread
                    .CurrentUICulture; //Let's change the culture to avoid "Input string was in an incorrect format"
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(Culture);
            var returnValue = decimal.Parse(unformatedString);
            Thread.CurrentThread.CurrentUICulture =
                current; //And now change it back, cuz we don't own the world, right?
            return returnValue;
        }

        private void ValueChanged(object sender, TextChangedEventArgs e)
        {
            // Keep the caret at the end
            CaretIndex = Text.Length;
        }

        private void MouseClicked(object sender, MouseButtonEventArgs e)
        {
            // Prevent changing the caret index
            e.Handled = true;
            Focus();
        }

        private void MouseReleased(object sender, MouseButtonEventArgs e)
        {
            // Prevent changing the caret index
            e.Handled = true;
            Focus();
        }

        private void KeyReleased(object sender, KeyEventArgs e)
        {
            Text = Format(Text);
            Value = FormatBack(Text);
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (IsValidKey((int) e.Key))
                return;
            e.Handled = true;
            CaretIndex = Text.Length;
        }

        private void PastingEventHandler(object sender, DataObjectEventArgs e)
        {
            // Prevent/disable paste
            e.CancelCommand();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            DataObject.AddCopyingHandler(this, PastingEventHandler);
            DataObject.AddPastingHandler(this, PastingEventHandler);
            CaretIndex = Text.Length;
            KeyDown += KeyPressed;
            KeyUp += KeyReleased;
            PreviewMouseDown += MouseClicked;
            PreviewMouseUp += MouseReleased;
            TextChanged += ValueChanged;
            Text = Format(string.Empty);
        }

        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CurrencyTextBox) d).Value = ((CurrencyTextBox) d).FormatBack(e.NewValue.ToString());
        }
    }
}
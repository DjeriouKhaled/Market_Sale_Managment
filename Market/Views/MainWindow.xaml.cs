using DevExpress.Xpf.Bars;
using Market.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Market.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private static bool IsTextAllowed(string text)
        {
            //regex that matches disallowed text
            Regex regex = new Regex("\\D");
            return !regex.IsMatch(text);
        }

        private void bobo(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);

        }


        private void Btn_false_OnClick(object sender, RoutedEventArgs e)
        {

            if (Qt_TextEdit.IsKeyboardFocusWithin)
            {
                if (Qt_TextEdit.Text.Length != 0)
                {
                    Qt_TextEdit.Text = Qt_TextEdit.Text.Remove(Qt_TextEdit.Text.Length - 1);
                }
            }
            else if (CodeBar.IsKeyboardFocusWithin)
            {
                if (CodeBar.Text.Length != 0)
                {
                    CodeBar.Text = CodeBar.Text.Remove(CodeBar.Text.Length - 1);
                }
            }
            else
            {
                if (tbPositionCursor.Text.Length != 0)
                {
                    tbPositionCursor.Text = tbPositionCursor.Text.Remove(tbPositionCursor.Text.Length - 1);
                }

            }
        }


        private void Btn_0_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("0"); }
        private void Btn_1_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("1"); }
        private void Btn_2_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("2"); }
        private void Btn_3_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("3"); }
        private void Btn_4_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("4"); }
        private void Btn_5_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("5"); }
        private void Btn_6_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("6"); }
        private void Btn_7_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("7"); }
        private void Btn_8_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("8"); }
        private void Btn_9_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("9"); }


        private void AddNumberToEditText(string s)
        {
            if (Qt_TextEdit.IsKeyboardFocusWithin)
            {
                Qt_TextEdit.Text = Qt_TextEdit.Text + s;
                // MakeIndexInLast
                Qt_TextEdit.Text = Qt_TextEdit.Text.Trim();
                Qt_TextEdit.Select(Qt_TextEdit.Text.Length, 0);
            }
            else if (CodeBar.IsKeyboardFocusWithin)
            {
                CodeBar.Text = CodeBar.Text + s;
                // MakeIndexInLast
                CodeBar.Text = CodeBar.Text.Trim();
                CodeBar.Select(CodeBar.Text.Length, 0);
            }
            else
            {
                tbPositionCursor.Text = tbPositionCursor.Text + s;
                // MakeIndexInLast
                tbPositionCursor.Select(tbPositionCursor.Text.Length, 0);
            }
        }


        private void AboutAPP_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var cApp = ((App)Application.Current);
            cApp.MainWindow = new AboutApp();
            cApp.MainWindow.ShowDialog();
        }
    }


}

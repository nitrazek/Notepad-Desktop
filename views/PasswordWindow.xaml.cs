using NotepadDesktop.models;
using NotepadDesktop.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NotepadDesktop.views
{
    public partial class PasswordWindow : Window
    {
        private PasswordViewModel viewModel;
        private bool _checkMode;

        public Note NoteForViewModel
        {
            set { viewModel.SelectedNote = value; }
        }
        public bool SetModeToCheck
        {
            set { _checkMode = value; }
        }

        public PasswordWindow(PasswordViewModel passwordViewModel)
        {
            InitializeComponent();
            viewModel = passwordViewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void passwordBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (_checkMode)
            {

            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

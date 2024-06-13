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
        private Action? _postCheckMethod;

        public Note NoteForViewModel
        {
            set { viewModel.SelectedNote = value; }
        }
        public bool SetModeToCheck
        {
            set { _checkMode = value; }
        }
        public Action? SetPostCheckMethod
        {
            set { _postCheckMethod = value; }
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
            passwordBox.Clear();
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
                if (passwordBox.Text.Length == 0)
                {
                    ErrorMessage.Visibility = Visibility.Visible;
                    return;
                }
                bool valid = viewModel.CheckPassword(int.Parse(passwordBox.Text));
                if (valid)
                {
                    ErrorMessage.Visibility = Visibility.Hidden;
                    _postCheckMethod?.Invoke();
                    Hide();
                    passwordBox.Clear();
                }
                else
                {
                    ErrorMessage.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (passwordBox.Text.Length == 0)
                {
                    ErrorMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    ErrorMessage.Visibility = Visibility.Hidden;
                    viewModel.SetPassword(int.Parse(passwordBox.Text));
                    _postCheckMethod?.Invoke();
                    Hide();
                    passwordBox.Clear();
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}

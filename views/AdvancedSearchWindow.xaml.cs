using NotepadDesktop.viewModels;
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

namespace NotepadDesktop.views
{
    public partial class AdvancedSearchWindow : Window
    {
        private AdvancedSearchViewModel viewModel;
        public AdvancedSearchWindow(AdvancedSearchViewModel advancedSearchViewModel)
        {
            DataContext = advancedSearchViewModel;
            viewModel = advancedSearchViewModel;
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ErrorMessage.Visibility = Visibility.Hidden;
            Hide();
        }

        private void ClearFilers_Button_Click(object sender, RoutedEventArgs e)
        {
            name.Text = string.Empty;
            content.Text = string.Empty;
            fromCreatingDTP.Value = null;
            toCreatingDTP.Value = null;
            fromNotificationDTP.Value = null;
            toNotificationDTP.Value = null;
            ErrorMessage.Visibility = Visibility.Hidden;
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            if (fromCreatingDTP.Value != null && toCreatingDTP.Value != null && fromCreatingDTP.Value.Value.CompareTo(toCreatingDTP.Value.Value) > 0)
            {
                ErrorMessage.Text = "Podano błędny zakres dat utworzenia notatki";
                ErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            if (fromNotificationDTP.Value != null && toNotificationDTP.Value != null && fromNotificationDTP.Value.Value.CompareTo(toNotificationDTP.Value.Value) > 0)
            {
                ErrorMessage.Text = "Podano błędny zakres dat powiadomień";
                ErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            ErrorMessage.Text = "";
            ErrorMessage.Visibility = Visibility.Hidden;
            viewModel.filterTreeView(name.Text, content.Text, fromCreatingDTP.Value, toCreatingDTP.Value, fromNotificationDTP.Value, toNotificationDTP.Value);
            Hide();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Visibility = Visibility.Hidden;
            Hide();
        }
    }
}

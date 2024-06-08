using NotepadDesktop.models;
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
    public partial class ConfirmationWindow : Window
    {
        public ConfirmationViewModel viewModel;
        public Note? NoteForViewModel
        {
            set 
            { 
                viewModel.SelectedNote = value;
                if (value == null)
                {
                    confirmationText.Text = "Nie można utworzyć notatki bez folderu!";
                    cancelButton.Visibility = Visibility.Hidden;
                    Title = "Brak folderu";
                }
                else
                {
                    confirmationText.Text = "Czy na pewno chcesz usunąć notatkę?";
                    cancelButton.Visibility = Visibility.Visible;
                    Title = "Czy na pewno?";
                }
            }
        }

        public ConfirmationWindow(ConfirmationViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteSelectedNote();
            Hide();
        }
    }
}

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
    /// <summary>
    /// Logika interakcji dla klasy FolderNameWindow.xaml
    /// </summary>
    public partial class FolderNameWindow : Window
    {
        private FolderNameViewModel viewModel;
        public FolderNameWindow(FolderNameViewModel folderNameViewModel)
        {
            InitializeComponent();
            DataContext = folderNameViewModel;
            viewModel = folderNameViewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ErrorMessage.Visibility = Visibility.Hidden;
            Hide();
        }

        private void CreateFolder_Button_Click(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text.Length <= 0)
            {
                ErrorMessage.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorMessage.Visibility = Visibility.Hidden;
                viewModel.CreateFolder(nameBox.Text);
                nameBox.Text = string.Empty;
                Hide();
            }
        }
    }
}

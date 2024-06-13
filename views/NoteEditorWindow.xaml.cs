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
    public partial class NoteEditorWindow : Window
    {

        private NoteEditorViewModel viewModel;
        public Note? NoteForViewModel
        {
            set
            { 
                viewModel.SelectedNote = value;
                viewModel.UpdateFolders();
            }
        }

        public NoteEditorWindow(NoteEditorViewModel noteEditorViewModel)
        {
            InitializeComponent();
            DataContext = noteEditorViewModel;
            viewModel = noteEditorViewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.SelectedNote = null;
            e.Cancel = true;
            ErrorMessage.Visibility = Visibility.Hidden;
            Hide();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if(noteTitle.Text.Length <= 0)
            {
                ErrorMessage.Visibility = Visibility.Visible;
            }
            else if (viewModel.SelectedNote != null)
            {
                ErrorMessage.Visibility = Visibility.Hidden;
                viewModel.SaveNote();
                Hide();
            }
            else
            {
                ErrorMessage.Visibility = Visibility.Hidden;
                viewModel.CreateNote(noteTitle.Text, noteContent.Text, ((Folder)folderComboBox.SelectedItem).Id);
                noteTitle.Text = string.Empty;
                noteContent.Text = string.Empty;
                Hide();
            }
        }
    }
}

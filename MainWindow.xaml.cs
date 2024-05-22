using NotepadDesktop.models;
using NotepadDesktop.viewModels;
using NotepadDesktop.views;
using System.ComponentModel;
using System.Windows;

namespace NotepadDesktop
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;
        
        private NoteEditorWindow noteEditorWindow;
        private ConfirmationWindow confirmationWindow;
        private PasswordWindow passwordWindow;
        private AdvancedSearchWindow advancedSearchWindow;

        public MainWindow(MainWindowViewModel mainWindowViewModel, NoteEditorWindow noteEditorWindow, ConfirmationWindow confirmationWindow, PasswordWindow passwordWindow, AdvancedSearchWindow advancedSearchWindow)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
            viewModel = mainWindowViewModel;
            this.noteEditorWindow = noteEditorWindow;
            this.confirmationWindow = confirmationWindow;
            this.passwordWindow = passwordWindow;
            this.advancedSearchWindow = advancedSearchWindow;
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.CurrentNote == null) return;
            noteEditorWindow.Owner = this;
            noteEditorWindow.NoteForViewModel = viewModel.CurrentNote;
            noteEditorWindow.ShowDialog();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.CurrentNote == null) return;
            confirmationWindow.Owner = this;
            confirmationWindow.ShowDialog();
        }

        private void Encrypt_Button_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.CurrentNote == null) return;
            passwordWindow.Owner = this;
            passwordWindow.ShowDialog();
        }

        private void Export_Button_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.CurrentNote == null) return;

        }

        private void Filter_Button_Click(object sender, RoutedEventArgs e)
        {
            advancedSearchWindow.Owner = this;
            advancedSearchWindow.ShowDialog();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            noteEditorWindow.Owner = this;
            noteEditorWindow.ShowDialog();
            viewModel.updateNotes();
        }

        private void ListBox_Selected(object sender, RoutedEventArgs e)
        {
            if (NoteListBox.SelectedItem == null) return;

            Note selectedNote = (Note)NoteListBox.SelectedItem;
            viewModel.CurrentNote = selectedNote;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Application.Current.Shutdown();
        }
    }
}
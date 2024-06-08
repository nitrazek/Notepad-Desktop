using NotepadDesktop.models;
using NotepadDesktop.viewModels;
using NotepadDesktop.views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace NotepadDesktop
{
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        private NoteEditorWindow noteEditorWindow;
        private ConfirmationWindow confirmationWindow;
        private PasswordWindow passwordWindow;
        private AdvancedSearchWindow advancedSearchWindow;
        private ICollectionView collectionView;

        public MainWindow(MainViewModel mainWindowViewModel, NoteEditorWindow noteEditorWindow, ConfirmationWindow confirmationWindow, PasswordWindow passwordWindow, AdvancedSearchWindow advancedSearchWindow)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
            viewModel = mainWindowViewModel;
            this.noteEditorWindow = noteEditorWindow;
            this.confirmationWindow = confirmationWindow;
            this.passwordWindow = passwordWindow;
            this.advancedSearchWindow = advancedSearchWindow;
            collectionView = CollectionViewSource.GetDefaultView(viewModel.Folders);
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.CurrentNote == null) return;
            noteEditorWindow.Owner = this;
            noteEditorWindow.NoteForViewModel = new Note(viewModel.CurrentNote);
            noteEditorWindow.ShowDialog();
            viewModel.updateFolders();
            collectionView = CollectionViewSource.GetDefaultView(viewModel.Folders);
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.CurrentNote == null) return;
            confirmationWindow.Owner = this;
            confirmationWindow.NoteForViewModel = new Note(viewModel.CurrentNote);
            confirmationWindow.ShowDialog();
            viewModel.updateFolders();
            collectionView = CollectionViewSource.GetDefaultView(viewModel.Folders);
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
            if (viewModel.Folders.Count == 0)
            {
                confirmationWindow.Owner = this;
                confirmationWindow.NoteForViewModel = null;
                confirmationWindow.ShowDialog();
                return;
            }
            noteEditorWindow.Owner = this;
            noteEditorWindow.ShowDialog();
            viewModel.updateFolders();
            collectionView = CollectionViewSource.GetDefaultView(viewModel.Folders);
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedItem = e.NewValue as Note;
            if (selectedItem != null)
            {
                viewModel.CurrentNote = selectedItem;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Application.Current.Shutdown();
        }

        private void FilterTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            collectionView.Filter = (item) =>
            {
                if (string.IsNullOrEmpty(FilterTextBox.Text))
                    return true;

                var folder = item as Folder;
                if (folder != null)
                {
                    return folder.Notes.Any(note => note.Title.IndexOf(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                return false;
            };

            collectionView.Refresh();
        }
    }
}

using NotepadDesktop.models;
using NotepadDesktop.viewModels;
using NotepadDesktop.views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using Microsoft.Win32;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using NotepadDesktop.utils;

namespace NotepadDesktop
{
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        private NoteEditorWindow noteEditorWindow;
        private ConfirmationWindow confirmationWindow;
        private PasswordWindow passwordWindow;
        private AdvancedSearchWindow advancedSearchWindow;
        private FolderNameWindow folderNameWindow;
        private ICollectionView collectionView;

        public MainWindow(MainViewModel mainWindowViewModel, NoteEditorWindow noteEditorWindow, ConfirmationWindow confirmationWindow, PasswordWindow passwordWindow, FolderNameWindow folderNameWindow, AdvancedSearchWindow advancedSearchWindow)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
            viewModel = mainWindowViewModel;
            this.noteEditorWindow = noteEditorWindow;
            this.confirmationWindow = confirmationWindow;
            this.passwordWindow = passwordWindow;
            this.folderNameWindow = folderNameWindow;
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
            passwordWindow.NoteForViewModel = new Note(viewModel.CurrentNote);
            passwordWindow.SetModeToCheck = false;
            passwordWindow.ShowDialog();
            viewModel.updateFolders();
            collectionView = CollectionViewSource.GetDefaultView(viewModel.Folders);
        }

        [Obsolete]
        private void Export_Button_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.CurrentNote == null) return;
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF file (*.pdf)|*.pdf",
                FileName = viewModel.CurrentNote.Title + ".pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                PdfExporter.ExportNoteToPdf(viewModel.CurrentNote, saveFileDialog.FileName);
            }
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
            noteEditorWindow.NoteForViewModel = null;
            noteEditorWindow.ShowDialog();
            viewModel.updateFolders();
            collectionView = CollectionViewSource.GetDefaultView(viewModel.Folders);
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedItem = e.NewValue as Note;
            if (selectedItem == null) return;

            if (selectedItem.Password == null)
            {
                viewModel.CurrentNote = selectedItem;
                return;
            }
                
            passwordWindow.Owner = this;
            passwordWindow.NoteForViewModel = selectedItem;
            passwordWindow.SetModeToCheck = true;
            passwordWindow.SetPostCheckMethod = () =>
            {
                viewModel.CurrentNote = selectedItem;
            };
            passwordWindow.ShowDialog();
          }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Application.Current.Shutdown();
        }

        private void FilterTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string filterText = FilterTextBox.Text.ToLower();
            collectionView = CollectionViewSource.GetDefaultView(viewModel.Folders);

            collectionView.Filter = (item) =>
            {
                var folder = item as Folder;
                if (folder == null) return false;

                if (filterText.Length == 0) return true;

                folder.Notes = folder.Notes
                    .Where(note => note.Title.Contains(filterText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                return folder.Notes.Any();
            };

            collectionView.Refresh();
        }

        private void AddFolder_Button_Click(object sender, RoutedEventArgs e)
        {
            folderNameWindow.Owner = this;
            folderNameWindow.ShowDialog();
            viewModel.updateFolders();
            collectionView = CollectionViewSource.GetDefaultView(viewModel.Folders);
        }

        private void DeleteFolder_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        /*
         * TODO
         * Ustawianie daty przypomnienia
         * Wyszukiwanie po nazwie
         * Dodać obsługę błędu w passwordWindow
         * Wyszukiwnie zaawansowane + walidacje
         * Usuwanie folderów
        */
    }
}

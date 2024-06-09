using NotepadDesktop.models;
using NotepadDesktop.repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NotepadDesktop.viewModels
{
    public class NoteEditorViewModel: INotifyPropertyChanged
    {
        private FolderRepository folderRepository;

        private Note? _selectedNote;

        public Note? SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Folder> Folders { get; set; }

        public NoteEditorViewModel(FolderRepository folderRepository)
        {
            this.folderRepository = folderRepository;
            Folders = new ObservableCollection<Folder>(folderRepository.GetAllFolders());
        }

        public void UpdateFolders()
        {
            Folders.Clear();
            folderRepository.GetAllFolders().ForEach(x => Folders.Add(x));
            OnPropertyChanged();
        }

        public void SaveNote()
        {
            Folder? folder = folderRepository.GetFolderByNoteId(SelectedNote!.Id);
            if (folder != null)
            {
                int index = folder.Notes.FindIndex(x => x.Id == SelectedNote!.Id);
                folder.Notes[index] = SelectedNote;
                folderRepository.UpdateFolder(folder);
            }

            SelectedNote = null;
        }

        public void CreateNote(string title, string content, Guid folderId)
        {
            Folder? folder = folderRepository.GetFolderById(folderId);
            if (folder != null)
            {
                Note note = new Note(title: title, content: content);
                folder.Notes.Add(note);
                folderRepository.UpdateFolder(folder);
            }
            SelectedNote = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

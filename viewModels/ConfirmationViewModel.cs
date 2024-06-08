using NotepadDesktop.models;
using NotepadDesktop.repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NotepadDesktop.viewModels
{
    public class ConfirmationViewModel: INotifyPropertyChanged
    {
        private FolderRepository folderRepository;
        private Note? _selectedNote;
        public Note? SelectedNote
        {
            get
            {
                return _selectedNote;
            }
            set
            {
                _selectedNote = value;
                OnPropertyChanged();
            }
        }

        public ConfirmationViewModel(FolderRepository noteRepository)
        {
            this.folderRepository = noteRepository;
        }

        public void DeleteSelectedNote()
        {
            if (SelectedNote == null) return;
            Folder? folder = folderRepository.GetFolderByNoteId(SelectedNote!.Id);
            if (folder == null) return;

            int index = folder.Notes.FindIndex(x => x.Id == SelectedNote.Id);
            if (index == -1) return;
            folder.Notes.RemoveAt(index);
            folderRepository.UpdateFolder(folder);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

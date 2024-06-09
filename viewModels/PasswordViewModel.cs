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
    public class PasswordViewModel: INotifyPropertyChanged
    {
        private FolderRepository folderRepository;
        private Note? _selectedNote;
        private bool _checkMode;

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

      
        public PasswordViewModel(FolderRepository folderRepository)
        {
            this.folderRepository = folderRepository;
        }

        public bool CheckPassword(int providedPassword)
        {
            Folder? folder = folderRepository.GetFolderByNoteId(SelectedNote.Id);
            if (folder == null) return false;
            int index = folder.Notes.FindIndex(x => x.Id == SelectedNote!.Id);
            if (index < 0) return false;

            return folder.Notes[index].Password == providedPassword;
        }

        public void SetPassword(int providedPassword)
        {
            Folder? folder = folderRepository.GetFolderByNoteId(SelectedNote.Id);
            if (folder == null) return;
            SelectedNote.Password = providedPassword;
            int index = folder.Notes.FindIndex(x => x.Id == SelectedNote!.Id);
            if (index < 0) return;
            folder.Notes[index] = SelectedNote;
            folderRepository.UpdateFolder(folder);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

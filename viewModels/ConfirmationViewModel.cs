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
        private FolderRepository noteRepository;
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
            this.noteRepository = noteRepository;
        }

        public void DeleteSelectedNote()
        {
            //noteRepository.DeleteNote(SelectedNote!);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

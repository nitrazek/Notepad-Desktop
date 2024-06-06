using NotepadDesktop.models;
using NotepadDesktop.repositories;
using System;
using System.Collections.Generic;
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

        public NoteEditorViewModel(FolderRepository noteRepository)
        {
            this.folderRepository = noteRepository;
        }


        public void SaveNote()
        {
            //folderRepository.UpdateNote(SelectedNote!);
            SelectedNote = null;
        }

        public void CreateNote(string title, string content)
        {
            Note note = new Note(title: title, content: content);
            //folderRepository.AddNote(note);
            SelectedNote = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

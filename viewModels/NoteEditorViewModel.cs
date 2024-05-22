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
        private NoteRepository noteRepository;

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

        public NoteEditorViewModel(NoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public void SaveNote(string title, string content)
        {
            SelectedNote!.Title = title;
            SelectedNote!.Content = content;
            noteRepository.UpdateNote(SelectedNote);
            SelectedNote = null;
        }

        public void CreateNote(string title, string content)
        {
            Note note = new Note(title: title, content: content);
            noteRepository.AddNote(note);
        }

        public void RefreshListeners()
        {
            OnPropertyChanged();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

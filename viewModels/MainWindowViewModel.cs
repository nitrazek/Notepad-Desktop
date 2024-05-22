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

namespace NotepadDesktop.viewModels
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        private NoteRepository noteRepository;
        private ObservableCollection<Note> _notes;
        private Note? _currentNote;

        public Note? CurrentNote
        {
            get { return _currentNote; }
            set
            {
                _currentNote = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                CurrentNote = null;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel(NoteRepository noteRepository) 
        {
            this.noteRepository = noteRepository;
            _notes = new ObservableCollection<Note>(noteRepository.GetAllNotes());
        }

        public void updateNotes()
        {
            Notes = new ObservableCollection<Note>(noteRepository.GetAllNotes());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

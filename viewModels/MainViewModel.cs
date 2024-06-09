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
    public class MainViewModel: INotifyPropertyChanged
    {
        private FolderRepository folderRepository;
        private ObservableCollection<Folder> _folders;
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

        public ObservableCollection<Folder> Folders
        {
            get
            {
                ObservableCollection<Folder> copy = new ObservableCollection<Folder>();
                foreach (var folder in _folders)
                {
                    copy.Add(new Folder(folder));
                }
                return copy;
            }
            set
            {
                _folders = value;
                CurrentNote = null;
                OnPropertyChanged();
            }
        }

        public MainViewModel(FolderRepository folderRepository) 
        {
            this.folderRepository = folderRepository;
            _folders = new ObservableCollection<Folder>(folderRepository.GetAllFolders());
        }

        public void updateFolders()
        {
            Folders = new ObservableCollection<Folder>(folderRepository.GetAllFolders());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

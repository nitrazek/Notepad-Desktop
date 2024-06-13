using NotepadDesktop.models;
using NotepadDesktop.repositories;
using NotepadDesktop.utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NotepadDesktop.viewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        private FolderRepository folderRepository;
        private List<Folder> _folders;
        private List<Folder> _treeList;
        private Note? _currentNote;
        private Folder? _currentFolder;

        public Note? CurrentNote
        {
            get { return _currentNote; }
            set
            {
                _currentNote = value; 
                OnPropertyChanged();
            }
        }

        public Folder? CurrentFolder
        {
            get { return _currentFolder; }
            set
            {
                _currentFolder = value;
                OnPropertyChanged();
            }

        }
        public List<Folder> Folders
        {
            get
            {
                List<Folder> copy = new List<Folder>();
                foreach (var folder in _folders)
                {
                    copy.Add(new Folder(folder));
                }
                return copy;
            }
            set
            {
                _folders = value;
                TreeList = value;
                if (CurrentNote != null)
                {
                    bool exist = false;
                    foreach (var folder in value)
                    {
                        foreach (var note in folder.Notes)
                        {
                            if (note.Id == CurrentNote.Id)
                            {
                                exist = true;
                                break;
                            }
                        }
                        if (exist) break;
                    }
                    if (exist) CurrentNote = null;
                }
                OnPropertyChanged();
            }
        }

        public List<Folder> TreeList
        {
            get { return _treeList; }
            set { _treeList = value; OnPropertyChanged(); }
        }

        public MainViewModel(FolderRepository folderRepository, NotificationManager notificationManager) 
        {
            this.folderRepository = folderRepository;
            _folders = folderRepository.GetAllFolders();
            _treeList = _folders;
            foreach (Folder folder in _folders)
            {
                foreach (Note note in folder.Notes)
                {
                    notificationManager.ScheduleNotification(note.NotificationDate, note.Title, note.Content, note.Id);
                }
            }
        }

        public void updateFolders()
        {
            if(CurrentNote != null)
            {
                Guid id = CurrentNote.Id;
                Folders = folderRepository.GetAllFolders();
                foreach(var folder in Folders)
                {
                    foreach(var note in folder.Notes)
                    {
                        if (note.Id == id)
                            CurrentNote = note;
                    }
                }
            }
            else
            {
                Folders = folderRepository.GetAllFolders();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

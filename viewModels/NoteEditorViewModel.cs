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
using System.Windows.Controls;

namespace NotepadDesktop.viewModels
{
    public class NoteEditorViewModel: INotifyPropertyChanged
    {
        private FolderRepository folderRepository;
        private NotificationManager notificationManager;

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

        public NoteEditorViewModel(FolderRepository folderRepository, NotificationManager notificationManager)
        {
            this.folderRepository = folderRepository;
            this.notificationManager = notificationManager;
            Folders = new ObservableCollection<Folder>(folderRepository.GetAllFolders());
        }

        public void UpdateFolders()
        {
            Folders.Clear();
            folderRepository.GetAllFolders().ForEach(x => Folders.Add(x));
            OnPropertyChanged();
        }

        public void SaveNote(Guid folderId)
        {
            Folder? oldFolder = folderRepository.GetFolderByNoteId(SelectedNote!.Id);
            if (oldFolder != null)
            {
                int index = oldFolder.Notes.FindIndex(x => x.Id == SelectedNote!.Id);
                if (oldFolder.Id == folderId)
                {
                    oldFolder.Notes[index] = SelectedNote;
                    folderRepository.UpdateFolder(oldFolder);
                    return;
                }
                else
                {
                    oldFolder.Notes.RemoveAt(index);
                    folderRepository.UpdateFolder(oldFolder);
                }
            }

            Folder? newFolder = folderRepository.GetFolderById(folderId);
            if(newFolder != null)
            {
                newFolder.Notes.Add(SelectedNote);
                folderRepository.UpdateFolder(newFolder);
            }
            
            SelectedNote = null;
        }

        public void CreateNote(string title, string content, DateTime? notificationDate, Guid folderId)
        {
            Folder? folder = folderRepository.GetFolderById(folderId);
            if (folder != null)
            {
                Note note = new Note(title: title, content: content, notificationDate: notificationDate);
                folder.Notes.Add(note);
                folderRepository.UpdateFolder(folder);
                notificationManager.ScheduleNotification(notificationDate, title, content, note.Id);
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

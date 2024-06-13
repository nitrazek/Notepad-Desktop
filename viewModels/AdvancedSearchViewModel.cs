using NotepadDesktop.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NotepadDesktop.viewModels
{
    public class AdvancedSearchViewModel: INotifyPropertyChanged
    {
        private MainViewModel mainViewModel;

        public AdvancedSearchViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public void filterTreeView(string title, string content, DateTime? fromCreateDate, DateTime? toCreateDate, DateTime? fromNotificationDate, DateTime? toNotificationDate)
        {
            List<Folder> folders = mainViewModel.Folders;
            foreach (var folder in folders)
            {
                if (folder == null) continue;

                folder.Notes = folder.Notes
                    .Where(note => note.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                    .Where(note => note.Content.Contains(content, StringComparison.OrdinalIgnoreCase))
                    .Where(note => fromCreateDate == null || note.CreatingDate.CompareTo(fromCreateDate.Value) > 0)
                    .Where(note => toCreateDate == null || note.CreatingDate.CompareTo(toCreateDate.Value) < 0)
                    .Where(note => fromNotificationDate == null || (note.NotificationDate != null && fromNotificationDate.Value.CompareTo(note.NotificationDate!.Value) < 0))
                    .Where(note => toNotificationDate == null || (note.NotificationDate != null && toNotificationDate.Value.CompareTo(note.NotificationDate!.Value) > 0))
                    .ToList();
            }

            mainViewModel.TreeList = folders.Where(folder => folder.Notes.Count > 0).ToList();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

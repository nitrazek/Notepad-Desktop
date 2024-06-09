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

        public bool CheckPassword(string providedPassword)
        {
            return true;
        }

        public void SetPassword(string providedPassword)
        {
            folerRepository;
            folerRepository.Get
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

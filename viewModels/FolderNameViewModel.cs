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
    public class FolderNameViewModel : INotifyPropertyChanged
    {
        private FolderRepository folderRepository;

        public FolderNameViewModel(FolderRepository folderRepository)
        {
            this.folderRepository = folderRepository;
        }

        public void CreateFolder(string name)
        {
            Folder folder = new Folder(name);
            folderRepository.AddFolder(folder);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

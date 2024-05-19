using NotepadDesktop.models;
using NotepadDesktop.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadDesktop.viewModels
{
    public class NoteEditorViewModel
    {
        private NoteRepository noteRepository;

        public NoteEditorViewModel(NoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        
    }
}

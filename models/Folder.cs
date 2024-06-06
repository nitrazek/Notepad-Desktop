using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadDesktop.models
{
    public class Folder
    {
        private Guid id;
        private string name;
        private List<Note> notes;

        public Guid Id { get { return id; } }
        public string Name { get { return name; } set { name = value; } }
        public List<Note> Notes { get { return notes; } set { notes = value; } }

        public Folder()
        {
            id = Guid.NewGuid();
            name = string.Empty;
            notes = new();
        }

        public Folder(string name, List<Note>? notes = null)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            if(notes == null)
                this.notes = new List<Note>();
            else
                this.notes = notes;
        }

        public Folder(Folder oldFolder)
        {
            this.id = oldFolder.Id;
            this.name = oldFolder.Name;
            this.notes = new List<Note>();
            foreach (Note note in oldFolder.Notes)
                notes.Add(new Note(note));
        }
    }
}

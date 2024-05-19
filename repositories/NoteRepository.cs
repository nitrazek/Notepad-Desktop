using NotepadDesktop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadDesktop.repositories
{
    public class NoteRepository
    {
        List<Note> notes = new List<Note>() { new Note(title: "test", content: "dupa")};

        public NoteRepository() { }

        public void AddNote(Note note)
        {
            notes.Add(note);
        }

        public Note? GetNoteById(Guid Id)
        {
            Note? note = notes.Find(note => note.Id == Id);
            if (note == null) return null;
            return new Note(note);
        }

        public List<Note> GetAllNotes()
        {
            return notes.Select(note => new Note(note)).ToList();
        }

        public void UpdateNote(Note editedNote)
        {
            int index = notes.FindIndex(note => note.Id == editedNote.Id);
            if(index > -1)
                notes[index] = editedNote;
        }

        public void DeleteNote(Guid Id)
        {
            Note? note = GetNoteById(Id);
            if (note != null)
                notes.Remove(note);
        }
    }
}
